using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace theFoodCampus.Migrations
{
    public partial class AddedTagElement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Recipes");
        }
    }
}
