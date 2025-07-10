
using FitnessTrackerAPI.ApplicationExstensions;
using FitnessTrackerAPI.Middleware;

namespace FitnessTrackerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAplicationServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseCors(options =>
            {
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.AllowCredentials();
                options.WithOrigins("https://localhost:4200", "http://localhost:4200");
            });
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            //using var scope = app.Services.CreateScope();
            //var services = scope.ServiceProvider;

            //try
            //{
            //    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
            //    var userManager = services.GetRequiredService<UserManager<User>>();

                
            //    var roles = new List<AppRole>
            //    {
            //        new() {Name="Admin"},
            //        new() {Name="Coach"},
            //        new() {Name="Member"},
            //    };

            //    foreach(var role in roles)
            //    {
            //        if (role.Name != null && !await roleManager.RoleExistsAsync(role.Name))
            //            await roleManager.CreateAsync(role);
            //    }

            //    if (await userManager.FindByEmailAsync("admin@admin.com") == null)
            //    {
            //        var admin = new User
            //        {
            //            Email = "admin@admin.com",
            //            NormalizedEmail = "admin@admin.com".ToUpper(),
            //            UserName = "admin",
            //            NormalizedUserName = "admin".ToUpper(),
            //            FullName = "admin",
            //            EmailConfirmed = true,
            //        };

            //        var result = await userManager.CreateAsync(admin, "Pa$$w0rd");

            //        if (result.Succeeded)
            //            await userManager.AddToRolesAsync(admin, [ "Admin", "Member"]);
            //    }
                
            //}catch (Exception ex)
            //{
            //    var logger = services.GetRequiredService<ILogger<Program>>();
            //    logger.LogError(ex, "Error during seeding db");
            //}

            app.Run();
        }
    }
}
