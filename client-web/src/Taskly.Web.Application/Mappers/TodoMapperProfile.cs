using AutoMapper;
using Taskly.Web.Application.Model;
using Taskly.Web.Infrastructure.DTO;

namespace Taskly.Web.Application.Mappers
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
