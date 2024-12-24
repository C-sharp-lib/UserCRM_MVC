using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using UserCRM.Models;

namespace UserCRM.DTO
{
    public class UserDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotMapped]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public bool? IsActive { get; set; } = true;
        public DateTime? HireDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}
