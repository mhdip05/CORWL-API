using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace NMS_API_N.Extension
{
    public static class SwaggerServiceExtention
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NMS API", Version = "v1" });
            });
            return services;
        }
    }
}
