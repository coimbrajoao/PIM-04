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

        [HttpPost ("/Cadastro")]
        public async Task< IActionResult> Register(CreateUserDto dto)
        {
            await _UserService.Register(dto);
            
            return Ok();
        }

        [HttpGet]
        [Route ("/view")]
        public async Task<IActionResult> ViewAllUsers()
        {
            var users = await _UserService.FindAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("/view{id}")]
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
        [Route("/{id}")]
        public async Task<IActionResult> Edit(int id,User userupdate)
        {
            var user = await _UserService.Update(id,userupdate);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [HttpDelete]
        [Route("/{id}")]
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
