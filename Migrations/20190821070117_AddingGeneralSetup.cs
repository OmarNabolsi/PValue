using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PValue.Migrations
{
    public partial class AddingGeneralSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HypothesisTests",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HypothesisTests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorsData",
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
                    table.PrimaryKey("PK_IndicatorsData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorsInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OppeIndicatorID = table.Column<int>(nullable: false),
                    HypothesisTest = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorsInfo", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HypothesisTests");

            migrationBuilder.DropTable(
                name: "IndicatorsData");

            migrationBuilder.DropTable(
                name: "IndicatorsInfo");
        }
    }
}
