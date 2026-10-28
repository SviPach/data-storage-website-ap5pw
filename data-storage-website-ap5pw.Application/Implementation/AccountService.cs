using System;
using System.Collections.Generic;
using System.Text;
using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Application.Records;
using data_storage_website_ap5pw.Application.ViewModels;
using data_storage_website_ap5pw.Domain.Entities.Interfaces;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Managers;
using data_storage_website_ap5pw.Domain.Enums;

namespace data_storage_website_ap5pw.Application.Implementation
{
    public class AccountService<TKey> : IAccountService where TKey : notnull
    {
        IAccountManager<TKey> _accountManager;

        public AccountService(IAccountManager<TKey> accountManager)
        {
            _accountManager = accountManager;
        }


        public async Task<string[]> Register(RegisterViewModel vm, params Roles[] roles)
        {
            IUser<TKey> user = new UserRecord<TKey>
            {
                UserName = vm.Username,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                PhoneNumber = vm.Phone
            };

            var errors = await _accountManager.Create(user, vm.Password, roles);
            return errors;
        }


        public async Task<bool> Login(LoginViewModel vm)
        {
            return await _accountManager.Login(vm.Username, vm.Password);
        }

        public Task Logout()
        {
            return _accountManager.Logout();
        }

        public async Task<string[]> ChangePassword(
            string username,
            string currentPassword,
            string newPassword)
        {
            return await _accountManager.ChangePassword(
                username,
                currentPassword,
                newPassword);
        }

        public async Task<(bool Success, string Message)> DeleteUser(int userId)
        {
            if (typeof(TKey) != typeof(int))
                return (false, "Unsupported user ID type.");

            return await _accountManager.DeleteUser((int)(object)userId);
        }
    }
}