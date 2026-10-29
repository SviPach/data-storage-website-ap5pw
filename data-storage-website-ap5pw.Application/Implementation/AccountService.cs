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

        public async Task<IList<IUser<int>>> GetAllUsers()
        {
            if (typeof(TKey) != typeof(int))
                throw new NotSupportedException(
                    "Only integer user IDs are supported.");

            var users = await _accountManager.GetAllUsers();

            return users
                .Cast<IUser<int>>()
                .ToList();
        }

        public async Task<IUser<int>?> GetUserById(int userId)
        {
            if (typeof(TKey) != typeof(int))
                throw new NotSupportedException(
                    "Only integer user IDs are supported.");

            var user = await _accountManager.GetUserById((TKey)(object)userId);

            return (IUser<int>?)(object?)user;
        }

        public async Task<IList<UserViewModel>> GetUsersWithRoles()
        {
            if (typeof(TKey) != typeof(int))
                throw new NotSupportedException(
                    "Only integer user IDs are supported.");

            var users = await _accountManager.GetAllUsers();
            var result = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _accountManager.GetUserRoles(
                    (TKey)(object)user.Id);

                result.Add(new UserViewModel
                {
                    Id = (int)(object)user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return result;
        }

        public async Task<(bool Success, string Message)> UpdateUser(
            EditUserViewModel model)
        {
            if (typeof(TKey) != typeof(int))
                return (false, "Unsupported user ID type.");

            if (model.Id <= 0)
                return (false, "Invalid user ID.");

            return await _accountManager.UpdateUser(
                (TKey)(object)model.Id,
                model.FirstName,
                model.LastName,
                model.UserName,
                model.Email,
                model.Role);
        }

        public async Task<IList<string>> GetUserRoles(int userId)
        {
            if (typeof(TKey) != typeof(int))
                throw new NotSupportedException(
                    "Only integer user IDs are supported.");

            return await _accountManager.GetUserRoles((TKey)(object)userId);
        }
    }
}