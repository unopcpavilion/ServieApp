﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceApp.DAL.EFContext;

namespace ServiceApp.DAL.Migrations
{
    [DbContext(typeof(ServiceContext))]
    [Migration("20200220012015_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServiceApp.DAL.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<bool>("Status");

                    b.Property<int?>("UsersUserId");

                    b.HasKey("Id");

                    b.HasIndex("UsersUserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ServiceApp.DAL.Models.Roles", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new { RoleId = 1, RoleName = "admin" },
                        new { RoleId = 2, RoleName = "user" }
                    );
                });

            modelBuilder.Entity("ServiceApp.DAL.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("SurName");

                    b.Property<string>("UserEmail");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new { UserId = 1, Name = "Admin", Password = "fEqNCco3Yq9h5ZUglD3CZJT4lBs=", UserEmail = "admin@mail.ru" }
                    );
                });

            modelBuilder.Entity("ServiceApp.DAL.Models.UsersRoles", b =>
                {
                    b.Property<int>("UsersId");

                    b.Property<int>("RolesId");

                    b.HasKey("UsersId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("UsersRoles");

                    b.HasData(
                        new { UsersId = 1, RolesId = 1 },
                        new { UsersId = 1, RolesId = 2 }
                    );
                });

            modelBuilder.Entity("ServiceApp.DAL.Models.Products", b =>
                {
                    b.HasOne("ServiceApp.DAL.Models.Users")
                        .WithMany("Products")
                        .HasForeignKey("UsersUserId");
                });

            modelBuilder.Entity("ServiceApp.DAL.Models.UsersRoles", b =>
                {
                    b.HasOne("ServiceApp.DAL.Models.Roles", "Roles")
                        .WithMany("UsersRoles")
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServiceApp.DAL.Models.Users", "Users")
                        .WithMany("UsersRoles")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
