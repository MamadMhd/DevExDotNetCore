using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DevExtremeAspNetCoreApp1.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DevExtremeAspNetCoreApp1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevExtremeAspNetCoreApp1Context") ?? throw new InvalidOperationException("Connection string 'DevExtremeAspNetCoreApp1Context' not found.")));

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id?}");

app.Run();
