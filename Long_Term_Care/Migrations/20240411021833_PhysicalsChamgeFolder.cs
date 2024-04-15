using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Long_Term_Care.Migrations
{
    /// <inheritdoc />
    public partial class PhysicalsChamgeFolder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplementIdImage",
                table: "healthSupplements");

            migrationBuilder.AddColumn<byte[]>(
                name: "SupplementImg",
                table: "healthSupplements",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SupplementLongTermPrice10",
                table: "healthSupplements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SupplementMidTermPrice5",
                table: "healthSupplements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SupplementPrice",
                table: "healthSupplements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplementImg",
                table: "healthSupplements");

            migrationBuilder.DropColumn(
                name: "SupplementLongTermPrice10",
                table: "healthSupplements");

            migrationBuilder.DropColumn(
                name: "SupplementMidTermPrice5",
                table: "healthSupplements");

            migrationBuilder.DropColumn(
                name: "SupplementPrice",
                table: "healthSupplements");

            migrationBuilder.AddColumn<string>(
                name: "SupplementIdImage",
                table: "healthSupplements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
