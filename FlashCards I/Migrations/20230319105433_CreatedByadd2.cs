using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashCards_I.Migrations
{
    public partial class CreatedByadd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardsSets_Users_CreatedById",
                table: "FlashCardsSets");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "FlashCardsSets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardsSets_Users_CreatedById",
                table: "FlashCardsSets",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardsSets_Users_CreatedById",
                table: "FlashCardsSets");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "FlashCardsSets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardsSets_Users_CreatedById",
                table: "FlashCardsSets",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
