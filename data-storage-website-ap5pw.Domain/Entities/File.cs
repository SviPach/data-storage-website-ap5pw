namespace data_storage_website_ap5pw.Domain.Entities
{
    public class File
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public long Size { get; set; }
        public required string Path { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
    }
}
