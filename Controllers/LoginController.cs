using Course.Data.Dtos;
using Course.Services;
using Microsoft.AspNetCore.Mvc;

namespace Course.Controllers
{
    [ApiController]
    [Route("Login")]//criando rota para web
    public class LoginController : Controller
    {
        private UserServices _UserService;

        public LoginController(UserServices userService)
        {
            _UserService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
           await _UserService.Login(dto);
            return Ok("Usuario autenticado");
        }
    }
}
