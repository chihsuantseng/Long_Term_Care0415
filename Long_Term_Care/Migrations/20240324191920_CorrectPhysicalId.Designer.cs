﻿// <auto-generated />
using Long_Term_Care.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Long_Term_Care.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240324191920_CorrectPhysicalId")]
    partial class CorrectPhysicalId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Long_Term_Care.Models.LongTermCare+Physicals", b =>
                {
                    b.Property<int>("PhysicalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhysicalId"));

                    b.Property<int>("ALT")
                        .HasColumnType("int");

                    b.Property<int>("AST")
                        .HasColumnType("int");

                    b.Property<int>("BUN")
                        .HasColumnType("int");

                    b.Property<int>("CHOL")
                        .HasColumnType("int");

                    b.Property<float>("CREA")
                        .HasColumnType("real");

                    b.Property<float>("Ca")
                        .HasColumnType("real");

                    b.Property<int>("DBP")
                        .HasColumnType("int");

                    b.Property<int>("Fe")
                        .HasColumnType("int");

                    b.Property<int>("GLU")
                        .HasColumnType("int");

                    b.Property<float>("HB")
                        .HasColumnType("real");

                    b.Property<float>("HCT")
                        .HasColumnType("real");

                    b.Property<int>("HDL")
                        .HasColumnType("int");

                    b.Property<float>("HbA1C")
                        .HasColumnType("real");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("LDL")
                        .HasColumnType("int");

                    b.Property<int>("PLT")
                        .HasColumnType("int");

                    b.Property<float>("RBC")
                        .HasColumnType("real");

                    b.Property<int>("SBP")
                        .HasColumnType("int");

                    b.Property<int>("TG")
                        .HasColumnType("int");

                    b.Property<int>("UIBC")
                        .HasColumnType("int");

                    b.Property<float>("WBC")
                        .HasColumnType("real");

                    b.Property<int>("Waist")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("PhysicalId");

                    b.ToTable("Physical");

                    b.HasData(
                        new
                        {
                            PhysicalId = 1,
                            ALT = 24,
                            AST = 26,
                            BUN = 21,
                            CHOL = 120,
                            CREA = 0.8f,
                            Ca = 2.2f,
                            DBP = 77,
                            Fe = 201,
                            GLU = 90,
                            HB = 12.7f,
                            HCT = 48.1f,
                            HDL = 100,
                            HbA1C = 3.9f,
                            Height = 176,
                            LDL = 230,
                            PLT = 267,
                            RBC = 4.6f,
                            SBP = 123,
                            TG = 105,
                            UIBC = 223,
                            WBC = 10.32f,
                            Waist = 78,
                            Weight = 70
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
