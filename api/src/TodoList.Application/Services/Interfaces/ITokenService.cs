using TodoList.Domain.Entities;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(IUser user);
        bool ValidateToken(string token);
    }
}
