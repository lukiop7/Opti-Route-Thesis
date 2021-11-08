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
        public int ReadyTime { get; set; }
        public int DueDate { get; set; }
        public int ServiceTime { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerViewModel, Customer>();
        }
    }
}
