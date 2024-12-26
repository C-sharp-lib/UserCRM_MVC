using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace UserCRM.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName {  get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DOB { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? HireDate { get; set; }

        public string? ImageUrl { get; set; }
        public string? Role { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<CustomerUsers> CustomerUsers { get; set; }
        public ICollection<UserTaskNotes> UserTaskNotes { get; set; }
        public ICollection<CampaignUserNotes> CampaignUserNotes { get; set; }
        public ICollection<JobUserNotes> JobUserNotes { get; set; }
        public ICollection<JobUserTasks> JobUserTasks { get; set; }
        public ICollection<CampaignUserTasks> CampaignUserTasks { get; set; }
    }
}

