using AutoMapper;
using Taskly.Client.Application.Model;
using Taskly.Client.Domain.DTO;

namespace Taskly.Client.Application.Mappers
{
    public class TodoMapperProfile : Profile
    {
        public TodoMapperProfile()
        {
            CreateMap<TodoModel, TodoDTO>()
                .ReverseMap();
        }
    }
}
