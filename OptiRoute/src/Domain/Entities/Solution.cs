using System;
using System.Collections.Generic;
using System.Linq;

namespace OptiRoute.Domain.Entities
{
    public class Solution
    {
        public bool Feasible { get; set; }
        public Depot Depot { get; set; }
        public List<Route> Routes { get; set; } = new List<Route>();

        public double Distance
        {
            get
            {
                return Math.Round(Routes.Sum(x => x.TotalDistance), 2);
            }
        }

        public double Time
        {
            get
            {
                return Math.Round(Routes.Sum(x => x.TotalTime), 2);
            }
        }
    }
}