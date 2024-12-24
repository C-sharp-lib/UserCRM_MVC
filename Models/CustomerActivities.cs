using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class CustomerActivities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }
        public int CustomerId { get; set; }
        public string ActivityType { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string? Notes { get; set; } = string.Empty;
    }
}
