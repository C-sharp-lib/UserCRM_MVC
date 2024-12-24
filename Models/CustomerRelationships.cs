using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.Models
{
    public class CustomerRelationships
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RelationshipId { get; set; }
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
        public string RelatedCustomerId { get; set; }
        public Customers RelatedCustomer { get; set; }
        public string? RelationshipType { get; set; } = string.Empty;
    }
}
