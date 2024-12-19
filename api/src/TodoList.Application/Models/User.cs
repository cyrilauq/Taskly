using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.Models
{
    public class User : IUser
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public DateOnly BirthDate { get; set; }
        public Guid Id { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
