using FluentValidation;
using ManagementInventory.Application.Behaviours;
using ManagementInventory.Application.Models.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ManagementInventory.Application
{
    /// <summary>
    /// Class to register all dependecies to application layer
    /// </summary>
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// Method to add all depedencies to application layer
        /// </summary>
        /// <param name="services">Collection to service to add dependencies</param>
        /// <param name="configuration">Configuration to read a configuration file(for example, appsettings.json)</param>
        /// <returns>Returns a collection of services with all dependencies injected</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var inventory = configuration.GetSection("ManagementInventoryApiSettings").Get<ManagementInventorySettings>();
            services.AddSingleton(inventory);
            services.AddHttpClient();
            services.AddHttpClient("ManagementInventory", c =>
            {
                c.BaseAddress = new Uri(inventory.Url);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddHttpContextAccessor();
            services.AddHttpClient();

            return services;
        }
    }
}