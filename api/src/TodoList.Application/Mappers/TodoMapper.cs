using AutoMapper;
using TodoList.Application.DTOs;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.Mappers
{
    internal class TodoMapper: Profile
    {
        public TodoMapper()
        {
            CreateMap<TodoDTO, ITodo>()
                .ReverseMap();
        }
    }
}
