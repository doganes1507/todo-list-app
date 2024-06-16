using AutoMapper;
using ToDoListApp.Api.Models;
using ToDoListApp.Common.DataObjects.User;

namespace ToDoListApp.Api.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<UserRegisterDto, User>()
            .ForMember(dest => dest.HashPassword,
                opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
    }
}