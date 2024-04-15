using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Long_Term_Care.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Physical",
                columns: table => new
                {
                    PhysicalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    SBP = table.Column<int>(type: "int", nullable: false),
                    DBP = table.Column<int>(type: "int", nullable: false),
                    Waist = table.Column<int>(type: "int", nullable: false),
                    WBC = table.Column<float>(type: "real", nullable: false),
                    RBC = table.Column<float>(type: "real", nullable: false),
                    HB = table.Column<float>(type: "real", nullable: false),
                    HCT = table.Column<float>(type: "real", nullable: false),
                    PLT = table.Column<int>(type: "int", nullable: false),
                    HDL = table.Column<int>(type: "int", nullable: false),
                    LDL = table.Column<int>(type: "int", nullable: false),
                    TG = table.Column<int>(type: "int", nullable: false),
                    CHOL = table.Column<int>(type: "int", nullable: false),
                    ALT = table.Column<int>(type: "int", nullable: false),
                    AST = table.Column<int>(type: "int", nullable: false),
                    CREA = table.Column<float>(type: "real", nullable: false),
                    BUN = table.Column<int>(type: "int", nullable: false),
                    Ca = table.Column<float>(type: "real", nullable: false),
                    HbA1C = table.Column<float>(type: "real", nullable: false),
                    GLU = table.Column<int>(type: "int", nullable: false),
                    UIBC = table.Column<int>(type: "int", nullable: false),
                    Fe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physical", x => x.PhysicalId);
                });

            migrationBuilder.InsertData(
                table: "Physical",
                columns: new[] { "PhysicalId", "ALT", "AST", "BUN", "CHOL", "CREA", "Ca", "DBP", "Fe", "GLU", "HB", "HCT", "HDL", "HbA1C", "Height", "LDL", "PLT", "RBC", "SBP", "TG", "UIBC", "WBC", "Waist", "Weight" },
                values: new object[] { 6, 24, 26, 21, 120, 0.8f, 2.2f, 77, 201, 90, 12.7f, 48.1f, 100, 3.9f, 176, 230, 267, 4.6f, 123, 105, 223, 10.32f, 78, 70 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Physical");
        }
    }
}
