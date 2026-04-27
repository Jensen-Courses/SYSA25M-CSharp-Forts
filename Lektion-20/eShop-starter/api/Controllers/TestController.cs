using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class TestController() : ApiBaseController
    {
        [Authorize()]
        [HttpGet("secure")]
        public ActionResult GetSecureInfo()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok($"Ditt användarnamn är {userName} och du har användarid {userId}");
        }
    }
}
