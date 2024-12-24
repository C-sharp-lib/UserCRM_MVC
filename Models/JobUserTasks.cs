using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class JobUserTasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobUserTaskId { get; set; }
        public int JobId { get; set; }
        public Jobs Job { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int TaskId { get; set; }
        public Tasks Task { get; set; }
    }
}
