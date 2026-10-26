using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Application.Implementation;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Managers;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository;
using data_storage_website_ap5pw.Infrastructure.Database;
using data_storage_website_ap5pw.Infrastructure.Identity;
using data_storage_website_ap5pw.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using data_storage_website_ap5pw.Infrastructure.Managers;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("MySQL");
ServerVersion serverVersion = MySqlServerVersion.AutoDetect(connectionString);
if (serverVersion == null)
{
    //if the AutoDetect() does not work, you can specify the version manually
    serverVersion = new MySqlServerVersion("8.0.43");
}
builder.Services.AddDbContext<DataStorageDbContext>(optionsBuilder => optionsBuilder.UseMySql(connectionString, serverVersion));

//Configuration for Identity
builder.Services.AddIdentity<User, Role>()
     .AddEntityFrameworkStores<DataStorageDbContext>()
     .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 1;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Security/Account/Login";
    options.LogoutPath = "/Security/Account/Logout";
    options.SlidingExpiration = true;
});

//repository registration
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ICarouselRepository, CarouselRepository>();

//Account manager registration
builder.Services.AddScoped<IAccountManager<int>, AccountIdentityManager>();

//registration of application services
builder.Services.AddScoped<IFileAppService, FileAppService>();
builder.Services.AddScoped<ICarouselAppService, CarouselAppService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IAccountService, AccountService<int>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();