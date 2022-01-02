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
   public class RouteDto : IMapFrom<Route>
    {
        public List<int> Customers { get; set; } = new List<int>();
        public double TotalTime { get; set; }
        public double TotalDistance { get; set; }
        public double TotalLoad { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Route,RouteDto>()
                .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src.Customers.Select(y => y.Id).ToList()))
                .ForMember(dest => dest.TotalTime, opt => opt.MapFrom(src => src.Vehicle.CurrentTime))
                .ForMember(dest => dest.TotalLoad, opt => opt.MapFrom(src => src.Vehicle.CurrentLoad))
                .ForMember(dest => dest.TotalDistance, opt => opt.MapFrom(src => src.TotalDistance));
        }
    }
}
