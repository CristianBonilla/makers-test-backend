﻿// <auto-generated />
using System;
using Makers.Dev.Infrastructure.Contexts.MakersDev;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Makers.Dev.Infrastructure.Contexts.MakersDev.Migrations
{
    [DbContext(typeof(MakersDevContext))]
    [Migration("20250401130547_AddedUserEntity")]
    partial class AddedUserEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Makers.Dev.Domain.Entities.Auth.RoleEntity", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("RoleId");

                    b.HasIndex("Name", "DisplayName")
                        .IsUnique();

                    b.ToTable("Role", "dbo");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("84b6844f-60a9-4336-a44d-4f23ba5fd12a"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DisplayName = "Admin",
                            Name = "AdminUser"
                        },
                        new
                        {
                            RoleId = new Guid("f9c078da-36f3-435f-9f52-666c659d2285"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DisplayName = "Common User",
                            Name = "CommonUser"
                        });
                });

            modelBuilder.Entity("Makers.Dev.Domain.Entities.Auth.UserEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("DocumentNumber", "Mobile", "Username", "Email")
                        .IsUnique();

                    b.ToTable("User", "dbo");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("c880a1fd-2c32-46cb-b744-a6fad6175a53"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DocumentNumber = "1023944678",
                            Email = "cristian10camilo95@gmail.com",
                            Firstname = "Cristian Camilo",
                            IsActive = true,
                            Lastname = "Bonilla",
                            Mobile = "+573163534451",
                            Password = "oO63zcP14ylquh+FDz/NdI3v2Zltfk2p4gmLcZ6bmmwcwCJlEMjIH95egAt/BGZiWjKVTkblXoQOuxv/OAFegw==",
                            RoleId = new Guid("84b6844f-60a9-4336-a44d-4f23ba5fd12a"),
                            Salt = new byte[] { 160, 238, 183, 205, 195, 245, 227, 41, 106, 186, 31, 133, 15, 63, 205, 116, 141, 239, 217, 153, 109, 126, 77, 169, 226, 9, 139, 113, 158, 155, 154, 108 },
                            Username = "chris__boni"
                        });
                });

            modelBuilder.Entity("Makers.Dev.Domain.Entities.Auth.UserEntity", b =>
                {
                    b.HasOne("Makers.Dev.Domain.Entities.Auth.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Makers.Dev.Domain.Entities.Auth.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
