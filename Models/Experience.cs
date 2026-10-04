using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    /// <summary>Maps to dbo.Experience — work history entries shown on the public timeline.</summary>
    public class Experience
    {
        public int ExperienceId { get; set; }

        [Required, StringLength(150)]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; } = string.Empty;

        [Required, StringLength(150)]
        [Display(Name = "Company / Organization")]
        public string Company { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date (leave blank if ongoing)")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Currently Working Here")]
        public bool IsCurrent { get; set; }

        [StringLength(150)]
        public string? Location { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
