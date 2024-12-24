using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class JobUserNotes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobUserNoteId { get; set; }
        public int JobId { get; set; }
        public Jobs Job {  get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int NoteId { get; set; }
        public Notes Notes { get; set; }
    }
}
