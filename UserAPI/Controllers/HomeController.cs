using Microsoft.AspNetCore.Mvc;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetHomeMessage() => Ok("User Api is working...");
    }
}