using AutoMapper;
using TodoList.Application.DTOs;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.Mappers
{
    public class TodoMapper: Profile
    {
        public TodoMapper()
        {
            CreateMap<TodoDTO, ITodo>()
                .ReverseMap();
        }
    }
}
