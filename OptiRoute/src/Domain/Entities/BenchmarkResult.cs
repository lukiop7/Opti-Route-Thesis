using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Domain.Entities
{
    public class BenchmarkResult
    {
        public int DbId { get; set; }

        public int SolutionDbId { get; set; }

        public int BenchmarkInstanceDbId { get; set; }

        public Solution Solution { get; set; }

        public BenchmarkInstance BenchmarkInstance {get;set;}
    }
}
