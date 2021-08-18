using MediatR;
using OptiRoute.Modules.Main.Application.Dtos;
using OptiRoute.Modules.Main.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Modules.Main.Application.Commands
{
    public class CreateTest
    {
        public class Command : IRequest<CreateTestResponseDto>
        {
            public CreateTestRequestDto CreateTestRequestDto { get; set; }
        }

        public class Handler: IRequestHandler<Command, CreateTestResponseDto>
        {
            private readonly ITestService testService;
            public Handler(ITestService testService)
            {
                this.testService = testService;
            }

            public async Task<CreateTestResponseDto> Handle(Command command, CancellationToken cancellationToken)
            {
                var createdUser = await testService.CreateTest(command.CreateTestRequestDto);

                return createdUser;
            }
        }
    }
}
