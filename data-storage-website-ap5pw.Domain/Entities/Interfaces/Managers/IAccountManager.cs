using data_storage_website_ap5pw.Domain.Entities.Interfaces;
using data_storage_website_ap5pw.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace data_storage_website_ap5pw.Domain.Entities.Interfaces.Managers
{
    public interface IAccountManager<TKey> where TKey : notnull
    {
        Task<string[]> Create(IUser<TKey> user, string password, params Roles[] roles);

        Task<bool> Login(string username, string password);
        Task Logout();

        Task<string[]> ChangePassword(
            string username,
            string currentPassword,
            string newPassword);

        Task<(bool Success, string Message)> DeleteUser(int userId);

        Task<IList<IUser<TKey>>> GetAllUsers();

        Task<IUser<TKey>?> GetUserById(TKey userId);

        Task<IList<string>> GetUserRoles(TKey userId);

        Task<(bool Success, string Message)> UpdateUser(
            TKey userId,
            string? firstName,
            string? lastName,
            string? userName,
            string? email,
            string role);
    }
}