using AutoMapper;
using TodoList.Application.DTOs;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.Mappers
{
    internal class UserMapper: Profile
    {
        public UserMapper() 
        {
            CreateMap<UserDto, IUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(opt => opt.BirthDate))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(opt => opt.Pseudo))
                .ReverseMap();
        }
    }
}
