using data_storage_website_ap5pw.Infrastructure.Identity;
using data_storage_website_ap5pw.Domain.Enums;

namespace data_storage_website_ap5pw.Infrastructure.Database.Seeding
{
    internal class RoleInit
    {
        public List<Role> GetRolesAMC()
        {
            List<Role> roles = new List<Role>();

            Role roleAdmin = new Role()
            {
                Id = 1,
                Name = Roles.Admin.ToString(),
                NormalizedName = Roles.Admin.ToString().ToUpper(),
                ConcurrencyStamp = "9cf14c2c-19e7-40d6-b744-8917505c3106"
            };

            Role roleManager = new Role()
            {
                Id = 2,
                Name = Roles.Manager.ToString(),
                NormalizedName = Roles.Manager.ToString().ToUpper(),
                ConcurrencyStamp = "be0efcde-9d0a-461d-8eb6-444b043d6660"
            };

            Role roleCustomer = new Role()
            {
                Id = 3,
                Name = Roles.Customer.ToString(),
                NormalizedName = Roles.Customer.ToString().ToUpper(),
                ConcurrencyStamp = "29dafca7-cd20-4cd9-a3dd-4779d7bac3ee"
            };


            roles.Add(roleAdmin);
            roles.Add(roleManager);
            roles.Add(roleCustomer);

            return roles;
        }
    }
}