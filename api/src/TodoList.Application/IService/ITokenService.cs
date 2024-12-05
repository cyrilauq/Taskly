using TodoList.Domain.Entities;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.IService
{
    public interface ITokenService
    {
        string GenerateToken(IUser user);
        bool ValidateToken(string token);
    }
}
