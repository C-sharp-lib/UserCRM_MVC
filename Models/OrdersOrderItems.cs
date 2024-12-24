using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserCRM.Models
{
    public class OrdersOrderItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdersOrderItemsId { get; set; }
        public int OrderId { get; set; }
        public Orders Order { get; set; }
        public int OrderItemId { get; set; }
        public OrderItems OrderItem { get; set; }
    }
}
