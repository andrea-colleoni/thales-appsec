using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetIdentity.Controllers
{
    [ApiController]
    [Route("api/secure")]
    public class SecureController : ControllerBase
    {
        [Route("check")]
        [Authorize]
        public ActionResult<string> Verifica()
        {
            return Ok($"ok {User.Identity.Name}");
        }
    }
}
