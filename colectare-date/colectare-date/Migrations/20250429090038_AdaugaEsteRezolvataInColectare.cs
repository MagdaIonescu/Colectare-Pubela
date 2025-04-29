using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colectare_date.Migrations
{
    /// <inheritdoc />
    public partial class AdaugaEsteRezolvataInColectare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsteRezolvata",
                table: "Colectari",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsteRezolvata",
                table: "Colectari");
        }
    }
}
