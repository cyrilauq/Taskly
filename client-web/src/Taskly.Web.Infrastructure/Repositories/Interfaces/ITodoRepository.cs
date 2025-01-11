﻿using Taskly.Web.Infrastructure.DTO;

namespace Taskly.Web.Infrastructure.Repositories.Interfaces
{
    public interface ITodoRepository : ICRUDRepository<TodoDTO, Guid>
    {
    }
}
