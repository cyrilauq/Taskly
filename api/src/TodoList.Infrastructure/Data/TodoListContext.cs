using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.Infrastructure.Entities;

namespace TodoList.Infrastructure.Data
{
    public class TodoListContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Todo> Todos { get; set; }

        public string DbPath { get; }

        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
