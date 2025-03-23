using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colectare_date.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cetateni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nume = table.Column<string>(type: "TEXT", nullable: false),
                    Prenume = table.Column<string>(type: "TEXT", nullable: false),
                    CNP = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cetateni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pubele",
                columns: table => new
                {
                    PubelaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pubele", x => x.PubelaId);
                });

            migrationBuilder.CreateTable(
                name: "PubeleCetateni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PubelaId = table.Column<string>(type: "TEXT", nullable: false),
                    CetateanId = table.Column<int>(type: "INTEGER", nullable: false),
                    Adresa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PubeleCetateni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PubeleCetateni_Cetateni_CetateanId",
                        column: x => x.CetateanId,
                        principalTable: "Cetateni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PubeleCetateni_Pubele_PubelaId",
                        column: x => x.PubelaId,
                        principalTable: "Pubele",
                        principalColumn: "PubelaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PubeleCetateni_CetateanId",
                table: "PubeleCetateni",
                column: "CetateanId");

            migrationBuilder.CreateIndex(
                name: "IX_PubeleCetateni_PubelaId",
                table: "PubeleCetateni",
                column: "PubelaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PubeleCetateni");

            migrationBuilder.DropTable(
                name: "Cetateni");

            migrationBuilder.DropTable(
                name: "Pubele");
        }
    }
}
