using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace Soundlinks.WebApi.Controllers
{
        /// <summary>
        /// The base controller for Soundlinks module.
        /// </summary>
        /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
        [ApiController]
        [Route(ApiRoute.SoundlinksModule)]
        public abstract class BaseController : ControllerBase
        {
            private IMediator mediator;

            protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        }
}
