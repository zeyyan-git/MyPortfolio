using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    /// <summary>Maps to dbo.Projects.</summary>
    public class Project
    {
        public int ProjectId { get; set; }

        [Required, StringLength(150)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Display(Name = "Tech Stack")]
        [StringLength(300)]
        public string? TechStack { get; set; }

        [Display(Name = "Live URL")]
        public string? ProjectUrl { get; set; }

        [Display(Name = "GitHub URL")]
        public string? GitHubUrl { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
