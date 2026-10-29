using data_storage_website_ap5pw.Domain.Entities.Interfaces;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Managers;
using data_storage_website_ap5pw.Domain.Enums;
using data_storage_website_ap5pw.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using data_storage_website_ap5pw.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace data_storage_website_ap5pw.Infrastructure.Managers
{
    public class AccountIdentityManager : IAccountManager<int>
    {
        UserManager<User> userManager;
        SignInManager<User> sigInManager;
        private readonly DataStorageDbContext _context;

        public AccountIdentityManager(
            UserManager<User> userManager,
            SignInManager<User> sigInManager,
            DataStorageDbContext context)
        {
            this.userManager = userManager;
            this.sigInManager = sigInManager;
            _context = context;
        }


        public async Task<string[]> Create(
    IUser<int> userIn,
    string password,
    params Roles[] roles)
        {
            var user = new User
            {
                UserName = userIn.UserName,
                FirstName = userIn.FirstName,
                LastName = userIn.LastName,
                Email = userIn.Email,
                PhoneNumber = userIn.PhoneNumber
            };

            var errors = new List<string>();

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return result.Errors.Select(e => e.Description).ToArray();

            foreach (var role in roles)
            {
                var roleResult = await userManager.AddToRoleAsync(
                    user, role.ToString());

                if (!roleResult.Succeeded)
                {
                    errors.AddRange(
                        roleResult.Errors.Select(e => e.Description));
                }
            }

            return errors.ToArray();
        }


        public async Task<bool> Login(string username, string password)
        {
            var result = await sigInManager.PasswordSignInAsync(username, password, true, true);
            return result.Succeeded;
        }

        public Task Logout()
        {
            return sigInManager.SignOutAsync();
        }

        public async Task<string[]> ChangePassword(
            string username,
            string currentPassword,
            string newPassword)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
                return new[] { "User was not found." };

            var result = await userManager.ChangePasswordAsync(
                user,
                currentPassword,
                newPassword);

            if (result.Succeeded)
                return Array.Empty<string>();

            return result.Errors
                .Select(error => error.Description)
                .ToArray();
        }

        public async Task<(bool Success, string Message)> DeleteUser(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return (false, "User not found.");

            if (await userManager.IsInRoleAsync(user, nameof(Roles.Admin)))
                return (false, "Administrator cannot be deleted.");

            var admin = (await userManager.GetUsersInRoleAsync(nameof(Roles.Admin)))
                .FirstOrDefault();

            if (admin == null)
                return (false, "No administrator exists.");

            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var files = await _context.Files
                    .Where(f => f.AuthorId == user.Id)
                    .ToListAsync();

                foreach (var file in files)
                    file.AuthorId = admin.Id;

                await _context.SaveChangesAsync();

                var result = await userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();

                    return (
                        false,
                        string.Join(" ", result.Errors.Select(e => e.Description))
                    );
                }

                await transaction.CommitAsync();

                return (
                    true,
                    $"User deleted successfully. {files.Count} file(s) reassigned to administrator {admin.UserName}."
                );
            }
            catch
            {
                await transaction.RollbackAsync();

                return (
                    false,
                    "An error occurred while deleting the user. No changes were saved."
                );
            }
        }

        public async Task<IList<IUser<int>>> GetAllUsers()
        {
            return await userManager.Users
                .OrderBy(u => u.UserName)
                .Cast<IUser<int>>()
                .ToListAsync();
        }

        public async Task<IUser<int>?> GetUserById(int userId)
        {
            return await userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<IList<string>> GetUserRoles(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return new List<string>();

            var roles = await userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<(bool Success, string Message)> UpdateUser(
            int userId,
            string? firstName,
            string? lastName,
            string? userName,
            string? email,
            string role)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return (false, "User not found.");

            if (await userManager.IsInRoleAsync(user, nameof(Roles.Admin)))
                return (false, "Administrator cannot be edited.");

            if (role != nameof(Roles.Customer) &&
                role != nameof(Roles.Manager))
            {
                return (false, "Invalid role selected.");
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserName = userName;
            user.Email = email;

            var updateResult = await userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return (
                    false,
                    string.Join(" ", updateResult.Errors.Select(e => e.Description))
                );
            }

            var currentRoles = await userManager.GetRolesAsync(user);

            var rolesToRemove = currentRoles
                .Where(r => r == nameof(Roles.Customer) ||
                            r == nameof(Roles.Manager))
                .ToList();

            if (rolesToRemove.Count > 0)
            {
                var removeResult = await userManager.RemoveFromRolesAsync(
                    user, rolesToRemove);

                if (!removeResult.Succeeded)
                {
                    return (
                        false,
                        string.Join(" ", removeResult.Errors.Select(e => e.Description))
                    );
                }
            }

            var addResult = await userManager.AddToRoleAsync(user, role);

            if (!addResult.Succeeded)
            {
                return (
                    false,
                    string.Join(" ", addResult.Errors.Select(e => e.Description))
                );
            }

            return (true, "User profile and role updated successfully.");
        }
    }
}