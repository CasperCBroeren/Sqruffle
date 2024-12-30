using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sqruffle.Data
{
    internal class SqruffleDesignTime : IDesignTimeDbContextFactory<SqruffleDatabase>
    {
        public SqruffleDatabase CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<SqruffleDesignTime>()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SqruffleDatabase>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));

            return new SqruffleDatabase(optionsBuilder.Options);
        }
    }
}
