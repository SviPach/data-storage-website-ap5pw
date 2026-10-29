using data_storage_website_ap5pw.Domain.Entities;
using data_storage_website_ap5pw.Infrastructure.Database.Seeding;
using data_storage_website_ap5pw.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace data_storage_website_ap5pw.Infrastructure.Database
{
    public class DataStorageDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<data_storage_website_ap5pw.Domain.Entities.File> Files { get; set; }
        public DbSet<Carousel> Carousels { get; set; }

        public DataStorageDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // File.AuthorId <-> User.Id foreign key
            modelBuilder.Entity<Domain.Entities.File>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            FileInit fileInit = new FileInit();
            modelBuilder.Entity<Domain.Entities.File>().HasData(fileInit.GetFiles3());
            CarouselInit carouselInit = new CarouselInit();
            modelBuilder.Entity<Carousel>().HasData(carouselInit.GetCarouselsIT3());

            //Identity - User and Role initialization
            //roles must be added first
            RoleInit rolesInit = new RoleInit();
            modelBuilder.Entity<Role>().HasData(rolesInit.GetRolesAMC());

            //then, create users ..
            UserInit userInit = new UserInit();
            User admin = userInit.GetAdmin();
            User manager = userInit.GetManager();

            //.. and add them to the table ..
            modelBuilder.Entity<User>().HasData(admin, manager);

            //.. and finally, connect the users with the roles
            UserRoleInit userRolesInit = new UserRoleInit();
            List<IdentityUserRole<int>> adminUserRoles = userRolesInit.GetRolesForAdmin();
            List<IdentityUserRole<int>> managerUserRoles = userRolesInit.GetRolesForManager();
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(adminUserRoles);
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(managerUserRoles);
        }
    }
}