using Course.Data.Dtos;
using Course.Services;
using Microsoft.AspNetCore.Mvc;

namespace Course.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]//criando rota para web
    public class LoginController : Controller
    {
        private readonly UserServices _UserService;

        public LoginController(UserServices userService)
        {
            _UserService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
          var token =   await _UserService.Login(dto);
            return Ok(token);
        }
    }
}
