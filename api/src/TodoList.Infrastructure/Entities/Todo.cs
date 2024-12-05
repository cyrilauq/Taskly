using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Entities
{
    public class Todo : Domain.Entities.Todo
    {
        // Navigation properties
        public User User { get; set; } = null!;
    }
}
