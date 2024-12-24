using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.DTO
{
    public class OrderDTO
    {
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal TotalAmount { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal DiscountAmount { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal NetAmount { get; set; }
        [Required]
        public string PaymentStatus { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string BillingAddress { get; set; }
        [Required]
        public string Notes { get; set; }
    }
}
