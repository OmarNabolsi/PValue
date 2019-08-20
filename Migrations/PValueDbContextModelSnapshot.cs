﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PValue.Data;

namespace PValue.Migrations
{
    [DbContext(typeof(PValueDbContext))]
    partial class PValueDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PValue.Models.Indicator_12", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AUBNetID");

                    b.Property<string>("CSNID");

                    b.Property<string>("DataSource");

                    b.Property<double>("DenominatorValue");

                    b.Property<DateTime>("IndicatorDate");

                    b.Property<string>("NameReference");

                    b.Property<double>("NumeratorValue");

                    b.Property<int>("OppeCycleID");

                    b.Property<int>("OppeIndicatorID");

                    b.Property<int>("OppePhysicianSubGroupID");

                    b.Property<string>("PatientMRN");

                    b.Property<string>("PayrollID");

                    b.HasKey("ID");

                    b.ToTable("Indicators_12");
                });

            modelBuilder.Entity("PValue.Models.P_Table", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Denominator");

                    b.Property<double>("Mean");

                    b.Property<double>("Numerator");

                    b.Property<int>("OppeCycleID");

                    b.Property<int>("OppeIndicatorID");

                    b.Property<int>("OppePhysicianSubGroupID");

                    b.Property<double>("P_Value");

                    b.Property<double>("PeerDenominator");

                    b.Property<double>("PeerMean");

                    b.Property<double>("PeerNumerator");

                    b.Property<string>("PhysicianID");

                    b.HasKey("ID");

                    b.ToTable("P_Table");
                });
#pragma warning restore 612, 618
        }
    }
}
