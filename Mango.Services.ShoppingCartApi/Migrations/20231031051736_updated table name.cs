using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.ShoppingCartApi.Migrations
{
    /// <inheritdoc />
    public partial class updatedtablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartDetails_cartHeader_CartHeaderId",
                table: "cartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartDetails",
                table: "cartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartHeader",
                table: "cartHeader");

            migrationBuilder.RenameTable(
                name: "cartDetails",
                newName: "CartDetails");

            migrationBuilder.RenameTable(
                name: "cartHeader",
                newName: "CartHeaders");

            migrationBuilder.RenameIndex(
                name: "IX_cartDetails_CartHeaderId",
                table: "CartDetails",
                newName: "IX_CartDetails_CartHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails",
                column: "CartDetailsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartHeaders",
                table: "CartHeaders",
                column: "CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "CartHeaderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartHeaders",
                table: "CartHeaders");

            migrationBuilder.RenameTable(
                name: "CartDetails",
                newName: "cartDetails");

            migrationBuilder.RenameTable(
                name: "CartHeaders",
                newName: "cartHeader");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CartHeaderId",
                table: "cartDetails",
                newName: "IX_cartDetails_CartHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartDetails",
                table: "cartDetails",
                column: "CartDetailsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartHeader",
                table: "cartHeader",
                column: "CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartDetails_cartHeader_CartHeaderId",
                table: "cartDetails",
                column: "CartHeaderId",
                principalTable: "cartHeader",
                principalColumn: "CartHeaderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
