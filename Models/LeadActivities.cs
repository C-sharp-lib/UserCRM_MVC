using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserCRM.Models
{
    public class LeadActivities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeadActivitiesId { get; set; }
        public int LeadId { get; set; }
        public Leads Lead { get; set; }
        public string ActivityType { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public Users CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
