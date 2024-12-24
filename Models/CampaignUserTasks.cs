using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class CampaignUserTasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampaignUserTaskId { get; set; }
        public int CampaignId { get; set; }
        public Campaigns Campaign {  get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int TaskId { get; set; }
        public Tasks Task {  get; set; }
    }
}
