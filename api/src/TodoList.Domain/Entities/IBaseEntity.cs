namespace TodoList.Domain.Entities;

public interface IBaseEntity<TId>
{
    public TId Id { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
