using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemImages_Items_ItemId",
                table: "ItemImages");

            migrationBuilder.DropIndex(
                name: "IX_ItemImages_ItemId",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ItemImages");

            migrationBuilder.AddColumn<long>(
                name: "ItemEntityId",
                table: "ItemImages",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_ItemEntityId",
                table: "ItemImages",
                column: "ItemEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemImages_Items_ItemEntityId",
                table: "ItemImages",
                column: "ItemEntityId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemImages_Items_ItemEntityId",
                table: "ItemImages");

            migrationBuilder.DropIndex(
                name: "IX_ItemImages_ItemEntityId",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "ItemEntityId",
                table: "ItemImages");

            migrationBuilder.AddColumn<long>(
                name: "ItemId",
                table: "ItemImages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_ItemId",
                table: "ItemImages",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemImages_Items_ItemId",
                table: "ItemImages",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
