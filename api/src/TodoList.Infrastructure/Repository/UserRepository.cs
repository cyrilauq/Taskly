using Microsoft.AspNetCore.Identity;
using TodoList.Application.Args;
using TodoList.Application.IRepository;
using TodoList.Infrastructure.Data;
using TodoList.Domain.Entities.Interfaces;
using TodoList.Infrastructure.Entities;

namespace TodoList.Infrastructure.Repository
{
    public class UserRepository(UserManager<User> userManager, TodoListContext todoListContext) : IUserRepository
    {
        public async Task<IUser> Add(IUser entity, string password)
        {
            var user = ToEntity(entity);
            var addResult = await userManager.CreateAsync(user, password);
            if(addResult.Succeeded)
            {
                await userManager.AddToRolesAsync(user, ["User"]);
            }
            return addResult.Succeeded ? user : throw new Exception(addResult.Errors.ElementAt(0).Description);
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IUser?> FindUserByEmailOrUsername(string email, string username)
        {
            return await userManager.FindByEmailAsync(email) ?? await userManager.FindByNameAsync(username);
        }

        public async Task<IUser?> FindUserByCredentials(string login, string password)
        {
            var user = await userManager.FindByEmailAsync(login) ?? await userManager.FindByNameAsync(login);
            if (user == null || !await userManager.CheckPasswordAsync(user, password)) return null;
            return user;
        }

        public Task<IEnumerable<IUser>> Search(UserSearchArg searchArgs)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> Update(int id, IUser entity)
        {
            throw new NotImplementedException();
        }

        private static User ToEntity(IUser user)
        {
            return new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Firstname = user.Firstname,
                Lastname = user.Lastname
            };
        }
    }
}
