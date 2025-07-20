using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTrackerAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class moveWeeklyWorkoutStreakToWeeklyProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeeklyWorkoutStreak",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "WeeklyWorkoutStreak",
                table: "WeeklyProgress",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeeklyWorkoutStreak",
                table: "WeeklyProgress");

            migrationBuilder.AddColumn<int>(
                name: "WeeklyWorkoutStreak",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
