using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obada_Shop.API.Services;

namespace Obada_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        IOS iosServices;
        public PlatformsController (IOS os)
        {
            this.iosServices = os;
        }

        [HttpGet]
        public IActionResult get()
        {
            return Ok(iosServices.RunService());
        }
    }
}
