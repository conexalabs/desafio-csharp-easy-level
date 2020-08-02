using DesafioCsharpEasy.Data;
using DesafioCsharpEasy.Models;
using DesafioCsharpEasy.Repository;
using DesafioCsharpEasy.Services.OpenWeatherMap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DesafioCsharpEasy
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
            services.AddControllers();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio C# Easy", Version = "v1" });
            });

            services.AddScoped<ICityTemperatureRepository, CityTemperatureRepository>();
            services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("InMemoryDB"));
            //services.AddDbContext<ApiContext>(opt => opt.UseSqlite("InMemoryDB"));

            var connectionString = Configuration.GetConnectionString("DB");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio C# Easy");
                c.RoutePrefix = "api/documentation";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
