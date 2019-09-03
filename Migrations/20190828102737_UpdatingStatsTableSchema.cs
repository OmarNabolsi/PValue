using Microsoft.EntityFrameworkCore.Migrations;

namespace PValue.Migrations
{
    public partial class UpdatingStatsTableSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DenominatorSum",
                table: "StatsTable");

            migrationBuilder.RenameColumn(
                name: "PeerNumeratorSum",
                table: "StatsTable",
                newName: "Sum");

            migrationBuilder.RenameColumn(
                name: "PeerDenominatorSum",
                table: "StatsTable",
                newName: "PeerSum");

            migrationBuilder.RenameColumn(
                name: "NumeratorSum",
                table: "StatsTable",
                newName: "Alpha");

            migrationBuilder.AlterColumn<double>(
                name: "PeerCount",
                table: "StatsTable",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "Count",
                table: "StatsTable",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sum",
                table: "StatsTable",
                newName: "PeerNumeratorSum");

            migrationBuilder.RenameColumn(
                name: "PeerSum",
                table: "StatsTable",
                newName: "PeerDenominatorSum");

            migrationBuilder.RenameColumn(
                name: "Alpha",
                table: "StatsTable",
                newName: "NumeratorSum");

            migrationBuilder.AlterColumn<int>(
                name: "PeerCount",
                table: "StatsTable",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "StatsTable",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<double>(
                name: "DenominatorSum",
                table: "StatsTable",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
