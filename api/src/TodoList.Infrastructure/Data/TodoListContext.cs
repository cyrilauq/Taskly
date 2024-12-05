using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.Infrastructure.Entities;

namespace TodoList.Infrastructure.Data
{
    public class TodoListContext(DbContextOptions<TodoListContext> options) : 
        IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(options)
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<Todo>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Todo>()
                .Property(t => t.CreatedOn)
                .HasDefaultValueSql("date('now')")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Todo>()
                .Property(t => t.UpdatedOn)
                .HasDefaultValueSql("date('now')")
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Todo>()
                .HasOne(s => s.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId)
                .IsRequired();
        }
    }
}
