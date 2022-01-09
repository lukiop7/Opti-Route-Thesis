using OptiRoute.Application.Common.Mappings;
using OptiRoute.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.Benchmarks.Queries.GetBenchmarkResultByIdQuery
{
    public class BenchmarkInstanceDto : IMapFrom<BenchmarkInstance>
    {
        public int DbId { get; set; }
        public string Name { get; set; }

        public double BestDistance { get; set; }

        public double BestVehicles { get; set; }

    }
}
