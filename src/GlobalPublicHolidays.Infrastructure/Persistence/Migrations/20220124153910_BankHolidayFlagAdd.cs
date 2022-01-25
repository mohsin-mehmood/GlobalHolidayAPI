using Microsoft.EntityFrameworkCore.Migrations;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Migrations
{
    public partial class BankHolidayFlagAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HolidayFlags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "BANK_HOLIDAY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HolidayFlags",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
