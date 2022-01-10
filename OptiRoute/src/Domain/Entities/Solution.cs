using System.Collections.Generic;

namespace OptiRoute.Domain.Entities
{
    public class Solution
    {
        public int DbId { get; set; }
        public bool Feasible { get; set; }
        public int DepotDbId { get; set; }
        public Depot Depot { get; set; }
        public List<Route> Routes { get; set; } = new List<Route>();

        public BenchmarkResult BenchmarkResult { get; set; }
        public BenchmarkResult BestBenchmarkResult { get; set; }

        public double Distance { get; set; }

        public double Time { get; set; }
    }
}