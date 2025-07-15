using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoFKRestaurante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurante_Cardapio_CardapioId",
                table: "Restaurante");

            migrationBuilder.DropIndex(
                name: "IX_Restaurante_CardapioId",
                table: "Restaurante");

            migrationBuilder.AlterColumn<string>(
                name: "SenhaHash",
                table: "Usuario",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Cardapio_RestauranteId",
                table: "Cardapio",
                column: "RestauranteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cardapio_Restaurante_RestauranteId",
                table: "Cardapio",
                column: "RestauranteId",
                principalTable: "Restaurante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cardapio_Restaurante_RestauranteId",
                table: "Cardapio");

            migrationBuilder.DropIndex(
                name: "IX_Cardapio_RestauranteId",
                table: "Cardapio");

            migrationBuilder.AlterColumn<string>(
                name: "SenhaHash",
                table: "Usuario",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurante_CardapioId",
                table: "Restaurante",
                column: "CardapioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurante_Cardapio_CardapioId",
                table: "Restaurante",
                column: "CardapioId",
                principalTable: "Cardapio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
