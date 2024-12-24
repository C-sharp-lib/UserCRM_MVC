using UserCRM.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.DTO
{
    public class OrderItemDTO
    {
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Precision(10, 2)]
        public decimal UnitPrice { get; set; }
        [Precision(10, 2)]
        public decimal TotalPrice { get; set; }
    }
}
