using Microsoft.AspNetCore.Mvc;
using OptiRoute.Modules.Main.Application.Commands;
using OptiRoute.Modules.Main.Application.Dtos;
using OptiRoute.Modules.Main.Application.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptiRoute.WebApi.Controllers.Main
{

    /// <summary>
    /// The controller for Main module.
    /// </summary>
    /// <seealso cref="OptiRoute.WebApi.Controllers.Main.BaseController" />
    public class MainController : BaseController
    {
        [HttpPost("create")]
        public async Task<ActionResult<CreateTestResponseDto>> CreateTest([FromBody]CreateTestRequestDto requestDto)
        {
            var result = await Mediator.Send(new CreateTest.Command() { CreateTestRequestDto = requestDto});
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<testDto>> GetTest()
        {
            var result = await Mediator.Send(new GetTest.Query());
            return Ok(result);
        }
    }
}
