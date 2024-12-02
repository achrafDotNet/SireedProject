using Sireed.API.Views.Indicateurs.ThematiqueHelper.NavigationTableBoards;
using Sireed.APPLICATION.ServicesIndicateurs;
using Sireed.INFRASTRUCTURE.RepositoryIndicateurs;
using Sireed.IP.RepÎP;
using Sireed.IP.SerIP;

namespace Sireed.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAllServices(this IServiceCollection services) {

            services.AddScoped<IRepositoryIndicateurs, RepositoryIndicateur>();
            services.AddScoped<IServicesIndicateur, IndicateurService>();
            services.AddScoped<IRepIPrepository, RepIPrepository>();
            services.AddScoped<ISerIPservice, SerIPservice>();
            services.AddScoped<INavigation, RNavigation>();

        }
    }
}
