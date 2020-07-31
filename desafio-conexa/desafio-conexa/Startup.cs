using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_conexa.DbContexts;
using desafio_conexa.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using desafio_conexa.Repository;

namespace desafio_conexa
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<CidadesDbContext>(opt => opt.UseInMemoryDatabase("LocalDB"));
            services.AddDbContext<TemperaturasDbContext>(opt => opt.UseInMemoryDatabase("LocalDB"));
            //services.AddDbContext<CidadesDbContext>((sp, option) => option.UseSqlServer(Configuration.GetConnectionString("BdSqlServer"),p => p.CommandTimeout(60)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            //services.AddDbContext<TemperaturasDbContext>((sp,option) => option.UseSqlServer(Configuration.GetConnectionString("BdSqlServer"), p => p.CommandTimeout(60)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true) ;

            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Temperaturas", Description = "Retorna a temperatura de uma cidade por nome ou localização." });
            });

            services.AddScoped<CidadeService>();
            services.AddScoped<TemperaturaService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api temperatura");
                c.RoutePrefix = "api/documentation";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
