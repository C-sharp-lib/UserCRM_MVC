using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserCRM.Models
{
    public class OrderItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Products Products { get; set; }
        public int Quantity { get; set; }
        [Precision(10,2)]
        public decimal UnitPrice { get; set; }
        [Precision(10,2)]
        public decimal TotalPrice { get; set; }
        public ICollection<OrdersOrderItems> OrdersOrderItems { get; set; }
    }
}
