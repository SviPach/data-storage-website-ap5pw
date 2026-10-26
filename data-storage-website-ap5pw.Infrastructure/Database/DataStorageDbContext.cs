using data_storage_website_ap5pw.Domain.Entities;
using data_storage_website_ap5pw.Infrastructure.Database.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using data_storage_website_ap5pw.Infrastructure.Identity;

namespace data_storage_website_ap5pw.Infrastructure.Database
{
    public class DataStorageDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<data_storage_website_ap5pw.Domain.Entities.File> Files { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DataStorageDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            FileInit fileInit = new FileInit();
            modelBuilder.Entity<Domain.Entities.File>().HasData(fileInit.GetFiles3());
            CarouselInit carouselInit = new CarouselInit();
            modelBuilder.Entity<Carousel>().HasData(carouselInit.GetCarouselsIT3());
        }
    }
}