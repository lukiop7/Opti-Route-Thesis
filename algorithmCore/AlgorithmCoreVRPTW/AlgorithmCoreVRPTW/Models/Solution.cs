﻿using System.Collections.Generic;
using System.Linq;

namespace AlgorithmCoreVRPTW.Models
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
                return Routes.Sum(x => x.TotalDistance);
            }
        }
    }
}