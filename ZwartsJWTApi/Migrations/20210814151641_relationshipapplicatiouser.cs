using Microsoft.EntityFrameworkCore.Migrations;

namespace ZwartsJWTApi.Migrations
{
    public partial class relationshipapplicatiouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "toDoLists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_toDoLists_UserId",
                table: "toDoLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_toDoLists_AspNetUsers_UserId",
                table: "toDoLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_toDoLists_AspNetUsers_UserId",
                table: "toDoLists");

            migrationBuilder.DropIndex(
                name: "IX_toDoLists_UserId",
                table: "toDoLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "toDoLists");
        }
    }
}
