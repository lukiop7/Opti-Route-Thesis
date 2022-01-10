using Microsoft.EntityFrameworkCore.Migrations;

namespace OptiRoute.Infrastructure.Migrations
{
    public partial class AddDistanceAndTimeToSolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Solutions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Time",
                table: "Solutions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Solutions");
        }
    }
}