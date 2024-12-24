using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    [Table("CustomerUsers")]
    public class CustomerUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerUsersId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public Users User { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
    }
}
