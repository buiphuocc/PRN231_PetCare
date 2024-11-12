using Application.Commons;
using Application.IService;
using Infrastructure.ViewModels.UserDTO;
using AutoMapper;
using Domain.Entities;
using Infrastructure.ServiceResponse;
using Infrastructure.Utils;
using Microsoft.Extensions.Caching.Memory;
using System.Data.Common;
using Infrastructure;
using Infrastructure.IService;
using Microsoft.Extensions.Options;


namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentTime _currentTime;
        private readonly AppConfiguration _config;
        private readonly IMemoryCache _memoryCache;

        public AuthenticationService(IMemoryCache memoryCache, IUnitOfWork unitOfWork, IMapper mapper,
        IOptions<AppConfiguration> configuration, ICurrentTime currentTime)
        {
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = configuration.Value; // Accessing the configured AppConfiguration
            _currentTime = currentTime;
        }

        public async Task<TokenResponse<string>> VerifyForgotPassCode(VerifyOTPResetDTO dto)
        {
            var response = new TokenResponse<string>();
            try
            {
                string key = $"{dto.Email}_OTP";
                if (_memoryCache.TryGetValue(key, out string? savedCode))
                {
                    if (savedCode == dto.CodeOTP)
                    {
                        response.Success = true;
                        response.Message = "OTP is valid.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Invalid OTP.";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "OTP not found.";
                }

                return response;
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<TokenResponse<string>> ForgotPass(string email)
        {
            var response = new TokenResponse<string>();
            try
            {
                var existEmail = await _unitOfWork.UserRepository.CheckEmailAddressExisted(email);
                if (existEmail == false)
                {
                    response.Success = false;
                    response.Message = "Email not found";
                    return response;
                }
                var user = await _unitOfWork.UserRepository.GetUserByEmail(email);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Invalid username";
                    return response;
                }
                if (user.EmailConfirmToken != null && !user.isActivated)
                {
                    System.Console.WriteLine(user.EmailConfirmToken + user.isActivated);
                    response.Success = false;
                    response.Message = "Please confirm via link in your mail";
                    return response;
                }
                var tokenEmail = await GenerateRandomPasswordResetTokenByEmailAsync(email);
                var codeEmail = SendMail.GenerateRandomCodeWithExpiration(tokenEmail, 1);
                var codeEmailSent = await SendMail.SendResetPass(_memoryCache, email, codeEmail, false);
                if (!codeEmailSent)
                {
                    response.Success = false;
                    response.Message = "Error when sending email";
                    return response;
                }

                response.Success = true;
                response.Message = "An email with a code to reset your password has been sent.";
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public Task<string> GenerateRandomPasswordResetTokenByEmailAsync(string email)
        {
            Random random = new Random();
            var token = "";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (int i = 0; i < 8; i++)
            {
                token += chars[random.Next(chars.Length)];
            }

            return Task.FromResult(token);
        }

        public async Task<TokenResponse<string>> LoginAsync(LoginUserDTO userObject)
        {
            var response = new TokenResponse<string>();

            try
            {
                // Hash the incoming password
                var passHash = HashPass.HashWithSHA256(userObject.PasswordHash);

               
                // Attempt to find the user with the provided credentials
                var userLogin = await _unitOfWork.UserRepository.GetUserByEmailAddressAndPasswordHash(userObject.Email, passHash);

                if (userLogin == null)
                {
                    // Invalid credentials
                    response.Success = false;
                    response.Message = "Invalid username or password.";
                    return response;
                }

                // Check if the user is activated
                if (!userLogin.isActivated && userLogin.EmailConfirmToken != null)
                {
                    response.Success = false;
                    response.Message = "Please confirm your email.";
                    return response;
                }
               // var userRole = await _unitOfWork.UserRepository.GetUserById(userLogin.AccountId);
                // Generate access and refresh tokens
                var token = GenerateJsonWebTokenString.GenerateJsonWebToken(userLogin, _config);
                var accessToken = GenerateJsonWebTokenString.GenerateAccessToken(userLogin, _config);
                var refreshToken = GenerateJsonWebTokenString.GenerateRefreshToken();

                // Store the refresh token in the database
                userLogin.RefreshToken = refreshToken;
                await _unitOfWork.UserRepository.Update(userLogin);

                // Populate the response
                response.Success = true;
                response.Message = "Login successfully.";
                response.DataToken = accessToken; // Store the access token in DataToken
                response.RefreshToken = refreshToken; // Store the refresh token directly
               // response.Role = userRole;
                return response;
            }
            catch (DbException ex)
            {
                // Handle database error
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
                // Log the exception for further analysis
                return response;
            }
            catch (Exception ex)
            {
                // Handle general error
                response.Success = false;
                response.Message = "An error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
                // Log the exception for further analysis
                return response;
            }
        }

       
        public async Task<ServiceResponse<RegisterDTO>> RegisterAsync(RegisterDTO userObjectDTO)
        {
            var response = new ServiceResponse<RegisterDTO>();
            try
            {
                var existEmail = await _unitOfWork.UserRepository.CheckEmailAddressExisted(userObjectDTO.Email);
                if (existEmail)
                {
                    response.Success = false;
                    response.Message = "Email already exists";
                    return response;
                }

                var userAccountRegister = _mapper.Map<Account>(userObjectDTO);
                userAccountRegister.PasswordHash = HashPass.HashWithSHA256(userObjectDTO.Password);

                // Create a token for email confirmation
                userAccountRegister.EmailConfirmToken = Guid.NewGuid().ToString();

                // Set initial activation status
                userAccountRegister.isActivated = true;
                userAccountRegister.Role = "Customer";

                // Generate a refresh token
                var refreshToken = GenerateJsonWebTokenString.GenerateRefreshToken();
                userAccountRegister.RefreshToken = refreshToken; // Assuming you have a RefreshToken property in your Account entity

                await _unitOfWork.UserRepository.AddAsync(userAccountRegister);

                // Send email confirmation link
                var confirmationLink = $"http://localhost:5211/swagger/confirm?token={userAccountRegister.EmailConfirmToken}";
                var emailSent = await SendMail.SendConfirmationEmail(userObjectDTO.Email, confirmationLink);
                if (!emailSent)
                {
                    response.Success = false;
                    response.Message = "Error when sending confirmation email";
                    return response;
                }

                var accountRegisteredDTO = _mapper.Map<RegisterDTO>(userAccountRegister);

                // Include refresh token in the response
                response.Success = true;
                response.Data = accountRegisteredDTO;
                response.Message = "Registration successful.";
                response.RefreshToken = refreshToken; // Include the refresh token in the response

            }
            catch (DbException e)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { e.Message };
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { e.Message };
            }

            return response;
        }


        public async Task<ServiceResponse<ResetPassDTO>> ResetPass(ResetPassDTO dto)
        {
            var response = new ServiceResponse<ResetPassDTO>();
            try
            {
                var userAccount = await _unitOfWork.UserRepository.GetUserByEmailAsync(dto.Email);
                if (userAccount == null)
                {
                    response.Success = false;
                    response.Message = "Email not found";
                    return response;
                }

                if (dto.Password != dto.ConfirmPassword)
                {
                    response.Success = false;
                    response.Message = "Password and Confirm Password do not match.";
                    return response;
                }

                userAccount.PasswordHash = HashPass.HashWithSHA256(dto.Password);
                await _unitOfWork.UserRepository.Update(userAccount);


                var accountRegistedDTO = _mapper.Map<ResetPassDTO>(userAccount);
                response.Success = true;
                response.Message = "Password reset successfully.";
            }
            catch (DbException e)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { e.Message };
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { e.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<RegisterDTO>> CreateStaff(RegisterDTO userObject)
        {
            var response = new ServiceResponse<RegisterDTO>();
            try
            {
                var existEmail = await _unitOfWork.UserRepository.CheckEmailAddressExisted(userObject.Email);
                if (existEmail)
                {
                    response.Success = false;
                    response.Message = "Email is already existed";
                    return response;
                }

                var userAccountRegister = _mapper.Map<Account>(userObject);
                userAccountRegister.PasswordHash = HashPass.HashWithSHA256(userObject.Password);
                //Create Token
                userAccountRegister.EmailConfirmToken = Guid.NewGuid().ToString();

                userAccountRegister.isActivated = true;
                userAccountRegister.Role = "Staff";

                await _unitOfWork.UserRepository.AddAsync(userAccountRegister);


                var confirmationLink =$"https://yourdomain.com/confirm?token={userAccountRegister.EmailConfirmToken}";
                //SendMail
                var emailSend = await SendMail.SendConfirmationEmail(userObject.Email, confirmationLink);
                if (!emailSend)
                {
                    response.Success = false;
                    response.Message = "Error when send mail";
                    return response;
                }

                var accountRegistedDTO = _mapper.Map<RegisterDTO>(userAccountRegister);
                response.Success = true;
                response.Data = accountRegistedDTO;
                response.Message = "Register successfully.";
            }
            catch (DbException e)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { e.Message };
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { e.Message };
            }

            return response;
        }



    }
}