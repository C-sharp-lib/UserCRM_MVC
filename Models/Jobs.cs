using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class Jobs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; } = string.Empty;
        public string JobStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Priority { get; set; }
        [Precision(10,2)]
        public decimal EstimatedCost { get; set; }
        [Precision(10,2)]
        public decimal ActualCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public ICollection<CustomerJobs> CustomerJobs { get; set; }
        public ICollection<JobUserNotes> JobUserNotes { get; set; }
        public ICollection<JobUserTasks> JobUserTasks { get; set; }
    }
}
