using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using UserCRM.Models;

namespace UserCRM.DTO
{
    public class UserDTO : Users
    {
        [NotMapped]
        public string FirstName { get; set; }
        [NotMapped]
        public string MiddleName { get; set; }
        [NotMapped]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }
        
        public bool IsActive { get; set; } = true;
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime HireDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
