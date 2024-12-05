using Microsoft.AspNetCore.Identity;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Infrastructure.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
