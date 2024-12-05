using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.API.Extensions;
using TodoList.API.Middlewares;
using TodoList.Application;
using TodoList.Infrastructure;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<ExceptionsMiddleware>();

builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();

builder.Services.AddJwtAuthorization();

builder.Services.AddCorsConfiguration();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionsMiddleware>();

app.UseCors("CORS");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<TodoListContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    await context.Database.MigrateAsync();
    if(!userManager.Users.Any())
    {
        var admin = new User
        {
            BirthDate = DateOnly.FromDateTime(DateTime.MinValue),
            Email = "admin@todo-list.com",
            Firstname = "Admin",
            Lastname = "Admin",
            UserName = "admin"
        };
        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await context.Todos.AddAsync(new Todo 
        {
            Name = "Test",
            Content = "Test",
            UserId = admin.Id
        });
        await context.SaveChangesAsync();
    }
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
