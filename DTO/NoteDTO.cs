using UserCRM.Models;

namespace UserCRM.DTO
{
    public class NoteDTO
    {
        public string Note { get; set; }
        public NoteType NoteType { get; set; }
        public int RelatedEntiityId { get; set; }
        public string UserId { get; set; }
    }
}
