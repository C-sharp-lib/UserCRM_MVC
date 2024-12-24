using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UserCRM.Models;

namespace UserCRM.DTO
{
    public class TaskDTO
    {
        [Required]
        public int RelatedEntityId { get; set; }
        [Required]
        public TaskType TaskType { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Priority { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime? CompletedDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
