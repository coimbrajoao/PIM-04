using AutoMapper;
using Course.Data;
using Course.Data.Dtos;
using Course.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Course.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services
{
    public class UserServices
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private TokenService _tokenService;
        private FolhaContext _folhaContext;
        private UserRepository _UserRepository;

        public UserServices(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService, FolhaContext folhaContext, UserRepository userRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _folhaContext = folhaContext;
            _UserRepository = userRepository;
        }

        public async Task Register(CreateUserDto dto)
        {
            User user = new User();

            user = _mapper.Map<User>(dto);

            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
            }
            catch (Exception e)
            {
                Console.WriteLine("Usuario tentando cadastrar => " + user.Email);
                Console.Error.Write("ERROOOO SEUS VAGABUNDOS =>>>>> " + e.Message);
            }
            // Tratar o resultado da criação do usuário, se necessário.
        }

        public async Task<string> Login(LoginUserDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Usuario não autenticado");
            }

            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return token;
        }
        public async Task<PagedResult<User>> GetPagedResultAsync(int pageNumber, int pageSize)
        {
            var result = await _folhaContext.Users.OrderBy(x => x.Id).ToListAsync();
            var count = await _folhaContext.Users.CountAsync();

            var item = result.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var pagedResult = new PagedResult<User>()
            {
                TotalCount = count,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = item
            };

            return pagedResult;
        }


        public async Task<User> FindById(int id)
        {
            var result = await _UserRepository.GetUserByIdAsync(id);
            return result;
        }

        public async Task<User> Update(int id, UserUpdateDto userupdates)
        {
            //var user = await _folhaContext.FindAsync<User>(id);
            try
            {
                var user = await _UserRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    throw new ApplicationException("Usuario nao encontrado");
                }

                user.Email = userupdates.Email;
                user.UserName = userupdates.UserName;
                user.PhoneNumber = userupdates.PhoneNumber;
                user.Name = userupdates.Name;
                user.Logradouro = userupdates.Logradouro;
                try
                {


                    // await _folhaContext.SaveChangesAsync();
                    await _UserRepository.UpdateUserAsync(user);
                    return user;
                }
                catch (Exception)
                {
                    throw new Exception("Falha ao editar o usuario");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Nao tem usuario selecionado para realizar a Edição");
            }

        }

        public async Task<User> Delete(int id)
        {
            //var user = await _folhaContext.FindAsync<User>(id);
            try
            {
                var user = await _UserRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    throw new ApplicationException("Usuário não encontrado");
                }

                //_folhaContext.Remove(user);
                //await _folhaContext.SaveChangesAsync();
                await _UserRepository.DeleteUserAsync(id);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("O usuario Tem movimentos de folha e não pode ser excluido");
            }
        }
    }
}
