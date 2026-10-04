using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    /// <summary>Maps to dbo.Events.</summary>
    public class Event
    {
        public int EventId { get; set; }

        [Required, StringLength(150)]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [StringLength(150)]
        public string? Location { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Featured")]
        public bool IsFeatured { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
