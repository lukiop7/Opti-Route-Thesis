using AutoMapper;
using OptiRoute.Application.Common.Mappings;
using OptiRoute.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.Benchmarks.Queries
{
   public class BenchmarkResultDto : IMapFrom<BenchmarkResult>
    {
        public int DbId { get; set; }

        public string Name { get; set; }

        public double BestDistance { get; set; }

        public double BestVehicles { get; set; }

        public double Distance { get; set; }

        public double Vehicles { get; set; }

        public int SolutionDbId { get; set; }

        public int BenchmarkInstanceDbId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BenchmarkResult, BenchmarkResultDto>()
                 .ForMember(dest => dest.Name, opt=> opt.MapFrom(src=> src.BenchmarkInstance.Name))
                 .ForMember(dest => dest.BestDistance, opt=> opt.MapFrom(src=> src.BenchmarkInstance.BestDistance))
                 .ForMember(dest => dest.BestVehicles, opt=> opt.MapFrom(src=> src.BenchmarkInstance.BestVehicles))
                 .ForMember(dest => dest.Distance, opt=> opt.MapFrom(src=> src.Solution.Distance))
                 .ForMember(dest => dest.Vehicles, opt=> opt.MapFrom(src=> src.Solution.Routes.Count));
        }
    }
}
