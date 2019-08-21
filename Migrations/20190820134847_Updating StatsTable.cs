using Microsoft.EntityFrameworkCore.Migrations;

namespace PValue.Migrations
{
    public partial class UpdatingStatsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OppeCycleID",
                table: "StatsTable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OppeIndicatorID",
                table: "StatsTable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OppePhysicianSubGroupID",
                table: "StatsTable",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OppeCycleID",
                table: "StatsTable");

            migrationBuilder.DropColumn(
                name: "OppeIndicatorID",
                table: "StatsTable");

            migrationBuilder.DropColumn(
                name: "OppePhysicianSubGroupID",
                table: "StatsTable");
        }
    }
}
