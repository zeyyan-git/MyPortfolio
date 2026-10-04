using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    /// <summary>Maps to dbo.Certifications.</summary>
    public class Certification
    {
        public int CertificationId { get; set; }

        [Required, StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(150)]
        [Display(Name = "Issuing Organization")]
        public string IssuingOrganization { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Issue Date")]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Credential ID")]
        [StringLength(100)]
        public string? CredentialId { get; set; }

        [Display(Name = "Credential URL")]
        public string? CredentialUrl { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
