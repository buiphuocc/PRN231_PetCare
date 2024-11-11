
using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepo : GenericRepo<Account>, IUserRepo
    {
        private readonly PetCareDbContext _dbContext;

        public UserRepo(PetCareDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<bool> CheckEmailAddressExisted(string email) =>
            await _dbContext.Accounts.AnyAsync(u => u.Email == email);

        public async Task<bool> CheckPhoneNumberExited(string phonenumber) =>
            await _dbContext.Accounts.AnyAsync(x => x.UserProfile.PhoneNumber == phonenumber);

        public async Task<Account> GetUserByConfirmationToken(string token)
        {
            return await _dbContext.Accounts.SingleOrDefaultAsync(
                u => u.EmailConfirmToken == token
            );
        }

        public async Task<Account> GetUserByEmailAddressAndPasswordHash(string email, string passwordHash)
        {

            var user = await _dbContext.Accounts
                .FirstOrDefaultAsync(record => record.Email.Equals(email));

            if (user is null)
            {
                throw new Exception("Email is not correct");
            }

            Console.WriteLine(user.PasswordHash + " compare to " + passwordHash);

            if (user.PasswordHash != passwordHash)
            {
                throw new Exception("Password is not correct");
            }

            return user;
        }


        public async Task<Account> GetUserByEmail(string email)
        {
            var user = await _dbContext.Accounts
                 .FirstOrDefaultAsync(record => record.Email == email);
            if (user is null)
            {
                throw new Exception("Email is not correct");
            }
            return user;
        }
        public async Task<Account> GetUserByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Account> GetUserById(int id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }

        public async Task<IEnumerable<Account?>> GetAllUsers()
        {
            return _dbContext.Accounts.Where(h => h.isActivated).ToList();
        }




        public async Task<int> CountUsersByRoleAsync(string roleName)
        {
            return await _dbContext.Accounts
                .Where(u => u.Role == roleName)
                .CountAsync();
        }


        public async Task<IEnumerable<Account?>> GetAllUsersAdmin()
        {
            return await _dbContext.Accounts
                .Where(u => u.Role == "Admin")
                .ToListAsync();
        }

        public async Task<IEnumerable<Account?>> GetAllUsersStaff()
        {
            return await _dbContext.Accounts
                .Where(u => u.Role == "Staff")
                .ToListAsync();
        }

        public async Task<IEnumerable<Account?>> GetAllUsersCustomer()
        {
            return await _dbContext.Accounts
                .Where(u => u.Role == "Customer")
                .ToListAsync();
        }


        public async Task AddUser(Account? user)
        {
            if (user != null) _dbContext.Accounts.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUser(Account updatedUser)
        {
            // Retrieve the existing user from the database
            var existingUser = await _dbContext.Accounts.FindAsync(updatedUser.AccountId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Update the properties
            existingUser.ShelterId = updatedUser.ShelterId;
            existingUser.Email = updatedUser.Email;
            existingUser.PasswordHash = updatedUser.PasswordHash;
            existingUser.Role = updatedUser.Role;
            existingUser.isActivated = updatedUser.isActivated;
            // Do not update the password to avoid setting it to null
            // existingUser.Password remains unchanged

            _dbContext.Accounts.Update(existingUser);
            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteUser(int id)
        {
            var user = await _dbContext.Accounts.FindAsync(id);
            if (user != null)
            {
                _dbContext.Accounts.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DisableUser(int id)
        {
            var user = await _dbContext.Accounts.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Update the user's status to "Disabled" or any relevant value
            user.isActivated = false; // Assuming "Disabled" is a valid status

            _dbContext.Accounts.Update(user);
            await _dbContext.SaveChangesAsync();
        }

    }
}