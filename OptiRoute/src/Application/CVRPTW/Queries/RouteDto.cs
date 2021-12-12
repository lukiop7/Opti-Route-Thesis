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
   public class RouteDto : IMapFrom<Route>
    {
        public List<int> Customers { get; set; } = new List<int>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Route,RouteDto>()
                .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src.Customers.Select(y => y.Id).ToList()));
        }
    }
}
