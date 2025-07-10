using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTrackerAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWeightlifingLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_WeightliftingLog_WeightliftingLogId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_RunLog_Workout_WorkoutId",
                table: "RunLog");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightliftingLog_Workout_WorkoutId",
                table: "WeightliftingLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workout",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightliftingLog",
                table: "WeightliftingLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RunLog",
                table: "RunLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise");

            migrationBuilder.RenameTable(
                name: "Workout",
                newName: "Workouts");

            migrationBuilder.RenameTable(
                name: "WeightliftingLog",
                newName: "WeightliftingLogs");

            migrationBuilder.RenameTable(
                name: "RunLog",
                newName: "RunLogs");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_WeightliftingLog_WorkoutId",
                table: "WeightliftingLogs",
                newName: "IX_WeightliftingLogs_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_RunLog_WorkoutId",
                table: "RunLogs",
                newName: "IX_RunLogs_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercise_WeightliftingLogId",
                table: "Exercises",
                newName: "IX_Exercises_WeightliftingLogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightliftingLogs",
                table: "WeightliftingLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RunLogs",
                table: "RunLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_WeightliftingLogs_WeightliftingLogId",
                table: "Exercises",
                column: "WeightliftingLogId",
                principalTable: "WeightliftingLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RunLogs_Workouts_WorkoutId",
                table: "RunLogs",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeightliftingLogs_Workouts_WorkoutId",
                table: "WeightliftingLogs",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_WeightliftingLogs_WeightliftingLogId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_RunLogs_Workouts_WorkoutId",
                table: "RunLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightliftingLogs_Workouts_WorkoutId",
                table: "WeightliftingLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightliftingLogs",
                table: "WeightliftingLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RunLogs",
                table: "RunLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "Workouts",
                newName: "Workout");

            migrationBuilder.RenameTable(
                name: "WeightliftingLogs",
                newName: "WeightliftingLog");

            migrationBuilder.RenameTable(
                name: "RunLogs",
                newName: "RunLog");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "Exercise");

            migrationBuilder.RenameIndex(
                name: "IX_WeightliftingLogs_WorkoutId",
                table: "WeightliftingLog",
                newName: "IX_WeightliftingLog_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_RunLogs_WorkoutId",
                table: "RunLog",
                newName: "IX_RunLog_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WeightliftingLogId",
                table: "Exercise",
                newName: "IX_Exercise_WeightliftingLogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workout",
                table: "Workout",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightliftingLog",
                table: "WeightliftingLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RunLog",
                table: "RunLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_WeightliftingLog_WeightliftingLogId",
                table: "Exercise",
                column: "WeightliftingLogId",
                principalTable: "WeightliftingLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RunLog_Workout_WorkoutId",
                table: "RunLog",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeightliftingLog_Workout_WorkoutId",
                table: "WeightliftingLog",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
