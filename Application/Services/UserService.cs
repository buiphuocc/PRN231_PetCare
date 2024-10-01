
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.ServiceResponse;
using Infrastructure.Ultilities;
using Infrastructure.ViewModels.UserDTO;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ServiceResponse<PaginationModel<UserDTO>>> GetAllUsers(int page, int pageSize, string search,
            string sort)
        {
            var response = new ServiceResponse<PaginationModel<UserDTO>>();

            try
            {
                var users = await _userRepo.GetAllUsers();
                if (!string.IsNullOrEmpty(search))
                {
                    users = users
                        .Where(u => u != null && (u.Email.Contains(search, StringComparison.OrdinalIgnoreCase)));
                }

                users = sort.ToLower() switch
                {
                    "name" => users.OrderBy(u => u?.AccountId),
                    "email" => users.OrderBy(u => u?.Email),
                    "role" => users.OrderBy(u => u?.Role),
                    "status" => users.OrderBy(u => u?.isActivated),
                    _ => users.OrderBy(u => u?.AccountId).ToList()
                };
                var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

                var paginationModel =
                    await Pagination.GetPaginationIENUM(userDTOs, page, pageSize); // Adjust pageSize as needed

                response.Data = paginationModel;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to retrieve users: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<UserCountDTO>> CountUsersByRoleAsync(string role)
        {
            var response = new ServiceResponse<UserCountDTO>();

            try
            {
                int userCount = await _userRepo.CountUsersByRoleAsync(role);

                var userCountDTO = new UserCountDTO
                {
                    UserCount = userCount,
                    RoleName = role
                };

                response.Data = userCountDTO;
                response.Success = true;
                response.Message = "User count retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to retrieve users by role: {ex.Message}";
            }

            return response;
        }



        public async Task<ServiceResponse<PaginationModel<UserDTO>>> GetAllUsersByStaff(int page, int pageSize,
            string search, string sort)
        {
            var response = new ServiceResponse<PaginationModel<UserDTO>>();

            try
            {
                var users = await _userRepo.GetAllUsersStaff();
                if (!string.IsNullOrEmpty(search))
                {
                    users = users
                        .Where(u => u != null && (u.Email.Contains(search, StringComparison.OrdinalIgnoreCase)));
                }

                users = sort.ToLower() switch
                {
                    "name" => users.OrderBy(u => u?.AccountId),
                    "email" => users.OrderBy(u => u?.Email),
                    "status" => users.OrderBy(u => u?.AccountId),
                    _ => users.OrderBy(u => u?.AccountId).ToList()
                };
                var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

                var paginationModel =
                    await Pagination.GetPaginationIENUM(userDTOs, page, pageSize); // Adjust pageSize as needed

                response.Data = paginationModel;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to retrieve staff users: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<PaginationModel<UserDTO>>> GetAllUsersByAdmin(int page, int pageSize,
            string search, string sort)
        {
            var response = new ServiceResponse<PaginationModel<UserDTO>>();

            try
            {
                var users = await _userRepo.GetAllUsersAdmin();
                if (!string.IsNullOrEmpty(search))
                {
                    users = users
                        .Where(u => u != null && (u.Email.Contains(search, StringComparison.OrdinalIgnoreCase)));
                }

                users = sort.ToLower() switch
                {
                    "name" => users.OrderBy(u => u?.AccountId),
                    "email" => users.OrderBy(u => u?.Email),
                    "status" => users.OrderBy(u => u?.AccountId),
                    _ => users.OrderBy(u => u?.AccountId).ToList()
                };
                var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

                var paginationModel =
                    await Pagination.GetPaginationIENUM(userDTOs, page, pageSize); // Adjust pageSize as needed

                response.Data = paginationModel;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to retrieve admin users: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<PaginationModel<UserDTO>>> GetAllUsersByCustomer(int page, int pageSize,
            string search, string sort)
        {
            var response = new ServiceResponse<PaginationModel<UserDTO>>();

            try
            {
                var users = await _userRepo.GetAllUsersCustomer();
                if (!string.IsNullOrEmpty(search))
                {
                    users = users
                        .Where(u => u != null && (u.Email.Contains(search, StringComparison.OrdinalIgnoreCase)));
                }

                users = sort.ToLower() switch
                {
                    "name" => users.OrderBy(u => u?.AccountId),
                    "email" => users.OrderBy(u => u?.Email),
                    "status" => users.OrderBy(u => u?.AccountId),
                    _ => users.OrderBy(u => u?.AccountId).ToList()
                };
                var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

                var paginationModel =
                    await Pagination.GetPaginationIENUM(userDTOs, page, pageSize); // Adjust pageSize as needed

                response.Data = paginationModel;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to retrieve customer users: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<UserDTO>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<UserDTO>();

            try
            {
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                }
                else
                {
                    var userDTO = _mapper.Map<UserDTO>(user);
                    serviceResponse.Data = userDTO;
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> AddUser(UserDTO user)
        {
            var serviceResponse = new ServiceResponse<int>();

            try
            {
                var userEntity = _mapper.Map<Account>(user);
                await _userRepo.AddUser(userEntity);

                serviceResponse.Data = userEntity.AccountId; // Assuming Id is set after insertion
                serviceResponse.Success = true;
                serviceResponse.Message = "User created successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Failed to create user: {ex.Message}";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> UpdateUser(UserUpdateDTO userUpdate)
        {
            var serviceResponse = new ServiceResponse<string>();

            try
            {
                var userEntity = _mapper.Map<Account>(userUpdate);
                await _userRepo.UpdateUser(userEntity);

                serviceResponse.Success = true;
                serviceResponse.Message = "User updated successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Failed to update user: {ex.Message}";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<string>();

            try
            {
                await _userRepo.DeleteUser(id);

                serviceResponse.Success = true;
                serviceResponse.Message = "User deleted successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Failed to delete user: {ex.Message}";
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<string>> DisableUser(int id)
        {
            var serviceResponse = new ServiceResponse<string>();

            try
            {
                await _userRepo.DisableUser(id);

                serviceResponse.Success = true;
                serviceResponse.Message = "User disable successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Failed to disable user: {ex.Message}";
            }

            return serviceResponse;
        }
    }
}