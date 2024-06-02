using labOpp.Context;
using labOpp.Model;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace labOpp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ConferenceDbContext>(options =>
               options.UseNpgsql(_configuration.GetConnectionString("ConnectionString")));

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddDbContext<ConferenceDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("ConnectionString"),
                        assembly =>
                            assembly.MigrationsAssembly("CollectingApplicationsAPI"));
            });


            services.AddScoped<IApplicationProvider, ApplicationsProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Social CRM API v1");
                x.RoutePrefix = "swagger";
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
