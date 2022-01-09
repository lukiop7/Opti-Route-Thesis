using AutoMapper;
using OptiRoute.Application.Common.Mappings;
using OptiRoute.Application.CVRPTW.Dtos;
using OptiRoute.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.Benchmarks.Queries.GetBenchmarkResultByIdQuery
{
    public class BenchmarkResultDetailsDto : IMapFrom<BenchmarkResult>
    {
        public int DbId { get; set; }

        public string Name { get; set; }

        public double BestDistance { get; set; }

        public double BestVehicles { get; set; }

        public SolutionDto SolutionDto { get; set; }

        public BenchmarkInstanceDto BenchmarkInstanceDto { get; set; }

        public int SolutionDbId { get; set; }

        public int BenchmarkInstanceDbId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BenchmarkResult, BenchmarkResultDetailsDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BenchmarkInstance.Name))
                 .ForMember(dest => dest.BestDistance, opt => opt.MapFrom(src => src.BenchmarkInstance.BestDistance))
                 .ForMember(dest => dest.BestVehicles, opt => opt.MapFrom(src => src.BenchmarkInstance.BestVehicles))
                 .ForMember(dest => dest.SolutionDto, opt => opt.MapFrom(src => src.Solution))
                 .ForMember(dest => dest.BenchmarkInstanceDto, opt => opt.MapFrom(src => src.BenchmarkInstance));
        }
    }
}
