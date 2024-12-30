using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserCRM.Models;

namespace UserCRM.DTO;

public class UpdateUserDTO
{
    [DataType(DataType.Text)]
    public string FirstName { get; set; }
     [DataType(DataType.Text)]
    public string MiddleName {  get; set; }
    [DataType(DataType.Text)]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "No date specified for this field.")]
    
    public DateTime? DOB { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "No date specified for this field.")]
    public DateTime? HireDate { get; set; }
    public IFormFile? ImageUrl { get; set; }
    
    [DataType(DataType.Text)]
    public string? Role { get; set; }
    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [DataType(DataType.Text)]
    public string UserName { get; set; }
    
    [DataType(DataType.Text)]
    public string? Phone { get; set; }
    public DateTime UpdatedAt { get; set; }

    public UpdateUserDTO()
    {
        UpdatedAt = DateTime.Now;
    }
}