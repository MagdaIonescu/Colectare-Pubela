using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colectare_date.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PubeleCetateni_Cetateni_CetateanId",
                table: "PubeleCetateni");

            migrationBuilder.AddForeignKey(
                name: "FK_PubeleCetateni_Cetateni_CetateanId",
                table: "PubeleCetateni",
                column: "CetateanId",
                principalTable: "Cetateni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PubeleCetateni_Cetateni_CetateanId",
                table: "PubeleCetateni");

            migrationBuilder.AddForeignKey(
                name: "FK_PubeleCetateni_Cetateni_CetateanId",
                table: "PubeleCetateni",
                column: "CetateanId",
                principalTable: "Cetateni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
