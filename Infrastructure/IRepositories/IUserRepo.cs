using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IUserRepo : IGenericRepo<Account>
    {
        Task<Account> GetUserByEmailAddressAndPasswordHash(string email, string passwordHash);
        Task<Account> GetUserByEmail(string email);
        Task<bool> CheckEmailAddressExisted(string emailaddress);
        Task<bool> CheckPhoneNumberExited(string phonenumber);
        Task<Account> GetUserByEmailAsync(string email);
        Task<Account> GetUserByConfirmationToken(string token);
        Task<Account> GetUserById(int id);
        Task<IEnumerable<Account?>> GetAllUsers();
        Task<int> CountUsersByRoleAsync(string role);
        Task<IEnumerable<Account?>> GetAllUsersAdmin();
        Task<IEnumerable<Account?>> GetAllUsersStaff();
        Task<IEnumerable<Account?>> GetAllUsersCustomer();
        Task AddUser(Account? user);
        Task UpdateUser(Account user);
        Task DeleteUser(int id);

        Task DisableUser(int id);
    }
}
