using Microsoft.EntityFrameworkCore.Migrations;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Migrations
{
    public partial class HolidaysTableIndexesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Holidays_CountryCode_Year_Region",
                table: "Holidays");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CountryCode_Region",
                table: "Holidays",
                columns: new[] { "CountryCode", "Region" });

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_Year",
                table: "Holidays",
                column: "Year");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Holidays_CountryCode_Region",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_Year",
                table: "Holidays");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CountryCode_Year_Region",
                table: "Holidays",
                columns: new[] { "CountryCode", "Year", "Region" },
                unique: true,
                filter: "[Region] IS NOT NULL");
        }
    }
}
