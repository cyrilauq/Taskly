namespace Taskly.Web.Infrastructure.DTO
{
    public class TodoDTO(string id, string content, string name, Guid userId, bool isDone, DateTime? createdOn, DateTime? deletedOn)
    {
        public string Id { get; set; } = id;
        public string Content { get; set; } = content;
        public string Name { get; set; } = name;
        public Guid UserId { get; set; } = userId;
        public bool IsDone { get; set; } = isDone;
        public DateTime? CreatedOn { get; set; } = createdOn;
        public DateTime? DeletedOn { get; set; } = deletedOn;
    }
}
