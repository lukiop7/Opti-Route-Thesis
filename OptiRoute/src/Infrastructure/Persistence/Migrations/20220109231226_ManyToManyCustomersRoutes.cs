using Microsoft.EntityFrameworkCore.Migrations;

namespace OptiRoute.Infrastructure.Migrations
{
    public partial class ManyToManyCustomersRoutes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Routes_RouteDbId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RouteDbId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RouteDbId",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "CustomerRoute",
                columns: table => new
                {
                    CustomersDbId = table.Column<int>(type: "integer", nullable: false),
                    RoutesDbId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRoute", x => new { x.CustomersDbId, x.RoutesDbId });
                    table.ForeignKey(
                        name: "FK_CustomerRoute_Customers_CustomersDbId",
                        column: x => x.CustomersDbId,
                        principalTable: "Customers",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerRoute_Routes_RoutesDbId",
                        column: x => x.RoutesDbId,
                        principalTable: "Routes",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRoute_RoutesDbId",
                table: "CustomerRoute",
                column: "RoutesDbId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerRoute");

            migrationBuilder.AddColumn<int>(
                name: "RouteDbId",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RouteDbId",
                table: "Customers",
                column: "RouteDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Routes_RouteDbId",
                table: "Customers",
                column: "RouteDbId",
                principalTable: "Routes",
                principalColumn: "DbId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
