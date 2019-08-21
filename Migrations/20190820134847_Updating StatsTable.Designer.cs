﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PValue.Data;

namespace PValue.Migrations
{
    [DbContext(typeof(PValueDbContext))]
    [Migration("20190820134847_Updating StatsTable")]
    partial class UpdatingStatsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.ToTable("Indicator_12");
                });

            modelBuilder.Entity("PValue.Models.StatsTable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<double>("DenominatorSum");

                    b.Property<double>("LevenesTest");

                    b.Property<double>("Mean");

                    b.Property<double>("NumeratorSum");

                    b.Property<int>("OppeCycleID");

                    b.Property<int>("OppeIndicatorID");

                    b.Property<int>("OppePhysicianSubGroupID");

                    b.Property<double>("PValue");

                    b.Property<double>("PValue_EqualVariances");

                    b.Property<double>("PValue_UnequalVariances");

                    b.Property<string>("PayrollID");

                    b.Property<int>("PeerCount");

                    b.Property<double>("PeerDenominatorSum");

                    b.Property<double>("PeerMean");

                    b.Property<double>("PeerNumeratorSum");

                    b.Property<double>("PeerStandardDeviation");

                    b.Property<double>("StandardDeviation");

                    b.HasKey("ID");

                    b.ToTable("StatsTable");
                });
#pragma warning restore 612, 618
        }
    }
}