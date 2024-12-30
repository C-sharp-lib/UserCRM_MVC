using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.DTO
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UPC { get; set; }
        public string Description { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal Price { get; set; }
        public string? Currency { get; set; }
        [Required]
        public int QuantityInStock { get; set; } = 0;
        public string Category { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
