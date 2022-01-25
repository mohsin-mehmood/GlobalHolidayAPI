using Microsoft.EntityFrameworkCore.Migrations;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Migrations
{
    public partial class HoildayRegionOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Holidays_CountryCode_Year_Region",
                table: "Holidays");

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "Holidays",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CountryCode_Year_Region",
                table: "Holidays",
                columns: new[] { "CountryCode", "Year", "Region" },
                unique: true,
                filter: "[Region] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Holidays_CountryCode_Year_Region",
                table: "Holidays");

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "Holidays",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CountryCode_Year_Region",
                table: "Holidays",
                columns: new[] { "CountryCode", "Year", "Region" },
                unique: true);
        }
    }
}
