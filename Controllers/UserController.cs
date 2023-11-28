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
        private readonly UserServices _UserService;

        public UserController(UserServices registerUser)
        {
            _UserService = registerUser;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto dto)
        {
            try
            {

                var CreatedUser = await _UserService.Register(dto);
                return CreatedAtAction(nameof(Register), new { id = CreatedUser.Id }, CreatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Erro ao criar usuario", erro = ex.Message });
            }


        }

        [HttpGet]

        public async Task<IActionResult> ViewAllUsers(int pag, int pagesize)
        {
            try
            {

                int page = pag;
                int pageSize = pagesize;
                var users = await _UserService.GetPagedResultAsync(page, pageSize);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao listar usuarios", erro = ex.Message });
            }
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {

                var user = await _UserService.FindById(id);
                if (user == null)
                {
                    return NotFound(new { message = "Usuario Não encontrado" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao recuperar usuario", erro = ex.Message });
            }
        }

        [HttpPut]

        public async Task<IActionResult> Edit(int id, UserUpdateDto userupdate)
        {
            try
            {

                var user = await _UserService.Update(id, userupdate);
                if (user == null)
                {
                    return NotFound(new { message = "Usuario Não encontrado" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao atualizar usuario", erro = ex.Message });
            }
        }



        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var user = await _UserService.Delete(id);
                if (user == null)
                {
                    return NotFound(new { message = "Usuario Não encontrado" });
                }
                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao excluir usuario", erro = ex.Message });
            }
        }


    }
}
