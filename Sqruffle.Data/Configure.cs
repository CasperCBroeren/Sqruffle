using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sqruffle.Data
{
    public static class Configure
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<SqruffleDatabase>(opt =>
                  opt.UseNpgsql(configuration.GetConnectionString("Default")));
            services.AddTransient<ISqruffleDatabase, SqruffleDatabase>();
        }
    }
}
