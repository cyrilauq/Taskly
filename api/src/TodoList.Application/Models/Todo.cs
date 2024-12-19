using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.Models
{
    public class Todo : ITodo
    {
        public required string Name { get; set; }
        public string? Content { get; set; }
        public bool IsDone { get; set; }
        public Guid Id { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public Guid UserId { get; set; }
    }
}
