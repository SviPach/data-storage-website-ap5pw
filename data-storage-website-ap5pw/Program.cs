using Microsoft.EntityFrameworkCore;
using data_storage_website_ap5pw.Application.Abstraction;
using data_storage_website_ap5pw.Application.Implementation;
using data_storage_website_ap5pw.Domain.Entities.Interfaces.Repository;
using data_storage_website_ap5pw.Infrastructure.Database;
using data_storage_website_ap5pw.Infrastructure.Repository;

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

//repository registration
builder.Services.AddScoped<IFileRepository, FileRepository>();

//registration of application services
builder.Services.AddScoped<IFileAppService, FileAppService>();

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