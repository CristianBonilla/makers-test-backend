using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Makers.Dev.Infrastructure.Contexts.MakersDev.Migrations;

/// <inheritdoc />
public partial class AddedUserEntity : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "User",
        schema: "dbo",
        columns: table => new
        {
          UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
          RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          DocumentNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
          Mobile = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
          Username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
          Password = table.Column<string>(type: "varchar(max)", nullable: false),
          Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
          Firstname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
          Lastname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
          IsActive = table.Column<bool>(type: "bit", nullable: false),
          Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
          Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
          Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_User", x => x.UserId);
          table.ForeignKey(
                    name: "FK_User_Role_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "dbo",
                    principalTable: "Role",
                    principalColumn: "RoleId",
                    onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.InsertData(
        schema: "dbo",
        table: "User",
        columns: new[] { "UserId", "DocumentNumber", "Email", "Firstname", "IsActive", "Lastname", "Mobile", "Password", "RoleId", "Salt", "Username" },
        values: new object[] { new Guid("c880a1fd-2c32-46cb-b744-a6fad6175a53"), "1023944678", "cristian10camilo95@gmail.com", "Cristian Camilo", true, "Bonilla", "+573163534451", "oO63zcP14ylquh+FDz/NdI3v2Zltfk2p4gmLcZ6bmmwcwCJlEMjIH95egAt/BGZiWjKVTkblXoQOuxv/OAFegw==", new Guid("84b6844f-60a9-4336-a44d-4f23ba5fd12a"), new byte[] { 160, 238, 183, 205, 195, 245, 227, 41, 106, 186, 31, 133, 15, 63, 205, 116, 141, 239, 217, 153, 109, 126, 77, 169, 226, 9, 139, 113, 158, 155, 154, 108 }, "chris__boni" });

    migrationBuilder.CreateIndex(
        name: "IX_User_DocumentNumber_Mobile_Username_Email",
        schema: "dbo",
        table: "User",
        columns: new[] { "DocumentNumber", "Mobile", "Username", "Email" },
        unique: true);

    migrationBuilder.CreateIndex(
        name: "IX_User_RoleId",
        schema: "dbo",
        table: "User",
        column: "RoleId");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "User",
        schema: "dbo");
  }
}
