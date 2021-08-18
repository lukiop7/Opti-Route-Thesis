using AutoMapper;
using OptiRoute.Modules.Main.Application.Dtos;
using OptiRoute.Modules.Main.Domain.Interfaces;
using OptiRoute.Shared.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Modules.Main.Application.Services
{
    public class TestService: ITestService
    {
        private readonly ITestRepository testRepository;
        private readonly IMapper mapper;

        public TestService(ITestRepository testRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.testRepository = testRepository;
        }

        public async Task<CreateTestResponseDto> CreateTest(CreateTestRequestDto createTestRequestDto)
        {
            var newTest = mapper.Map<Test>(createTestRequestDto);

            var createdTest = await testRepository.AddTestAsync(newTest);

            var testResponse = mapper.Map<CreateTestResponseDto>(createdTest);

            return testResponse;
        }
    }
}
