using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptiRoute.Infrastructure.Migrations
{
    public partial class VrptwDbSetupWithBenchmarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenchmarkInstances",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    BestDistance = table.Column<double>(type: "double precision", nullable: false),
                    BestVehicles = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenchmarkInstances", x => x.DbId);
                });

            migrationBuilder.CreateTable(
                name: "Depots",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depots", x => x.DbId);
                });

            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Feasible = table.Column<bool>(type: "boolean", nullable: false),
                    DepotDbId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_Solutions_Depots_DepotDbId",
                        column: x => x.DepotDbId,
                        principalTable: "Depots",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BenchmarkResults",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SolutionDbId = table.Column<int>(type: "integer", nullable: false),
                    BenchmarkInstanceDbId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenchmarkResults", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_BenchmarkResults_BenchmarkInstances_BenchmarkInstanceDbId",
                        column: x => x.BenchmarkInstanceDbId,
                        principalTable: "BenchmarkInstances",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenchmarkResults_Solutions_SolutionDbId",
                        column: x => x.SolutionDbId,
                        principalTable: "Solutions",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    DepotDbId = table.Column<int>(type: "integer", nullable: false),
                    SolutionDbId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_Routes_Depots_DepotDbId",
                        column: x => x.DepotDbId,
                        principalTable: "Depots",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Routes_Solutions_SolutionDbId",
                        column: x => x.SolutionDbId,
                        principalTable: "Solutions",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Demand = table.Column<int>(type: "integer", nullable: false),
                    ReadyTime = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<int>(type: "integer", nullable: false),
                    ServiceTime = table.Column<int>(type: "integer", nullable: false),
                    RouteDbId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_Customers_Routes_RouteDbId",
                        column: x => x.RouteDbId,
                        principalTable: "Routes",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkResults_BenchmarkInstanceDbId",
                table: "BenchmarkResults",
                column: "BenchmarkInstanceDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkResults_SolutionDbId",
                table: "BenchmarkResults",
                column: "SolutionDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RouteDbId",
                table: "Customers",
                column: "RouteDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_DepotDbId",
                table: "Routes",
                column: "DepotDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_SolutionDbId",
                table: "Routes",
                column: "SolutionDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_DepotDbId",
                table: "Solutions",
                column: "DepotDbId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenchmarkResults");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "BenchmarkInstances");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Solutions");

            migrationBuilder.DropTable(
                name: "Depots");
        }
    }
}
