using UserCRM.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.DTO
{
    public class JobDTO
    {
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobDescription { get; set; } = string.Empty;
        [Required]
        public string JobStatus { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Priority { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal EstimatedCost { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal ActualCost { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
