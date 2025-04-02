using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colectare_date.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareLongitudineSiLatitudineInTabelaColectari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Colectari",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Latitudine",
                table: "Colectari",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitudine",
                table: "Colectari",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Colectari");

            migrationBuilder.DropColumn(
                name: "Latitudine",
                table: "Colectari");

            migrationBuilder.DropColumn(
                name: "Longitudine",
                table: "Colectari");
        }
    }
}
