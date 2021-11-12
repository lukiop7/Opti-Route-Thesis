using AlgorithmCoreVRPTW.Models;
using AutoMapper;
using OptiRoute.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.CVRPTW.ViewModels
{
  public  class CustomerViewModel : IMapFrom<Customer>
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Demand { get; set; }
        public DateTime ReadyTime { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ServiceTime { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerViewModel, Customer>()
                     .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate.TimeOfDay.TotalSeconds))
                 .ForMember(dest => dest.ReadyTime, opt => opt.MapFrom(src => src.ReadyTime.TimeOfDay.TotalSeconds))
                 .ForMember(dest => dest.ServiceTime, opt => opt.MapFrom(src => src.ServiceTime.TimeOfDay.TotalSeconds)); 
        }
    }
}
