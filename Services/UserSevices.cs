using AutoMapper;
using Course.Data;
using Course.Data.Dtos;
using Course.Models;
using Course.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Course.Services
{
    public class UserServices
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;
        private readonly FolhaContext _folhaContext;
        private readonly UserRepository _UserRepository;

        public UserServices(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService, FolhaContext folhaContext, UserRepository userRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _folhaContext = folhaContext;
            _UserRepository = userRepository;
        }

        public async Task<User> Register(CreateUserDto dto)
        {
            // Verificar se o nome de usuário (username) já está em uso
            var existingUsername = await _userManager.FindByNameAsync(dto.UserName);
            if (existingUsername != null)
            {
                throw new ApplicationException("Nome de usuário já está em uso. Escolha outro.");
            }

            // Verificar se o CPF já está em uso
            var existingCpf = await _userManager.Users.FirstOrDefaultAsync(u => u.CPF == dto.CPF);
            if (existingCpf != null)
            {
                throw new ApplicationException("CPF já está em uso. Cada CPF deve ser único.");
            }

            
            _ = new User();

            User user = _mapper.Map<User>(dto);

            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, dto.Password);



                // Retorne o usuário se a criação for bem-sucedida
                return user;



            }
            catch (Exception e)
            {
                throw new Exception("Erro ao cadastra Usuário", e);

            }
            // Tratar o resultado da criação do usuário, se necessário.
        }

        public async Task<object> Login(LoginUserDto dto)
        {

            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            
            if (!result.Succeeded)
            {
                throw new ApplicationException("Usuario não autenticado");
            }

            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());
            var token = _tokenService.GenerateToken(user);
            var data = new { tokenUser =  token, id =  user.Id}; 
            return data;
        }
        public async Task<PagedResult<User>> GetPagedResultAsync(int pageNumber, int pageSize)

        {
            try
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
            catch (Exception e)
            {
                throw new ApplicationException("Erro ao buscar usuarios", e);
            }
        }


        public async Task<User> FindById(int id)
        {
            try
            {

                var result = await _UserRepository.GetUserByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Erro ao buscar usuario",e);
            }
        }

        public async Task<User> Update(int id, UserUpdateDto userupdates)
        {
            //var user = await _folhaContext.FindAsync<User>(id);
            try
            {
                var user = await _UserRepository.GetUserByIdAsync(id) ?? throw new ApplicationException("Usuario nao encontrado");
                user.Email = userupdates.Email;
                user.UserName = userupdates.UserName;
                user.PhoneNumber = userupdates.PhoneNumber;
                user.Name = userupdates.Name;
                user.Publicplace = userupdates.Publicplace;
                user.City = userupdates.City;
                user.GrossSalary = userupdates.GrossSalary;
                user.Office = userupdates.Office;
                user.LevelAcesse = userupdates.LevelAcesse;
                user.Status = userupdates.Status;
                user.Uf = userupdates.Uf;
                user.Number = userupdates.NumberHome;
                user.Neighborhood = userupdates.Neighborhood;
                user.Complement = userupdates.Complement;

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
                throw new Exception("Nao tem usuario selecionado para realizar a Edição", ex);
            }

        }

        public async Task<User> Delete(int id)
        {
            //var user = await _folhaContext.FindAsync<User>(id);
            try
            {
                var user = await _UserRepository.GetUserByIdAsync(id) ?? throw new ApplicationException("Usuário não encontrado");

                //_folhaContext.Remove(user);
                //await _folhaContext.SaveChangesAsync();
                await _UserRepository.DeleteUserAsync(id);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("O usuario Tem movimentos de folha e não pode ser excluido",ex);
            }
        }
    }
}
