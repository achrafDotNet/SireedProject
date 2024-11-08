using Microsoft.EntityFrameworkCore;
using Sireed.API.Data;
using Sireed.APPLICATION.ServicesIndicateurs;
using Sireed.INFRASTRUCTURE.RepositoryIndicateurs;
using Sireed.IP.Extensions;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Text.Json;
using Sireed.IP.Rep�P;
using Sireed.IP.SerIP;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Add Razor Pages services.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));


builder.Services.AddScoped<IRepositoryIndicateurs, RepositoryIndicateur>();
builder.Services.AddScoped<IServicesIndicateur, IndicateurService>();
builder.Services.AddScoped<IRepIPrepository, RepIPrepository>();
builder.Services.AddScoped<ISerIPservice, SerIPservice>();

LicenceSireed.ConfigureServices(builder.Services); // Add Dependecie de IP
// Add services to the container.

// Add services to the container.
builder.Services.AddControllersWithViews() // Configure Controllers with Views
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });


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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=SireedHome}/{id?}");

app.Run();
