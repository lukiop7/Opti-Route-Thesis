using AutoMapper;
using OptiRoute.Application.Common.Mappings;
using OptiRoute.Domain.Entities;

namespace OptiRoute.Application.CVRPTW.Dtos
{
    public class DepotDto : IMapFrom<Depot>
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int DueDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DepotDto, Depot>().ReverseMap();
        }
    }
}