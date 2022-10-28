using Microsoft.OpenApi.Models;

namespace ManagementInventory.API
{
    /// <summary>
    /// Class to configure API
    /// </summary>
    public static class ManagementInventoryRegistration
    {
        /// <summary>
        /// Method to add all depedencies to configure api
        /// </summary>
        /// <param name="services">Collection to service to add dependencies</param>
        /// <param name="configuration">Configuration to read a configuration file(for example, appsettings.json)</param>
        /// <returns>Returns a collection of services with Swagger configured</returns>
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerConfig();
            return services;
        }

        /// <summary>
        /// Method to register Swagger
        /// </summary>
        /// <param name="services">Collection to service to add dependencies</param>
        /// <returns>Returns a collection of services with Swagger configured</returns>
        private static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            //Register and configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.OrderActionsBy((apiDesc) => $"{apiDesc.RelativePath}_{apiDesc.HttpMethod}");
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ManagementInventory API", Version = "v1" });

                c.CustomSchemaIds(x => x.FullName);
            });

            return services;
        }
    }
}