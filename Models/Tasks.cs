using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class Tasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<UserTaskNotes> UserTaskNotes { get; set; }
        public ICollection<JobUserTasks> JobUserTasks { get; set; }
        public ICollection<CampaignUserTasks> CampaignUserTasks { get; set; }
        public string TruncateWordsThree(string text, int wordCount = 3)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length <= wordCount) return text;
            return string.Join(" ", words.Take(wordCount)) + "...";
        }
    }
}
