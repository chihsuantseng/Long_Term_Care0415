using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Long_Term_Care.Migrations
{
    /// <inheritdoc />
    public partial class CorrectPhysicalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Physical",
                keyColumn: "PhysicalId",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "Physical",
                columns: new[] { "PhysicalId", "ALT", "AST", "BUN", "CHOL", "CREA", "Ca", "DBP", "Fe", "GLU", "HB", "HCT", "HDL", "HbA1C", "Height", "LDL", "PLT", "RBC", "SBP", "TG", "UIBC", "WBC", "Waist", "Weight" },
                values: new object[] { 1, 24, 26, 21, 120, 0.8f, 2.2f, 77, 201, 90, 12.7f, 48.1f, 100, 3.9f, 176, 230, 267, 4.6f, 123, 105, 223, 10.32f, 78, 70 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Physical",
                keyColumn: "PhysicalId",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Physical",
                columns: new[] { "PhysicalId", "ALT", "AST", "BUN", "CHOL", "CREA", "Ca", "DBP", "Fe", "GLU", "HB", "HCT", "HDL", "HbA1C", "Height", "LDL", "PLT", "RBC", "SBP", "TG", "UIBC", "WBC", "Waist", "Weight" },
                values: new object[] { 6, 24, 26, 21, 120, 0.8f, 2.2f, 77, 201, 90, 12.7f, 48.1f, 100, 3.9f, 176, 230, 267, 4.6f, 123, 105, 223, 10.32f, 78, 70 });
        }
    }
}
