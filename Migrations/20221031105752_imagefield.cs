using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace theFoodCampus.Migrations
{
    public partial class imagefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isUrl",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isUrl",
                table: "Images");
        }
    }
}
