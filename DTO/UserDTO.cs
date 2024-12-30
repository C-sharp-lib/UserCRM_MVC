using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using UserCRM.Models;

namespace UserCRM.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Middle name is required")]
        [DataType(DataType.Text)]
        public string MiddleName {  get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime? DOB { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Hire date is required")]
        public DateTime? HireDate { get; set; }
        [Required(ErrorMessage = "Image URL is required")]
        public IFormFile? ImageUrl { get; set; }
        [Required(ErrorMessage = "Role is required")]
        [DataType(DataType.Text)]
        public string? Role { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UserDTO()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
