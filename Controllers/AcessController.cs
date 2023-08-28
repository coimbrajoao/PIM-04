using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AcessController : Controller
    {
        [HttpGet]
        [Authorize (policy:  "Idade Minima")]
        public IActionResult Get()
        {
            return Ok("Acesso permitido");
        }
    }
}
