using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Makers.Dev.Infrastructure.Contexts.MakersDev.Migrations;

/// <inheritdoc />
public partial class AddedBankLoanEntity : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "BankLoan",
        schema: "dbo",
        columns: table => new
        {
          BankLoanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
          UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Status = table.Column<string>(type: "nvarchar(450)", nullable: false),
          Amount = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
          PaymentTerm = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
          Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
          Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_BankLoan", x => x.BankLoanId);
          table.ForeignKey(
                    name: "FK_BankLoan_User_UserId",
                    column: x => x.UserId,
                    principalSchema: "dbo",
                    principalTable: "User",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.InsertData(
        schema: "dbo",
        table: "BankLoan",
        columns: new[] { "BankLoanId", "Amount", "PaymentTerm", "Status", "UserId" },
        values: new object[,]
        {
                  { new Guid("2a08545b-cf87-4501-ae6d-cc389ad3d6a0"), 1900000m, new DateTimeOffset(new DateTime(2025, 9, 19, 20, 11, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Approved", new Guid("c880a1fd-2c32-46cb-b744-a6fad6175a53") },
                  { new Guid("c51322bd-4ae7-48dc-a1d4-55b8f3660f6b"), 70800000m, new DateTimeOffset(new DateTime(2025, 6, 22, 15, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Pending", new Guid("c880a1fd-2c32-46cb-b744-a6fad6175a53") },
                  { new Guid("d4aca576-9a20-4ce4-b349-fc528fb76ce7"), 120500000m, new DateTimeOffset(new DateTime(2027, 2, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Rejected", new Guid("c880a1fd-2c32-46cb-b744-a6fad6175a53") }
        });

    migrationBuilder.CreateIndex(
        name: "IX_BankLoan_Status_Amount",
        schema: "dbo",
        table: "BankLoan",
        columns: new[] { "Status", "Amount" });

    migrationBuilder.CreateIndex(
        name: "IX_BankLoan_UserId",
        schema: "dbo",
        table: "BankLoan",
        column: "UserId");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "BankLoan",
        schema: "dbo");
  }
}
