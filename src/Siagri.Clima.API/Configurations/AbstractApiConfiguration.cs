using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Siagri.Clima.Business.Dtos;
using Siagri.Clima.Business.Validators;
using Siagri.Clima.Data.Context;
using Siagri.Clima.Data.IoC;

namespace Siagri.Clima.API.Configurations
{
    public static class AbstractApiConfiguration
    {

        public static void AddApiConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            //contexto
            services.AddDbContext<SiagriContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //ioc
            services.AddIoC();

            //cors

            services.AddCors();

            //mvc

            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CidadeDtoValidator>());

            //swagger

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Siagri Clima API" });
            });
        }

        public static void UseApiConfigurations(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(option => option.AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Siagri Clima API");
            });
        }
    }
}
