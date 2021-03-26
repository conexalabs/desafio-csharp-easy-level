using Microsoft.Extensions.DependencyInjection;
using Siagri.Clima.Business.Interfaces.Repositories;
using Siagri.Clima.Business.Interfaces.Services;
using Siagri.Clima.Business.Services;
using Siagri.Clima.Data.Repositories;

namespace Siagri.Clima.Data.IoC
{
    public static class Bootstrapper
    {
        public static void AddIoC(this IServiceCollection services)
        {
            services.AddServices();
            services.AddRepositories();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ILocalidadeService, LocalidadeService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILocalidadeRepository, LocalidadeRepository>();
        }
    }
}
