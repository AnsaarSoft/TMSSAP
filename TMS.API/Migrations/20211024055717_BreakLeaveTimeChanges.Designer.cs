﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TMS.API.Database;

namespace TMS.API.Migrations
{
    [DbContext(typeof(TMSContext))]
    [Migration("20211024055717_BreakLeaveTimeChanges")]
    partial class BreakLeaveTimeChanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TMS.Models.Model.BreakTime", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("rTimeSheetID")
                        .HasColumnType("int");

                    b.Property<int?>("rUserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("rTimeSheetID");

                    b.HasIndex("rUserID");

                    b.ToTable("BreakTimes");
                });

            modelBuilder.Entity("TMS.Models.Model.LeaveTime", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("rTimeSheetID")
                        .HasColumnType("int");

                    b.Property<int?>("rUserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("rTimeSheetID");

                    b.HasIndex("rUserID");

                    b.ToTable("LeaveTimes");
                });

            modelBuilder.Entity("TMS.Models.Model.TimeSheet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DayDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("flgBreak")
                        .HasColumnType("bit");

                    b.Property<bool>("flgLeave")
                        .HasColumnType("bit");

                    b.Property<int?>("rUserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("rUserID");

                    b.ToTable("TimeSheets");
                });

            modelBuilder.Entity("TMS.Models.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LeaveHours")
                        .HasColumnType("float");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SBOId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TMS.Models.Model.BreakTime", b =>
                {
                    b.HasOne("TMS.Models.Model.TimeSheet", "rTimeSheet")
                        .WithMany()
                        .HasForeignKey("rTimeSheetID");

                    b.HasOne("TMS.Models.Model.User", "rUser")
                        .WithMany()
                        .HasForeignKey("rUserID");

                    b.Navigation("rTimeSheet");

                    b.Navigation("rUser");
                });

            modelBuilder.Entity("TMS.Models.Model.LeaveTime", b =>
                {
                    b.HasOne("TMS.Models.Model.TimeSheet", "rTimeSheet")
                        .WithMany()
                        .HasForeignKey("rTimeSheetID");

                    b.HasOne("TMS.Models.Model.User", "rUser")
                        .WithMany()
                        .HasForeignKey("rUserID");

                    b.Navigation("rTimeSheet");

                    b.Navigation("rUser");
                });

            modelBuilder.Entity("TMS.Models.Model.TimeSheet", b =>
                {
                    b.HasOne("TMS.Models.Model.User", "rUser")
                        .WithMany()
                        .HasForeignKey("rUserID");

                    b.Navigation("rUser");
                });
#pragma warning restore 612, 618
        }
    }
}