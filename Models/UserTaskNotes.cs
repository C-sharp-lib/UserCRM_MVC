using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserCRM.Models
{
    public class UserTaskNotes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTaskNoteId { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int TaskId { get; set; }
        public Tasks Task {  get; set; }
        public int NoteId { get; set; }
        public Notes Note { get; set; }
    }
}
