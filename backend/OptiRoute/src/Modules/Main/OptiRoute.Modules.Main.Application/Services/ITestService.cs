using OptiRoute.Modules.Main.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Modules.Main.Application.Services
{
    public interface ITestService
    {
        Task<CreateTestResponseDto> CreateTest(CreateTestRequestDto createTestRequestDto);
    }
}
