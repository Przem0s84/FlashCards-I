using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashCards_I.Migrations
{
    public partial class CreatedByadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "FlashCardsSets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FlashCardsSets_CreatedById",
                table: "FlashCardsSets",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardsSets_Users_CreatedById",
                table: "FlashCardsSets",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardsSets_Users_CreatedById",
                table: "FlashCardsSets");

            migrationBuilder.DropIndex(
                name: "IX_FlashCardsSets_CreatedById",
                table: "FlashCardsSets");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "FlashCardsSets");
        }
    }
}
