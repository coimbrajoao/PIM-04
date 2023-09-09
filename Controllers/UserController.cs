using AutoMapper;
using Course.Data.Dtos;
using Course.Models;
using Course.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Course.Controllers
{

    [ApiController]
    [Route("user")]//criando rota para web
    public class UserController : Controller
    {
        private UserServices _UserService;

        public UserController(UserServices registerUser)
        {
            _UserService = registerUser;
        }

        [HttpPost ("Cadastro")]
        public async Task< IActionResult> Register(CreateUserDto dto)
        {
            Console.WriteLine(dto.UserEmail);
            await _UserService.Register(dto);
            return Ok();
        }

        [HttpGet]
        [Route ("view")]
        public async Task<IActionResult> ViewAllUsers()
        {
            int page = 1;
            int pageSize = 1;
            var users = await _UserService.GetPagedResultAsync(page, pageSize);
            return Ok(users);
        }

        [HttpGet]
        [Route("view{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var user= await _UserService.FindById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id,UserUpdateDto userupdate)
        {
            var user = await _UserService.Update(id,userupdate);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _UserService.Delete(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

    
    }
}
