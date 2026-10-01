namespace data_storage_website_ap5pw.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public ICollection<File> Files { get; set; }
    }
}
