using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.IRepository;
using TodoList.Infrastructure.Entities;

namespace TodoList.Infrastructure.Repository
{
    public class RoleRepository(RoleManager<Role> roleManager, UserManager<User> userManager) : IRoleRepository
    {
        public async Task<IEnumerable<string>> Find(RoleSearchArgs? args = null, CancellationToken cancellationToken = default)
        {
            if (args == null) return await roleManager.Roles.Select(r => r.Name!).ToListAsync(cancellationToken);
            if (args.UserName != null) return await userManager.GetRolesAsync((await userManager.FindByNameAsync(args.UserName))!);
            return [];
        }
    }
}
