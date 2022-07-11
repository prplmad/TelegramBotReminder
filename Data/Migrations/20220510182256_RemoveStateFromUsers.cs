using Microsoft.EntityFrameworkCore.Migrations;

namespace DataStore.Migrations
{
    public partial class RemoveStateFromUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_States_StateId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StateId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StateId",
                table: "Users",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_States_StateId",
                table: "Users",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
