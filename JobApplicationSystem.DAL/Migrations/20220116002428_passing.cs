using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplicationSystem.DAL.Migrations
{
    public partial class passing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PssingYear",
                table: "Education");

            migrationBuilder.AddColumn<string>(
                name: "PassingYear",
                table: "Education",
                maxLength: 4,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassingYear",
                table: "Education");

            migrationBuilder.AddColumn<string>(
                name: "PssingYear",
                table: "Education",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);
        }
    }
}
