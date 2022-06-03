using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asset_Tracking_Databased.Migrations
{
    public partial class Examples : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficePlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UKPrice = table.Column<float>(type: "real", nullable: false),
                    OfficeID = table.Column<int>(type: "int", nullable: false),
                    ProductTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Offices_OfficeID",
                        column: x => x.OfficeID,
                        principalTable: "Offices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "ID", "Currency", "OfficePlace" },
                values: new object[,]
                {
                    { 1, "EUR", "Spain" },
                    { 2, "SEK", "Sweden" },
                    { 3, "GBP", "United Kingdom" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ID", "TypeName" },
                values: new object[,]
                {
                    { 1, "Laptops" },
                    { 2, "Mobiles" },
                    { 3, "Computers" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Brand", "Date", "Model", "OfficeID", "ProductTypeID", "UKPrice" },
                values: new object[,]
                {
                    { 1, "Acer", "2010-05-29", "Liquid Z900", 2, 2, 120f },
                    { 2, "Acer", "2020-12-18", "Nitro 5", 3, 1, 855f },
                    { 3, "Alienware", "2022-06-03", "AURORA RYZEN R10", 1, 3, 1839f },
                    { 4, "HP", "2017-1-1", "Pavilion TP01", 1, 3, 1100f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OfficeID",
                table: "Products",
                column: "OfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeID",
                table: "Products",
                column: "ProductTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
