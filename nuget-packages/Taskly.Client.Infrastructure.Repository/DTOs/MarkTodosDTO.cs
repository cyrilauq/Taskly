namespace Taskly.Client.Infrastructure.Repository.DTOs
{
    internal class MarkTodosDTO
    {
        public required IEnumerable<Guid> TodoIds { get; set; }
        public required bool IsDone { get; set; }
    }
}
