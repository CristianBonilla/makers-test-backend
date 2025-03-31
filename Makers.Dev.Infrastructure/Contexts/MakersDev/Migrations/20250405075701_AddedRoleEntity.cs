using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Makers.Dev.Infrastructure.Contexts.MakersDev.Migrations;

/// <inheritdoc />
public partial class AddedRoleEntity : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.AddColumn<Guid>(
        name: "RoleId",
        schema: "dbo",
        table: "User",
        type: "uuid",
        nullable: false,
        defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

    migrationBuilder.CreateTable(
        name: "Role",
        schema: "dbo",
        columns: table => new
        {
          RoleId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
          Name = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
          DisplayName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
          Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
          xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
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

    migrationBuilder.UpdateData(
        schema: "dbo",
        table: "User",
        keyColumn: "UserId",
        keyValue: new Guid("c880a1fd-2c32-46cb-b744-a6fad6175a53"),
        column: "RoleId",
        value: new Guid("84b6844f-60a9-4336-a44d-4f23ba5fd12a"));

    migrationBuilder.CreateIndex(
        name: "IX_User_RoleId",
        schema: "dbo",
        table: "User",
        column: "RoleId");

    migrationBuilder.CreateIndex(
        name: "IX_Role_Name_DisplayName",
        schema: "dbo",
        table: "Role",
        columns: new[] { "Name", "DisplayName" },
        unique: true);

    migrationBuilder.AddForeignKey(
        name: "FK_User_Role_RoleId",
        schema: "dbo",
        table: "User",
        column: "RoleId",
        principalSchema: "dbo",
        principalTable: "Role",
        principalColumn: "RoleId",
        onDelete: ReferentialAction.Cascade);
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropForeignKey(
        name: "FK_User_Role_RoleId",
        schema: "dbo",
        table: "User");

    migrationBuilder.DropTable(
        name: "Role",
        schema: "dbo");

    migrationBuilder.DropIndex(
        name: "IX_User_RoleId",
        schema: "dbo",
        table: "User");

    migrationBuilder.DropColumn(
        name: "RoleId",
        schema: "dbo",
        table: "User");
  }
}
