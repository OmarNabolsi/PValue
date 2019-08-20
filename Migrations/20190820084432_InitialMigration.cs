using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PValue.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Indicators_12",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameReference = table.Column<string>(nullable: true),
                    DataSource = table.Column<string>(nullable: true),
                    OppeCycleID = table.Column<int>(nullable: false),
                    OppeIndicatorID = table.Column<int>(nullable: false),
                    OppePhysicianSubGroupID = table.Column<int>(nullable: false),
                    PayrollID = table.Column<string>(nullable: true),
                    AUBNetID = table.Column<string>(nullable: true),
                    CSNID = table.Column<string>(nullable: true),
                    PatientMRN = table.Column<string>(nullable: true),
                    IndicatorDate = table.Column<DateTime>(nullable: false),
                    NumeratorValue = table.Column<double>(nullable: false),
                    DenominatorValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators_12", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "P_Table",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OppeCycleID = table.Column<int>(nullable: false),
                    OppeIndicatorID = table.Column<int>(nullable: false),
                    OppePhysicianSubGroupID = table.Column<int>(nullable: false),
                    PhysicianID = table.Column<string>(nullable: true),
                    Numerator = table.Column<double>(nullable: false),
                    Denominator = table.Column<double>(nullable: false),
                    Mean = table.Column<double>(nullable: false),
                    PeerNumerator = table.Column<double>(nullable: false),
                    PeerDenominator = table.Column<double>(nullable: false),
                    PeerMean = table.Column<double>(nullable: false),
                    P_Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P_Table", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Indicators_12");

            migrationBuilder.DropTable(
                name: "P_Table");
        }
    }
}
