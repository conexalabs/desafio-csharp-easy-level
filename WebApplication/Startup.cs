using System;
using Application.Entidades.City;
using Infra.Data.DBContext;
using Infrastructure.IoC.Dependency;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(options =>  
            {  
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo  
                {  
                    Title = "Conexa Swagger",  
                    Version = "v2" 
                });  
            });  
            RegisterServices(services);
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ConexaContext>();
                
                context.Database.Migrate();
                AdicionarDadosTeste(context);
            }
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();  
            app.UseSwaggerUI(options =>options.SwaggerEndpoint("/swagger/v2/swagger.json", "PlaceInfo Services"));
            
        }
        private static void AdicionarDadosTeste(ConexaContext context)
        {
            var testeCity = new City()
            {
                CityName = "Sorriso",
                Temp = "9.9",
                UltimaAtualizacao = DateTime.Now
            };
            context.Citys.Add(testeCity);
            context.SaveChanges();
        }
        public void RegisterServices(IServiceCollection services)
        {
            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword"] ?? "0rUOw5M5ad";
            var database = Configuration["Database"] ?? "ConexaDB";
            services.AddDbContext<ConexaContext>(options => options.UseSqlServer(
                $"Server={server},{port};Database={database};User Id={user};Password={password};"));
            DependencyContainer.RegisterServices(services);
        }
    }
}