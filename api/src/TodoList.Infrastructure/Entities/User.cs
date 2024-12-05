using Microsoft.AspNetCore.Identity;
using TodoList.Domain.Entities;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Infrastructure.Entities
{
    public class User : IdentityUser<Guid>, IUser
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public IEnumerable<Todo> Todos { get; set; } = new List<Todo>();
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
