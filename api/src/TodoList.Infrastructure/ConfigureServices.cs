using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repository;
using TodoList.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using TodoList.Domain.IRepository;

namespace TodoList.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<TodoListContext>(options => options.UseSqlite("Data Source=C:\\Users\\cyril\\AppData\\Local\\todolist.db"));

            services.AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddEntityFrameworkStores<TodoListContext>();

            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }
}
