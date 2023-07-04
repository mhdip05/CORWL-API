using Microsoft.OpenApi.Models;

namespace CORWL_API.Extension
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
