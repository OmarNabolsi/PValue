using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PValue.Migrations
{
    public partial class AddingStatsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "Indicator_12",
            //     columns: table => new
            //     {
            //         ID = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //         NameReference = table.Column<string>(nullable: true),
            //         DataSource = table.Column<string>(nullable: true),
            //         OppeCycleID = table.Column<int>(nullable: false),
            //         OppeIndicatorID = table.Column<int>(nullable: false),
            //         OppePhysicianSubGroupID = table.Column<int>(nullable: false),
            //         PayrollID = table.Column<string>(nullable: true),
            //         AUBNetID = table.Column<string>(nullable: true),
            //         CSNID = table.Column<string>(nullable: true),
            //         PatientMRN = table.Column<string>(nullable: true),
            //         IndicatorDate = table.Column<DateTime>(nullable: false),
            //         NumeratorValue = table.Column<double>(nullable: false),
            //         DenominatorValue = table.Column<double>(nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Indicator_12", x => x.ID);
            //     });

            migrationBuilder.CreateTable(
                name: "StatsTable",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: false),
                    PayrollID = table.Column<string>(nullable: true),
                    NumeratorSum = table.Column<double>(nullable: false),
                    DenominatorSum = table.Column<double>(nullable: false),
                    Mean = table.Column<double>(nullable: false),
                    StandardDeviation = table.Column<double>(nullable: false),
                    PeerCount = table.Column<int>(nullable: false),
                    PeerNumeratorSum = table.Column<double>(nullable: false),
                    PeerDenominatorSum = table.Column<double>(nullable: false),
                    PeerMean = table.Column<double>(nullable: false),
                    PeerStandardDeviation = table.Column<double>(nullable: false),
                    LevenesTest = table.Column<double>(nullable: false),
                    PValue_EqualVariances = table.Column<double>(nullable: false),
                    PValue_UnequalVariances = table.Column<double>(nullable: false),
                    PValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsTable", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropTable(
            //     name: "Indicator_12");

            migrationBuilder.DropTable(
                name: "StatsTable");
        }
    }
}
