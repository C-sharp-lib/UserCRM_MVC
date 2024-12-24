using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserCRM.Models
{
    [Table("CustomerJobs")]
    public class CustomerJobs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerJobsId { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customers Customers { get; set; }
        [ForeignKey("JobId")]
        public int JobId { get; set; }
        public Jobs Jobs { get; set; }
    }
}
