using FitnessTrackerAPI.Data;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessTrackerAPI.ApplicationExstensions
{
    public static class ApplicationServiceExstensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Add Identity
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
                

            }).AddRoles<AppRole>()
              .AddRoleManager<RoleManager<AppRole>>()
              .AddEntityFrameworkStores<DataContext>();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // DB Connection
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ITokenService, TokenService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            

            return services;
        }
    }
}
