using UTB.Eshop.Domain.Entities;

namespace data_storage_website_ap5pw_Domain.Entities;

public class User : Entity<int>
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}