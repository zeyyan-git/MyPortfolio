namespace PortfolioWebsite.Models
{
    /// <summary>
    /// Maps to dbo.Profile — a single row holding Zeyyan Najeeb's public info.
    /// Class is named PortfolioProfile (not "Profile") to avoid clashing with
    /// System.Security.Claims / other framework types, matching what
    /// Scaffold-DbContext would generate if the table were called "Profile".
    /// </summary>
    public class PortfolioProfile
    {
        public int ProfileId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ResumeUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? GitHubUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
