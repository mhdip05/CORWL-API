using CORWL_API.Helper;
using Microsoft.OpenApi.Models;

namespace CORWL_API.Extension
{
    public static class SwaggerServiceExtention
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CORWL-API-V1",
                    Description = "CORWL-API-V1"
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "CORWL-API-V2",
                    Description = "CORWL-API-V2"
                });
                
                c.ResolveConflictingActions(a => a.First());
                c.OperationFilter<RemoveVersionFromParamter>();
                c.OperationFilter<RemoveApiVersionFromParamter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();


            });
            return services;
        }
    }
}
