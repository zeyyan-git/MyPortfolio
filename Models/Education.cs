using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    /// <summary>Maps to dbo.Education — academic history shown on the public timeline.</summary>
    public class Education
    {
        public int EducationId { get; set; }

        [Required, StringLength(150)]
        [Display(Name = "Degree / Qualification")]
        public string Degree { get; set; } = string.Empty;

        [Required, StringLength(150)]
        [Display(Name = "Institution")]
        public string Institution { get; set; } = string.Empty;

        [Display(Name = "Details (grade, CGPA, honors, etc.)")]
        public string? Details { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date (leave blank if ongoing)")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Currently Studying Here")]
        public bool IsCurrent { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
