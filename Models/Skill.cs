using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    /// <summary>Maps to dbo.Skills.</summary>
    public class Skill
    {
        public int SkillId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Category { get; set; }

        [Range(0, 100)]
        [Display(Name = "Proficiency (%)")]
        public int ProficiencyLevel { get; set; } = 80;

        [Display(Name = "Bootstrap Icon Class")]
        public string? IconClass { get; set; }
    }
}
