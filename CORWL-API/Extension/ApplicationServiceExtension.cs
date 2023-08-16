using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CORWL_API.DbContext;
using CORWL_API.Helper;
using CORWL_API.IServices;
using CORWL_API.Model.Entities;
using CORWL_API.Services;
using CORWL_API.Unit_Of_Work;
using CORWL_API.Model.FetchDTO;

namespace CORWL_API.Extension
{
    public static class ApplicationServiceExtension
    {
#nullable disable
        public static IServiceProvider serviceProvider;
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddSingleton<IEmailServices, MailServices>();
            services.AddSingleton<IFileServices, FileServices>();
            services.AddSingleton<IAzureBlob, AzureBlob>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<AzureBlobDto>(config.GetSection("AzureBlob"));
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddDbContextPool<DataContext>(options =>
            { 
                var conStr = config.GetConnectionString("DefaultConnection");
                options.UseNpgsql(conStr);
            });

            serviceProvider = services.BuildServiceProvider();

            return services;
        }
    }
}
