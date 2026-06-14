using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestivaCars.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPlacementKeyToBanners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "CompanyNameLabel",
                table: "Banners",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlacementKey",
                table: "Banners",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "CompanyNameLabel",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "PlacementKey",
                table: "Banners");

        }
    }
}
