using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoteAPI.Application.DTOs.Mapping;
using VoteAPI.Application.Interfaces;
using VoteAPI.Application.Services;
using VoteAPI.Domain.Interfaces;
using VoteAPI.Domain.Interfaces.Rabbit;
using VoteAPI.Infra.Data.Context;
using VoteAPI.Infra.Data.Repositories;
using VoteAPI.Infra.Data.Repositories.Rabbit;

namespace VoteAPI.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE") ?? configuration.GetConnectionString("DefaultConnection");
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            service.AddAutoMapper(typeof(MappingProfile));

            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IRabbitRepository, RabbitRepository>();

            service.AddScoped<IScheduleService, ScheduleService>();
            service.AddScoped<IVoteService, VoteService>();
            service.AddHttpClient<IExternalAPIService, ExternalAPIService>();
            
            return service;
        }
    }
}