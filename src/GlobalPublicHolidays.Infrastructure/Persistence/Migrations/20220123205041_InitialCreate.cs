using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "HolidayFlags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayFlags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    CountryCode = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    RegionName = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => new { x.CountryCode, x.RegionName });
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryHolidayType",
                columns: table => new
                {
                    CountriesCode = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    HolidayTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryHolidayType", x => new { x.CountriesCode, x.HolidayTypesId });
                    table.ForeignKey(
                        name: "FK_CountryHolidayType_Countries_CountriesCode",
                        column: x => x.CountriesCode,
                        principalTable: "Countries",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryHolidayType_HolidayTypes_HolidayTypesId",
                        column: x => x.HolidayTypesId,
                        principalTable: "HolidayTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObservedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HolidayTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holidays_HolidayTypes_HolidayTypeId",
                        column: x => x.HolidayTypeId,
                        principalTable: "HolidayTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HolidayHolidayFlag",
                columns: table => new
                {
                    FlagsId = table.Column<int>(type: "int", nullable: false),
                    HolidaysId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayHolidayFlag", x => new { x.FlagsId, x.HolidaysId });
                    table.ForeignKey(
                        name: "FK_HolidayHolidayFlag_HolidayFlags_FlagsId",
                        column: x => x.FlagsId,
                        principalTable: "HolidayFlags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HolidayHolidayFlag_Holidays_HolidaysId",
                        column: x => x.HolidaysId,
                        principalTable: "Holidays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HolidayName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HolidayId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayName_Holidays_HolidayId",
                        column: x => x.HolidayId,
                        principalTable: "Holidays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HolidayNote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HolidayId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayNote_Holidays_HolidayId",
                        column: x => x.HolidayId,
                        principalTable: "Holidays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HolidayFlags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SHOP_CLOSING_DAY" },
                    { 2, "REGIONAL_HOLIDAY" },
                    { 3, "ADDITIONAL_HOLIDAY" },
                    { 4, "PART_DAY_HOLIDAY" }
                });

            migrationBuilder.InsertData(
                table: "HolidayTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "public_holiday" },
                    { 2, "observance" },
                    { 3, "school_holiday" },
                    { 4, "other_day" },
                    { 5, "extra_working_day" },
                    { 6, "postal_holiday" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryHolidayType_HolidayTypesId",
                table: "CountryHolidayType",
                column: "HolidayTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayHolidayFlag_HolidaysId",
                table: "HolidayHolidayFlag",
                column: "HolidaysId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayName_HolidayId",
                table: "HolidayName",
                column: "HolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayNote_HolidayId",
                table: "HolidayNote",
                column: "HolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CountryCode_Year_Region",
                table: "Holidays",
                columns: new[] { "CountryCode", "Year", "Region" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_HolidayTypeId",
                table: "Holidays",
                column: "HolidayTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryHolidayType");

            migrationBuilder.DropTable(
                name: "HolidayHolidayFlag");

            migrationBuilder.DropTable(
                name: "HolidayName");

            migrationBuilder.DropTable(
                name: "HolidayNote");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "HolidayFlags");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "HolidayTypes");
        }
    }
}
