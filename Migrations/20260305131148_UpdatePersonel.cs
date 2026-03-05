using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersonel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Personeller_BirimId",
                table: "Personeller",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_UnvanId",
                table: "Personeller",
                column: "UnvanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_Birimler_BirimId",
                table: "Personeller",
                column: "BirimId",
                principalTable: "Birimler",
                principalColumn: "BirimId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_Unvanlar_UnvanId",
                table: "Personeller",
                column: "UnvanId",
                principalTable: "Unvanlar",
                principalColumn: "UnvanId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_Birimler_BirimId",
                table: "Personeller");

            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_Unvanlar_UnvanId",
                table: "Personeller");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_BirimId",
                table: "Personeller");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_UnvanId",
                table: "Personeller");
        }
    }
}
