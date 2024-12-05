using Microsoft.AspNetCore.Identity;

namespace TodoList.Infrastructure.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
