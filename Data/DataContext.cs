using FitnessTrackerAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessTrackerAPI.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User, AppRole, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
        IdentityUserToken<int>>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            builder.Entity<Workout>()
                .HasOne(w => w.RunLog)
                .WithOne(r => r.Workout)
                .HasForeignKey<RunLog>(l => l.WorkoutId);

            builder.Entity<WeightliftingLog>()
                .HasOne(w => w.Workout)
                .WithOne()
                .HasForeignKey<WeightliftingLog>(w => w.WorkoutId);

            builder.Entity<Exercise>()
                .HasOne(e => e.WeightliftingLog)
                .WithMany(wl => wl.Exercises)
                .HasForeignKey(e => e.WeightliftingLogId);
        }
    }
}
