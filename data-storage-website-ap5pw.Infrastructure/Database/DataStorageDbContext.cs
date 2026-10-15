using Microsoft.EntityFrameworkCore;
using data_storage_website_ap5pw.Domain.Entities;

namespace data_storage_website_ap5pw.Infrastructure.Database
{
    public class DataStorageDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<data_storage_website_ap5pw.Domain.Entities.File> Files { get; set; }
        public DataStorageDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}