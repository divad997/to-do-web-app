using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoInfrastructure.Migrations
{
    public partial class ReminderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ToDoLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Reminded",
                table: "ToDoLists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ToDoLists");

            migrationBuilder.DropColumn(
                name: "Reminded",
                table: "ToDoLists");
        }
    }
}
