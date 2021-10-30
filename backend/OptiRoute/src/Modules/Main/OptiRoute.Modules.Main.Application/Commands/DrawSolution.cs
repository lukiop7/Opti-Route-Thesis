using MediatR;
using OptiRoute.Modules.Main.Application.Dtos;
using OptiRoute.Shared.SolutionDrawer;
using OptiRoute.Shared.SolutionDrawer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Modules.Main.Application.Commands
{
    public class DrawSolution
    {
        public class Command : IRequest<DrawSolutionResponseDto>
        {
            public DrawSolutionRequestDto DrawSolutionRequestDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, DrawSolutionResponseDto>
        {
            private readonly ISolutionDrawer solutionDrawer;
            public Handler(ISolutionDrawer solutionDrawer)
            {
                this.solutionDrawer = solutionDrawer;
            }

            public async Task<DrawSolutionResponseDto> Handle(Command command, CancellationToken cancellationToken)
            {
                var success = solutionDrawer.DrawSolution(command.DrawSolutionRequestDto.Points, command.DrawSolutionRequestDto.Route, command.DrawSolutionRequestDto.Path);
                return success;
            }
        }
    }
}
