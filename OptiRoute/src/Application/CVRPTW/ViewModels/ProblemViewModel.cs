using AlgorithmCoreVRPTW.Models;
using OptiRoute.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.CVRPTW.ViewModels
{
   public class ProblemViewModel
    {
        public int Vehicles { get; set; }
        public int Capacity { get; set; }
        public DepotViewModel Depot { get; set; }
        public List<CustomerViewModel> Customers { get; set; }
        public List<List<double>> Distances { get; set; }
        public List<List<double>> Durations { get; set; }
    }
}
