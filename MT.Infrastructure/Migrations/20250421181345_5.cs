using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemImageEntity_Items_ItemId",
                table: "ItemImageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemImageEntity",
                table: "ItemImageEntity");

            migrationBuilder.RenameTable(
                name: "ItemImageEntity",
                newName: "ItemImages");

            migrationBuilder.RenameIndex(
                name: "IX_ItemImageEntity_ItemId",
                table: "ItemImages",
                newName: "IX_ItemImages_ItemId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemImages",
                table: "ItemImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemImages_Items_ItemId",
                table: "ItemImages",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemImages_Items_ItemId",
                table: "ItemImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemImages",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "ItemImages",
                newName: "ItemImageEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ItemImages_ItemId",
                table: "ItemImageEntity",
                newName: "IX_ItemImageEntity_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemImageEntity",
                table: "ItemImageEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemImageEntity_Items_ItemId",
                table: "ItemImageEntity",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
