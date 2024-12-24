using UserCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.DTO
{
    public class CustomerDTO
    {
        [Required]
        public string CustomerType { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Fax { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Industry { get; set; }
        [Required]
        public string Website { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string UpdatedBy { get; set; } = string.Empty;
        [Required]
        public string Notes { get; set; }
    }
}
