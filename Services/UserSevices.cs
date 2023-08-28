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
        private TokenService _tokenService;

        public UserServices(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task Register(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);
            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
            // Tratar o resultado da criação do usuário, se necessário.
        }

        public async Task<string> Login(LoginUserDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Usuario não autenticado");
            }

            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName== dto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return token;
        }
    }
}
