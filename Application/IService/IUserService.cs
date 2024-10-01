
using Infrastructure.ViewModels.UserDTO;
using Infrastructure.ServiceResponse;

namespace Application.IService
{
    public interface IUserService
    {
        Task<ServiceResponse<PaginationModel<UserDTO>>> GetAllUsers(int page, int pageSize, string search, string sort);
        Task<ServiceResponse<UserCountDTO>> CountUsersByRoleAsync(string role);
        Task<ServiceResponse<PaginationModel<UserDTO>>> GetAllUsersByStaff(int page, int pageSize, string search, string sort);
        Task<ServiceResponse<PaginationModel<UserDTO>>> GetAllUsersByAdmin(int page, int pageSize, string search, string sort);
        Task<ServiceResponse<PaginationModel<UserDTO>>> GetAllUsersByCustomer(int page, int pagesize, string search, string sort);
        Task<ServiceResponse<UserDTO>> GetUserById(int id);
        Task<ServiceResponse<int>> AddUser(UserDTO user);
        Task<ServiceResponse<string>> UpdateUser(UserUpdateDTO userUpdate);
        Task<ServiceResponse<string>> DeleteUser(int id);

        Task<ServiceResponse<string>> DisableUser(int id);

    }
}
