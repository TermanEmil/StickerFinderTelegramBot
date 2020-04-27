using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class DescriptionDbConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StickerDescriptions_Users_AuthorId",
                table: "StickerDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_StickerDescriptions_Stickers_StickerId",
                table: "StickerDescriptions");

            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "Stickers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "StickerId",
                table: "StickerDescriptions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StickerDescriptions",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "StickerDescriptions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StickerDescriptions_Users_AuthorId",
                table: "StickerDescriptions",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StickerDescriptions_Stickers_StickerId",
                table: "StickerDescriptions",
                column: "StickerId",
                principalTable: "Stickers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StickerDescriptions_Users_AuthorId",
                table: "StickerDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_StickerDescriptions_Stickers_StickerId",
                table: "StickerDescriptions");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Stickers");

            migrationBuilder.AlterColumn<string>(
                name: "StickerId",
                table: "StickerDescriptions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StickerDescriptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "StickerDescriptions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_StickerDescriptions_Users_AuthorId",
                table: "StickerDescriptions",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StickerDescriptions_Stickers_StickerId",
                table: "StickerDescriptions",
                column: "StickerId",
                principalTable: "Stickers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
