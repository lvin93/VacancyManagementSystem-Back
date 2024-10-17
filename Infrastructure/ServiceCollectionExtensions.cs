using Application;
using Domain.DomainServices;
using FluentValidation;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<VacancyManagementDbContext>(options =>
              {
                   options.UseSqlServer(connectionString);
              });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileUpload, FileUploadService>();

            return services;
        }
    }
}