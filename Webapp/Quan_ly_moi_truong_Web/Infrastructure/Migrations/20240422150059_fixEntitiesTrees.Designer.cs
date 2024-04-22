﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(WebDbContext))]
    [Migration("20240422150059_fixEntitiesTrees")]
    partial class fixEntitiesTrees
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Deparment.Departments", b =>
                {
                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("AdminCreated")
                        .HasColumnType("bit");

                    b.Property<string>("DepartmentEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("DirectMembersCount")
                        .HasColumnType("bigint");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments", (string)null);

                    b.HasData(
                        new
                        {
                            DepartmentId = "01egqt2p26jkcil",
                            AdminCreated = true,
                            DepartmentEmail = "dev@gmail.com",
                            DepartmentName = "Quan ly cay xanh",
                            Description = "string",
                            DirectMembersCount = 3L
                        });
                });

            modelBuilder.Entity("Domain.Entities.HubConnection.HubConnections", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HubConnections");
                });

            modelBuilder.Entity("Domain.Entities.Notification.Notifications", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NotificationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Sender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Domain.Entities.Report.Reports", b =>
                {
                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ActualResolutionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpectedResolutionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssueLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerGmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ReportImpact")
                        .HasColumnType("int");

                    b.Property<string>("ResponseId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ReportId");

                    b.ToTable("Reports", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Role.Roles", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("abccde85-c7dc-4f78-9e4e-b1b3e7abee84"),
                            RoleName = "Manager"
                        },
                        new
                        {
                            RoleId = new Guid("cacd4b3a-8afe-43e9-b757-f57f5c61f8d8"),
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = new Guid("8977ef77-e554-4ef3-8353-3e01161f84d0"),
                            RoleName = "Employee"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Tree.Trees", b =>
                {
                    b.Property<Guid>("TreeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("BodyDiameter")
                        .HasColumnType("real");

                    b.Property<DateTime>("CutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IntervalCutTime")
                        .HasColumnType("int");

                    b.Property<float>("LeafLength")
                        .HasColumnType("real");

                    b.Property<string>("Note")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<DateTime>("PlantTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("TreeCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TreeLocation")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("TreeTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isCut")
                        .HasColumnType("bit");

                    b.HasKey("TreeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("TreeTypeId");

                    b.HasIndex("UsersId");

                    b.ToTable("Trees", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TreeType.TreeTypes", b =>
                {
                    b.Property<Guid>("TreeTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TreeTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TreeTypeId");

                    b.ToTable("TreeTypes", (string)null);

                    b.HasData(
                        new
                        {
                            TreeTypeId = new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"),
                            TreeTypeName = "Cay than go"
                        });
                });

            modelBuilder.Entity("Domain.Entities.User.Users", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.UserRefreshToken.UserRefreshTokens", b =>
                {
                    b.Property<Guid>("UserRefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("Expire")
                        .HasColumnType("bigint");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserRefreshTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRefreshTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Tree.Trees", b =>
                {
                    b.HasOne("Domain.Entities.Deparment.Departments", "department")
                        .WithMany("Trees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.TreeType.TreeTypes", "TreeType")
                        .WithMany("trees")
                        .HasForeignKey("TreeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User.Users", null)
                        .WithMany("trees")
                        .HasForeignKey("UsersId");

                    b.Navigation("TreeType");

                    b.Navigation("department");
                });

            modelBuilder.Entity("Domain.Entities.User.Users", b =>
                {
                    b.HasOne("Domain.Entities.Deparment.Departments", "Departments")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Role.Roles", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departments");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.UserRefreshToken.UserRefreshTokens", b =>
                {
                    b.HasOne("Domain.Entities.User.Users", "User")
                        .WithMany("UserRefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Deparment.Departments", b =>
                {
                    b.Navigation("Trees");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Entities.Role.Roles", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Entities.TreeType.TreeTypes", b =>
                {
                    b.Navigation("trees");
                });

            modelBuilder.Entity("Domain.Entities.User.Users", b =>
                {
                    b.Navigation("UserRefreshTokens");

                    b.Navigation("trees");
                });
#pragma warning restore 612, 618
        }
    }
}
