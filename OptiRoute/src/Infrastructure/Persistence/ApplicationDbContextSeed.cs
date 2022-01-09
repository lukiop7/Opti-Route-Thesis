using OptiRoute.Domain.Entities;
using OptiRoute.Domain.ValueObjects;
using OptiRoute.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace OptiRoute.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Colour = Colour.Blue,
                    Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    }
                });

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedBenchmarksDataAsync(ApplicationDbContext context)
        {

            if (!context.BenchmarkInstances.Any())
            {
                BenchmarkInstance[] benchmarkInstances = new BenchmarkInstance[]
                {
                    new BenchmarkInstance(){Name="C101", BestDistance=828.94, BestVehicles=10},
                    new BenchmarkInstance(){Name="C102", BestDistance=828.94, BestVehicles=10},
                    new BenchmarkInstance(){Name="C103", BestDistance=828.06, BestVehicles=10},
                    new BenchmarkInstance(){Name="C104", BestDistance=824.78, BestVehicles=10},
                    new BenchmarkInstance(){Name="C105", BestDistance=828.94, BestVehicles=10},
                    new BenchmarkInstance(){Name="C106", BestDistance=828.94, BestVehicles=10},
                    new BenchmarkInstance(){Name="C107", BestDistance=828.94, BestVehicles=10},
                    new BenchmarkInstance(){Name="C108", BestDistance=828.94, BestVehicles=10},
                    new BenchmarkInstance(){Name="C109", BestDistance=828.94, BestVehicles=10},

                    new BenchmarkInstance(){Name="C201", BestDistance=591.56, BestVehicles=3},
                    new BenchmarkInstance(){Name="C202", BestDistance=591.56, BestVehicles=3},
                    new BenchmarkInstance(){Name="C203", BestDistance=591.17, BestVehicles=3},
                    new BenchmarkInstance(){Name="C204", BestDistance=590.60, BestVehicles=3},
                    new BenchmarkInstance(){Name="C205", BestDistance=588.88, BestVehicles=3},
                    new BenchmarkInstance(){Name="C206", BestDistance=588.49, BestVehicles=3},
                    new BenchmarkInstance(){Name="C207", BestDistance=588.29, BestVehicles=3},
                    new BenchmarkInstance(){Name="C208", BestDistance=588.32, BestVehicles=3},

                    new BenchmarkInstance(){Name="R101", BestDistance=1650.80, BestVehicles=19},
                    new BenchmarkInstance(){Name="R102", BestDistance=1486.12, BestVehicles=17},
                    new BenchmarkInstance(){Name="R103", BestDistance=1292.68, BestVehicles=13},
                    new BenchmarkInstance(){Name="R104", BestDistance=1007.31, BestVehicles=9},
                    new BenchmarkInstance(){Name="R105", BestDistance=1377.11, BestVehicles=14},
                    new BenchmarkInstance(){Name="R106", BestDistance=1252.03, BestVehicles=12},
                    new BenchmarkInstance(){Name="R107", BestDistance=1104.66, BestVehicles=10},
                    new BenchmarkInstance(){Name="R108", BestDistance=960.88, BestVehicles=9},
                    new BenchmarkInstance(){Name="R109", BestDistance=1194.73, BestVehicles=11},
                    new BenchmarkInstance(){Name="R110", BestDistance=1118.84, BestVehicles=10},
                    new BenchmarkInstance(){Name="R111", BestDistance=1096.72, BestVehicles=10},
                    new BenchmarkInstance(){Name="R112", BestDistance=982.14, BestVehicles=9},

                    new BenchmarkInstance(){Name="R201", BestDistance=1252.37, BestVehicles=4},
                    new BenchmarkInstance(){Name="R202", BestDistance=1191.70, BestVehicles=3},
                    new BenchmarkInstance(){Name="R203", BestDistance=939.50, BestVehicles=3},
                    new BenchmarkInstance(){Name="R204", BestDistance=825.52, BestVehicles=2},
                    new BenchmarkInstance(){Name="R205", BestDistance=994.43, BestVehicles=3},
                    new BenchmarkInstance(){Name="R206", BestDistance=906.14, BestVehicles=3},
                    new BenchmarkInstance(){Name="R207", BestDistance=890.61, BestVehicles=2},
                    new BenchmarkInstance(){Name="R208", BestDistance=726.82, BestVehicles=2},
                    new BenchmarkInstance(){Name="R209", BestDistance=909.16, BestVehicles=3},
                    new BenchmarkInstance(){Name="R210", BestDistance=939.37, BestVehicles=3},
                    new BenchmarkInstance(){Name="R211", BestDistance=885.71, BestVehicles=2},

                    new BenchmarkInstance(){Name="RC101", BestDistance=1696.95, BestVehicles=14},
                    new BenchmarkInstance(){Name="RC102", BestDistance=1554.75, BestVehicles=12},
                    new BenchmarkInstance(){Name="RC103", BestDistance=1261.67, BestVehicles=11},
                    new BenchmarkInstance(){Name="RC104", BestDistance=1135.48, BestVehicles=10},
                    new BenchmarkInstance(){Name="RC105", BestDistance=1629.44, BestVehicles=13},
                    new BenchmarkInstance(){Name="RC106", BestDistance=1424.73, BestVehicles=11},
                    new BenchmarkInstance(){Name="RC107", BestDistance=1230.48, BestVehicles=11},
                    new BenchmarkInstance(){Name="RC108", BestDistance=1139.82, BestVehicles=10},

                    new BenchmarkInstance(){Name="RC201", BestDistance=1406.94, BestVehicles=4},
                    new BenchmarkInstance(){Name="RC202", BestDistance=1365.65, BestVehicles=3},
                    new BenchmarkInstance(){Name="RC203", BestDistance=1049.62, BestVehicles=3},
                    new BenchmarkInstance(){Name="RC204", BestDistance=798.46, BestVehicles=3},
                    new BenchmarkInstance(){Name="RC205", BestDistance=1297.65, BestVehicles=4},
                    new BenchmarkInstance(){Name="RC206", BestDistance=1146.32, BestVehicles=3},
                    new BenchmarkInstance(){Name="RC207", BestDistance=1061.14, BestVehicles=3},
                    new BenchmarkInstance(){Name="RC208", BestDistance=828.14, BestVehicles=3},
                };

                await context.AddRangeAsync(benchmarkInstances);
                await context.SaveChangesAsync();
            }
        }
    }
}
