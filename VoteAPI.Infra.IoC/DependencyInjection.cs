using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoteAPI.Infra.Data.Context;

namespace VoteAPI.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE") ?? configuration.GetConnectionString("DefaultConnection");
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //service.AddAutoMapper(typeof(MappingProfile));
            
            return service;
        }
    }
}