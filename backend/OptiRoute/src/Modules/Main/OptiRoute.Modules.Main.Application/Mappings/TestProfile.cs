using AutoMapper;
using OptiRoute.Modules.Main.Application.Dtos;
using OptiRoute.Shared.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptiRoute.Modules.Main.Application.Mappings
{
    public class TestProfile:Profile
    {
        public TestProfile()
        {
            CreateMap<Test, testDto>()
                .ForMember(test => test.Name,
                opt => opt.MapFrom(x => x.Name))
                  .ForMember(test => test.Age,
                opt => opt.MapFrom(x => x.Age));

            CreateMap<CreateTestRequestDto, Test>()
                .ForMember(test => test.Name,
                opt => opt.MapFrom(x => x.Name))
                  .ForMember(test => test.Age,
                opt => opt.MapFrom(x => x.Age));

            CreateMap<Test,CreateTestResponseDto>()
                  .ForMember(test => test.Id,
                opt => opt.MapFrom(x => x.Id))
                .ForMember(test => test.Name,
                opt => opt.MapFrom(x => x.Name))
                  .ForMember(test => test.Age,
                opt => opt.MapFrom(x => x.Age));
        }
    }
}
