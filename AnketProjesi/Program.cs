using Microsoft.EntityFrameworkCore;
using AnketProjesi.Repository;

var builder = WebApplication.CreateBuilder(args);
// Veritabaný baðlantý dizesini almak için Configuration ekleyin
builder.Configuration.AddJsonFile("appsettings.json");

// Veritabaný baðlantýsýný ekleyin
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<anket3DbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Deneme}/{id?}");

app.Run();
