using Microsoft.EntityFrameworkCore.Migrations;

namespace OptiRoute.Infrastructure.Migrations
{
    public partial class AddBestSolutionToBenchmarkResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestSolutionDbId",
                table: "BenchmarkResults",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkResults_BestSolutionDbId",
                table: "BenchmarkResults",
                column: "BestSolutionDbId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BenchmarkResults_Solutions_BestSolutionDbId",
                table: "BenchmarkResults",
                column: "BestSolutionDbId",
                principalTable: "Solutions",
                principalColumn: "DbId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenchmarkResults_Solutions_BestSolutionDbId",
                table: "BenchmarkResults");

            migrationBuilder.DropIndex(
                name: "IX_BenchmarkResults_BestSolutionDbId",
                table: "BenchmarkResults");

            migrationBuilder.DropColumn(
                name: "BestSolutionDbId",
                table: "BenchmarkResults");
        }
    }
}