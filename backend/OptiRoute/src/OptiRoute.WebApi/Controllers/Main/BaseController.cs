using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace OptiRoute.WebApi.Controllers.Main
{
    /// <summary>
    /// The base controller for the Main module.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route(ApiRoute.MainModule)]
    public abstract class BaseController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
