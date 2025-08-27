using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertyInfo.API.Migrations
{
    /// <inheritdoc />
    public partial class PropertyInfoUpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    IdOwner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.IdOwner);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    IdProperty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CodeInternal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdOwner = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.IdProperty);
                    table.ForeignKey(
                        name: "FK_Properties_Owners_IdOwner",
                        column: x => x.IdOwner,
                        principalTable: "Owners",
                        principalColumn: "IdOwner",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    IdPropertyImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    IdProperty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.IdPropertyImage);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_IdProperty",
                        column: x => x.IdProperty,
                        principalTable: "Properties",
                        principalColumn: "IdProperty",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTraces",
                columns: table => new
                {
                    IdPropertyTrace = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdProperty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTraces", x => x.IdPropertyTrace);
                    table.ForeignKey(
                        name: "FK_PropertyTraces_Properties_IdProperty",
                        column: x => x.IdProperty,
                        principalTable: "Properties",
                        principalColumn: "IdProperty",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "IdOwner", "Address", "Birthday", "Name", "Photo" },
                values: new object[,]
                {
                    { 1, "address number one", new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name owner first", "files/photos/owner1.pdf" },
                    { 2, "address number two", new DateTime(2000, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name owner second", "files/photos/owner2.pdf" },
                    { 3, "address number three", new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name owner third", "files/photos/owner3.pdf" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "IdProperty", "Address", "CodeInternal", "IdOwner", "Name", "Price", "Year" },
                values: new object[,]
                {
                    { 1, "address property number one", "0101", 1, "Property number one", 1500000m, new DateTime(2005, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "address property number two", "0101", 2, "Property number two", 1500000m, new DateTime(2005, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "address property number three", "0101", 3, "Property number three", 1500000m, new DateTime(2005, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_IdOwner",
                table: "Properties",
                column: "IdOwner");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_IdProperty",
                table: "PropertyImages",
                column: "IdProperty");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTraces_IdProperty",
                table: "PropertyTraces",
                column: "IdProperty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "PropertyTraces");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
