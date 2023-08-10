using CORWL_API.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace CORWL_API.Extension
{
    public static class ApiVersioningExtension
    {
#nullable disable
        public static IServiceCollection AddApiVersioningServices(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version")
                  //new HeaderApiVersionReader("x-version")
                  //new MediaTypeApiVersionReader("ver")
                  //consolo.log(test)
                  //consolo.log(test)
                  //consolo.log(test)
                  //consolo.log(test)
                );

            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
