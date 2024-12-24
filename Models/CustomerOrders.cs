using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserCRM.Models
{
    public class CustomerOrders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerOrderId { get; set; }
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
        public int OrderId { get; set; }
        public Orders Order { get; set; }
    }
}
