using AutoMapper;
using OptiRoute.Application.Common.Mappings;
using OptiRoute.Domain.Entities;
using NUnit.Framework;
using System;
using System.Runtime.Serialization;
using OptiRoute.Application.CVRPTW.Dtos;

namespace OptiRoute.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
        
        [Test]
        [TestCase(typeof(DepotDto), typeof(Depot))]
        [TestCase(typeof(CustomerDto), typeof(Customer))]
        [TestCase(typeof(ProblemDto), typeof(Problem))]
        [TestCase(typeof(Solution), typeof(SolutionDto))]
        [TestCase(typeof(Route), typeof(RouteDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
