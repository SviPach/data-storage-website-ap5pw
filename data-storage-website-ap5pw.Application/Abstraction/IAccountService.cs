using data_storage_website_ap5pw.Application.ViewModels;
using data_storage_website_ap5pw.Domain.Entities.Interfaces;
using data_storage_website_ap5pw.Domain.Enums;

namespace data_storage_website_ap5pw.Application.Abstraction
{
    public interface IAccountService
    {
        Task<string[]> Register(RegisterViewModel vm, params Roles[] roles);

        Task<bool> Login(LoginViewModel vm);
        Task Logout();

        Task<string[]> ChangePassword(
            string username,
            string currentPassword,
            string newPassword);

        Task<(bool Success, string Message)> DeleteUser(int userId);

        Task<IList<IUser<int>>> GetAllUsers();

        Task<IUser<int>?> GetUserById(int userId);

        Task<IList<UserViewModel>> GetUsersWithRoles();

        Task<(bool Success, string Message)> UpdateUser(
            EditUserViewModel model);

        Task<IList<string>> GetUserRoles(int userId);
    }
}