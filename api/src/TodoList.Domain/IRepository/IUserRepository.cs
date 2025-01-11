using TodoList.Domain.Args;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Domain.IRepository
{
    public interface IUserRepository
    {
        Task<IUser> Add(IUser entity, string password);
        Task<bool> Delete(int id);
        Task<IEnumerable<IUser>> Search(UserSearchArg searchArgs);
        Task<IUser> Update(int id, IUser entity);
        Task<IUser?> FindUserByEmailOrUsername(string email, string username);
        Task<IUser?> FindUserByCredentials(string login, string password);
    }
}
