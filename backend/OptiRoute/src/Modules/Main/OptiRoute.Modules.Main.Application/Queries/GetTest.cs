using AutoMapper;
using MediatR;
using OptiRoute.Modules.Main.Application.Dtos;
using OptiRoute.Modules.Main.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Modules.Main.Application.Queries
{
    public class GetTest
    {
        public class Query: IRequest<testDto>
        {
            public Query()
            {

            }
        }

        public class Handler : IRequestHandler<Query, testDto>
        {
            private readonly IMapper mapper;
            private readonly ITestRepository testRepository;

            public Handler(IMapper mapper, ITestRepository testRepository)
            {
                this.mapper = mapper;
                this.testRepository = testRepository;
            }

            public async Task<testDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var test = await testRepository.GetTask();
                var testDto = mapper.Map<testDto>(test);

                return testDto;
            }
        }
    }
}
