using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class Customers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string CustomerType { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Industry { get; set; }
        public string Website { get; set; }
        public string ContactPerson { get; set; }
        public ICollection<CustomerJobs> CustomerJobs { get; set; }
        public ICollection<CustomerUsers> CustomerUsers { get; set; }
        public ICollection<CustomerOrders> CustomerOrders { get; set; }

        public string Notes { get; set; }
    }
}
