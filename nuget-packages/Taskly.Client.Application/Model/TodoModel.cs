using System.ComponentModel.DataAnnotations;

namespace Taskly.Client.Application.Model
{
    public class TodoModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 5)]
        public string Content { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 5)]
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public bool IsDone { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}

