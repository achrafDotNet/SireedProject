using Microsoft.EntityFrameworkCore;
using Sireed.API.Data;
using Sireed.APPLICATION.ServicesIndicateurs;
using Sireed.INFRASTRUCTURE.RepositoryIndicateurs;
using Sireed.IP.Extensions;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Text.Json;
using Sireed.IP.RepÎP;
using Sireed.IP.SerIP;
using Sireed.API.Views.Indicateurs.ThematiqueHelper.NavigationTableBoards;
using Sireed.API.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var builder = WebApplication.CreateBuilder(args);

// Ajouter CORS pour permettre les appels depuis votre frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Add Razor Pages services.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));


//builder.Services.AddScoped<IRepositoryIndicateurs, RepositoryIndicateur>();
//builder.Services.AddScoped<IServicesIndicateur, IndicateurService>();
//builder.Services.AddScoped<IRepIPrepository, RepIPrepository>();
//builder.Services.AddScoped<ISerIPservice, SerIPservice>();
//builder.Services.AddScoped<INavigation, RNavigation>();


ServiceCollectionExtensions.AddAllServices(builder.Services);


LicenceSireed.ConfigureServices(builder.Services); // Add Dependecie de IP
// Add services to the container.

// Add services to the container.
builder.Services.AddControllersWithViews() // Configure Controllers with Views
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });



var app = builder.Build();

// Utiliser CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapControllers();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
pattern: "{controller=Home}/{action=SireedHome}/{id?}");

app.Run();
