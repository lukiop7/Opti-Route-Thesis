using AutoMapper;
using OptiRoute.Application.Common.Mappings;
using OptiRoute.Domain.Entities;
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
        
        public DepotDto Depot { get; set; }

        public double Distance { get; set; }
        public double Time { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Solution, SolutionDto>()
                .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
        }
    }
}
