using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.CVRPTW.ViewModels
{
    public class SolutionViewModel
    {
        public bool Feasible { get; set; }
        public List<RouteViewModel> Routes { get; set; } = new List<RouteViewModel>();

        public double Distance { get; set; }
    }
}
