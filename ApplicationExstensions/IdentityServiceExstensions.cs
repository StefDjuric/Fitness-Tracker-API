using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FitnessTrackerAPI.ApplicationExstensions
{
    
    public static class IdentityServiceExstensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               var tokenkey = config["TokenKey"] ?? throw new Exception("No token key found.");

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
           });

            services.AddAuthorizationBuilder()
                .AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"))
                .AddPolicy("RequireCoachRole", policy => policy.RequireRole("Coach"));

            return services;
        }
       
    }
}
