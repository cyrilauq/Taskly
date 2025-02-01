using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.API.Extensions;
using TodoList.API.Middlewares;
using TodoList.Application;
using TodoList.Infrastructure;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddTransient<ExceptionsMiddleware>();

builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices(configuration);

builder.Services.AddJwtAuthorization();

builder.Services.AddCorsConfiguration();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionsMiddleware>();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseCors("CORS");

app.UseHttpsRedirection();


app.MapWhen(ctx => !ctx.Request.Path.Value.Contains("/swagger/")  && !ctx.Request.Path.Value.Contains("/api/"), subApp =>
{
    subApp.UseRouting();
    subApp.UseEndpoints(endpoints =>
    {
        app.MapFallbackToController("Index", "Fallback");
    });
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<TodoListContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    await context.Database.MigrateAsync();
    if(!userManager.Users.Any())
    {
        await roleManager.CreateAsync(new Role { Name = "User" });
        await roleManager.CreateAsync(new Role {  Name = "Admin" });
        var admin = new User
        {
            BirthDate = DateOnly.FromDateTime(DateTime.MinValue),
            Email = "admin@todo-list.com",
            Firstname = "Admin",
            Lastname = "Admin",
            UserName = "admin"
        };
        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRolesAsync(admin, ["User", "Admin"]);
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
