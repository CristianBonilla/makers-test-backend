using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Makers.Dev.Infrastructure.Contexts.MakersDev.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.EnsureSchema(
        name: "dbo");

    migrationBuilder.CreateTable(
        name: "Role",
        schema: "dbo",
        columns: table => new
        {
          RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
          Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
          DisplayName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
          Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
          Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Role", x => x.RoleId);
        });

    migrationBuilder.InsertData(
        schema: "dbo",
        table: "Role",
        columns: new[] { "RoleId", "DisplayName", "Name" },
        values: new object[,]
        {
                  { new Guid("84b6844f-60a9-4336-a44d-4f23ba5fd12a"), "Admin", "AdminUser" },
                  { new Guid("f9c078da-36f3-435f-9f52-666c659d2285"), "Common User", "CommonUser" }
        });

    migrationBuilder.CreateIndex(
        name: "IX_Role_Name_DisplayName",
        schema: "dbo",
        table: "Role",
        columns: new[] { "Name", "DisplayName" },
        unique: true);
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "Role",
        schema: "dbo");
  }
}
