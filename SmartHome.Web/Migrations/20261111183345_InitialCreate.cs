using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartHome.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PowerWatts = table.Column<double>(type: "float", nullable: false),
                    InstalledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsOn = table.Column<bool>(type: "bit", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "Лампи, LED ленти и ключове.", "bi-lightbulb", "Осветление" },
                    { 2, "Термостати, климатици и сензори за температура.", "bi-thermometer-half", "Климат" },
                    { 3, "Камери, брави и датчици за движение.", "bi-shield-lock", "Сигурност" },
                    { 4, "Умни контакти и релета.", "bi-plug", "Контакти" },
                    { 5, "Телевизори и тонколони.", "bi-tv", "Мултимедия" },
                    { 6, "Хладилници, перални и други уреди.", "bi-gear-wide-connected", "Битова техника" }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Country", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "Нидерландия", "Philips Hue", "https://www.philips-hue.com" },
                    { 2, "Китай", "Xiaomi", "https://www.mi.com" },
                    { 3, "България", "Shelly", "https://www.shelly.com" },
                    { 4, "САЩ", "Google Nest", "https://store.google.com" },
                    { 5, "Южна Корея", "Samsung", "https://www.samsung.com" },
                    { 6, "Китай", "TP-Link", "https://www.tp-link.com" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "Floor", "Name" },
                values: new object[,]
                {
                    { 1, "Основното помещение за почивка и гости.", 1, "Хол" },
                    { 2, "Кухня с трапезария.", 1, "Кухня" },
                    { 3, "Голямата спалня.", 2, "Спалня" },
                    { 4, null, 2, "Детска стая" },
                    { 5, "Баня с тоалетна.", 2, "Баня" },
                    { 6, "Гараж и вход към двора.", 0, "Гараж" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "CategoryId", "InstalledOn", "IsOn", "ManufacturerId", "Name", "Notes", "PowerWatts", "RoomId", "SerialNumber" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "Лампа над дивана", "Сменя цвета според времето на деня.", 9.5, 1, "PH-HUE-1001" },
                    { 2, 5, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5, "Смарт телевизор", null, 120.0, 1, "SS-TV-55Q80" },
                    { 3, 2, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, "Термостат", "Поддържа 22 °C през деня.", 3.0, 1, "GN-TH-3010" },
                    { 4, 4, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, "Контакт за кафемашина", "Включва се автоматично в 7:00.", 1200.0, 2, "SH-PLUG-2201" },
                    { 5, 1, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, "LED лента под шкафовете", null, 14.0, 2, "XI-LED-7781" },
                    { 6, 6, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5, "Хладилник с Wi-Fi", null, 150.0, 2, "SS-RF-9020" },
                    { 7, 1, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "Нощна лампа", null, 6.0, 3, "PH-HUE-1002" },
                    { 8, 2, new DateTime(2023, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, "Климатик", "Профилактика веднъж годишно.", 950.0, 3, "XI-AC-1250" },
                    { 9, 3, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6, "Бебефон с камера", null, 4.5, 4, "TP-CAM-C200" },
                    { 10, 4, new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, "Нагревател за кърпи", null, 400.0, 5, "SH-RELAY-1PM" },
                    { 11, 3, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, "Камера за входа", "Записва при движение.", 6.5, 6, "GN-CAM-OUT1" },
                    { 12, 3, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, "Контролер за гаражна врата", null, 5.0, 6, "SH-GATE-0042" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_CategoryId",
                table: "Devices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ManufacturerId",
                table: "Devices",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_RoomId",
                table: "Devices",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SerialNumber",
                table: "Devices",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_Name",
                table: "Manufacturers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name",
                table: "Rooms",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
