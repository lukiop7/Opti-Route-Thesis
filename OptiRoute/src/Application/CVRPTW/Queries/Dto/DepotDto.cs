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
    public class DepotDto: IMapFrom<Depot>
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime DueDate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DepotDto, Depot>()
                .ForMember(dest=> dest.DueDate, opt=> opt.MapFrom(src=> src.DueDate.TimeOfDay.TotalSeconds)).ReverseMap();
        }
    }
}
