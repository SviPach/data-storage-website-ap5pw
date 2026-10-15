using data_storage_website_ap5pw.Domain.Entities;

namespace data_storage_website_ap5pw.Domain.Entities
{
    public class File : Entity<int>
    {
        public string FileName { get; set; } = string.Empty;

        public long Size { get; set; }

        public string StoragePath { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}

