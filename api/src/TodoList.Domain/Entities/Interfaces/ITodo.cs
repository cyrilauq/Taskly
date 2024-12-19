namespace TodoList.Domain.Entities.Interfaces
{
    public interface ITodo : IBaseEntity<Guid>
    {
        public string Name { get; set; }
        public string? Content { get; set; }
        public bool IsDone { get; set; }
        public Guid UserId { get; set; }
    }
}
