namespace TodoList.Domain.Entities.Interfaces
{
    public interface IUser : IBaseEntity<Guid>
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
