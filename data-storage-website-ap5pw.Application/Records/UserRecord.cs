using data_storage_website_ap5pw.Domain.Entities.Interfaces;

namespace data_storage_website_ap5pw.Application.Records
{
    internal record UserRecord<TKey> : IUser<TKey> where TKey : notnull
    {
        public TKey Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}