using AutoMapper;
using Course.Data.Dtos;
using Course.Models;
using Microsoft.AspNetCore.Identity;

namespace Course.Services
{
    public class UserServices 
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public UserServices(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Register(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);
            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
            // Tratar o resultado da criação do usuário, se necessário.
        }

        public async Task Login(LoginUserDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }
        }
    }
}
