using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UserCRM.DTO
{
    public class CampaignDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal Budget { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal Spend { get; set; }
        [Required]
        public string TargetAudience { get; set; }
        [Required]
        public string Channel { get; set; }
        [Required]
        public string Goals { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal RevenueTarget { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal ActualRevenue { get; set; }
        [Required]
        public long Impressions { get; set; }
        [Required]
        public long Clicks { get; set; }
        [Required]
        public int LeadsGenerated { get; set; }
        [Required]
        public int Conversions { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal ROI { get; set; }
    }
}
