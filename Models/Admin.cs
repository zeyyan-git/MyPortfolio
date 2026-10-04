using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    /// <summary>Maps to dbo.Admins — accounts allowed into the admin dashboard.</summary>
    public class Admin
    {
        public int AdminId { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
