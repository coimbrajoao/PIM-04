using AutoMapper;
using Course.Data.Dtos;
using Course.Models;
using Course.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Course.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]//criando rota para web
    public class UserController : Controller
    {
        private UserServices _UserService;

        public UserController(UserServices registerUser)
        {
            _UserService = registerUser;
        }

        [HttpPost]
        public async Task< IActionResult> Register(CreateUserDto dto)
        {
            Console.WriteLine(dto.UserEmail);
            await _UserService.Register(dto);
            return Ok();
        }

        [HttpGet]
        
        public async Task<IActionResult> ViewAllUsers(int pag, int pagesize)
        {
            int page = pag;
            int pageSize = pagesize;
            var users = await _UserService.GetPagedResultAsync(page, pageSize);
            return Ok(users);
        }

        [HttpGet]
        [Route("id")]
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
