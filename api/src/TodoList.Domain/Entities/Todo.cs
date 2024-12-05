namespace TodoList.Domain.Entities
{
    public class Todo : IBaseEntity<Guid>
    {
        public required string Name { get; set; }
        public string? Content { get; set; }
        public bool IsDone { get; set; }
        public Guid Id { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        // Navigation properties
        public Guid UserId { get; set; }
        //public User User { get; set; } = null!;
    }
}
