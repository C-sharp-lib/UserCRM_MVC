using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class Notes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
        public string Note {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<CampaignUserNotes> CampaignUserNotes { get; set; }
        public ICollection<JobUserNotes> JobUserNotes { get; set; }
        public ICollection<UserTaskNotes> UserTaskNotes { get; set; }
    }
}
