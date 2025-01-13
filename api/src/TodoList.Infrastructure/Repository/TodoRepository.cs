using Microsoft.EntityFrameworkCore;
using TodoList.Infrastructure.Data;
using TodoList.Domain.Entities.Interfaces;
using TodoList.Domain.IRepository;
using TodoList.Domain.Args;
using TodoList.Domain.Exceptions;

namespace TodoList.Infrastructure.Repository;

public class TodoRepository(TodoListContext context) : ITodoRepository
{
    public async Task<ITodo> AddAsync(ITodo entity)
    {
        var result = (await context.Todos.AddAsync(new Entities.Todo { Name = entity.Name, Content = entity.Content, UserId = entity.UserId })).Entity;
        await context.SaveChangesAsync();
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var toDelete = await context.Todos.FindAsync(id, cancellationToken) ?? throw new EntityNotExistsException($"No entity found for the id {id}");
        context.Todos.Remove(toDelete);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ITodo>> GetAllAsync(TodoSearchArg searchArgs)
    {
        var query = context.Todos.AsQueryable();

        if (searchArgs.IsDeleted) query = query.Where(t => t.DeletedOn != null);
        if (searchArgs.UserId != Guid.Empty) query = query.Where(t => t.UserId == searchArgs.UserId);

        var queryString = query.ToQueryString();

        return await query.ToListAsync();
    }

    public async Task<ITodo> UpdateAsync(string id, ITodo entity)
    {
        if (!await UserExists(entity.UserId)) throw new InvalidOperationException("The entity isn't related to an existing user");
        if (await context.Todos.FindAsync(Guid.Parse(id)) is null || !entity.Id.Equals(Guid.Parse(id))) throw new InvalidOperationException("No entity related to the given id");
        entity.UpdatedOn = DateTime.Now;
        context.Todos.Update(entity as Entities.Todo);
        await context.SaveChangesAsync();
        return entity;
    }

    private async Task<bool> UserExists(Guid id)
    {
        return await context.Users.AnyAsync(u => u.Id.Equals(id));
    }
}
