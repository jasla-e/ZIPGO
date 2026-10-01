using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ZIPGO.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    public class AdminController:ControllerBase
    {
        [HttpGet]
        public IActionResult GetAdminData()
        {
            return Ok("only admin can access");
        }



    }
}
