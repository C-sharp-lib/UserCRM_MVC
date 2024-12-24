using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class CustomerSegments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SegmentId { get; set; }
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
        public string? SegmentName { get; set; } = string.Empty;
    }
}
