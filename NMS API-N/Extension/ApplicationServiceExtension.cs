using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Helper;
using NMS_API_N.IServices;
using NMS_API_N.Services;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Extension
{
    public static class ApplicationServiceExtension
    {
#nullable disable
        public static IServiceProvider serviceProvider;
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddSingleton<IEmailServices, MailServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddDbContextPool<DataContext>(options =>
            {
                var conStr = config.GetConnectionString("DefaultConnection");

                options.UseSqlServer(conStr);
            });

            serviceProvider = services.BuildServiceProvider();

            return services;
        }
    }
}
