using AlgorithmCoreVRPTW.Models;
using AutoMapper;
using OptiRoute.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.CVRPTW.Dtos
{
    public class SolutionDto : IMapFrom<Solution>
    {
        public bool Feasible { get; set; }
        public List<RouteDto> Routes { get; set; } = new List<RouteDto>();

        public double Distance { get; set; }
        public double Time { get; set; }
    }
}
