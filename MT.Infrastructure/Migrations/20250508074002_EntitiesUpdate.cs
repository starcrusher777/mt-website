using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserEntityId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ContactsEntity_ContactsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalsEntity_PersonalsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SocialsEntity_SocialsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ContactsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonalsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SocialsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserEntityId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ContactsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonalsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SocialsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "SocialsEntity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "PersonalsEntity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ContactsEntity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SocialsEntity_UserId",
                table: "SocialsEntity",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalsEntity_UserId",
                table: "PersonalsEntity",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsEntity_UserId",
                table: "ContactsEntity",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactsEntity_Users_UserId",
                table: "ContactsEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalsEntity_Users_UserId",
                table: "PersonalsEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialsEntity_Users_UserId",
                table: "SocialsEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactsEntity_Users_UserId",
                table: "ContactsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalsEntity_Users_UserId",
                table: "PersonalsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialsEntity_Users_UserId",
                table: "SocialsEntity");

            migrationBuilder.DropIndex(
                name: "IX_SocialsEntity_UserId",
                table: "SocialsEntity");

            migrationBuilder.DropIndex(
                name: "IX_PersonalsEntity_UserId",
                table: "PersonalsEntity");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_ContactsEntity_UserId",
                table: "ContactsEntity");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SocialsEntity");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PersonalsEntity");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ContactsEntity");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.AddColumn<long>(
                name: "ContactsId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PersonalsId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SocialsId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserEntityId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactsId",
                table: "Users",
                column: "ContactsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalsId",
                table: "Users",
                column: "PersonalsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SocialsId",
                table: "Users",
                column: "SocialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserEntityId",
                table: "Orders",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserEntityId",
                table: "Orders",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ContactsEntity_ContactsId",
                table: "Users",
                column: "ContactsId",
                principalTable: "ContactsEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonalsEntity_PersonalsId",
                table: "Users",
                column: "PersonalsId",
                principalTable: "PersonalsEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SocialsEntity_SocialsId",
                table: "Users",
                column: "SocialsId",
                principalTable: "SocialsEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
