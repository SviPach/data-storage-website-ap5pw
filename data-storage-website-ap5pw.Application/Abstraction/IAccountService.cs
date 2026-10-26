using System;
using System.Collections.Generic;
using System.Text;
using data_storage_website_ap5pw.Application.ViewModels;
using data_storage_website_ap5pw.Domain.Enums;

namespace data_storage_website_ap5pw.Application.Abstraction
{
    public interface IAccountService
    {
        Task<string[]> Register(RegisterViewModel vm, params Roles[] roles);

        Task<bool> Login(LoginViewModel vm);
        Task Logout();
    }
}