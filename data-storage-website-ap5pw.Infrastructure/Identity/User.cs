using Microsoft.AspNetCore.Identity;
using data_storage_website_ap5pw.Domain.Entities.Interfaces;

namespace data_storage_website_ap5pw.Infrastructure.Identity
{
    /// <summary>
    /// Our User class which can be modified
    /// </summary>
    public class User : IdentityUser<int>, IUser<int>
    {
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
    }
}