using Microsoft.AspNetCore.Mvc;
using Soundlinks.Modules.Soundlinks.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Soundlinks.WebApi.Controllers
{
    /// <summary>
    /// The controller for Soundlinks module.
    /// </summary>
    /// <seealso cref="Soundlinks.WebApi.Controllers.Soundlinks.BaseController" />
    public class SoundlinksController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetSoundlinksDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("id:long")]
        public async Task<ActionResult<GetSoundlinkDto>> GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
