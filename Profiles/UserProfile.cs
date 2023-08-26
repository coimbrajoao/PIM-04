using AutoMapper;
using Course.Data.Dtos;
using Course.Models;

namespace Course.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
