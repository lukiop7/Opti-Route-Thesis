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
   public class ProblemDto : IMapFrom<Problem>
    {
        public int Vehicles { get; set; }
        public int Capacity { get; set; }
        public DepotDto Depot { get; set; }
        public List<CustomerDto> Customers { get; set; }
        public List<List<double>> Distances { get; set; }
        public List<List<double>> Durations { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProblemDto, Problem>().ReverseMap();
        }
    }
}
