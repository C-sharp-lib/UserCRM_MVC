using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UserCRM.DTO;

public class UpdateProductDTO
{
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product upc is required")]
        public string UPC { get; set; }
        [Required(ErrorMessage = "Product description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Product price is required")]
        [Precision(10, 2)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Product currency type is required")]
        public string Currency { get; set; }
        [Required(ErrorMessage = "Product quantity is required")]
        public int QuantityInStock { get; set; } = 0;
        [Required(ErrorMessage = "Product category is required")]
        public string Category { get; set; } = string.Empty;
        [Required(ErrorMessage = "Product image is required")]
        public IFormFile ImageUrl { get; set; }
}