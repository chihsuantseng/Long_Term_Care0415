using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Long_Term_Care.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableExaminDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "ExaminDate",
                table: "Physical",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "healthSupplements",
                columns: table => new
                {
                    SupplementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplementName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplementDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplementEthnic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplementPrecautions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplementIdImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_healthSupplements", x => x.SupplementId);
                });

            migrationBuilder.UpdateData(
                table: "Physical",
                keyColumn: "PhysicalId",
                keyValue: 1,
                column: "ExaminDate",
                value: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "healthSupplements");

            migrationBuilder.DropColumn(
                name: "ExaminDate",
                table: "Physical");
        }
    }
}
