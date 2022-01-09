using System;
using System.Collections.Generic;
using System.Linq;

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

        public double Distance
        {
            get
            {
                return Math.Round(Routes.Sum(x => x.TotalDistance), 2);
            }
            private set { }
        }

        public double Time
        {
            get
            {
                return Math.Round(Routes.Sum(x => x.TotalTime), 2);
            }
            private set { }
        }
    }
}