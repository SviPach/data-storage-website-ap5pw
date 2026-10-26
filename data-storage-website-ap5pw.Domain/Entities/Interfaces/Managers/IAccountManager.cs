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
    }
}