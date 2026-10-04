using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    /// <summary>Maps to dbo.Messages — contact form submissions from visitors.</summary>
    public class Message
    {
        public int MessageId { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Your Name")]
        public string SenderName { get; set; } = string.Empty;

        [Required, StringLength(100), EmailAddress]
        [Display(Name = "Your Email")]
        public string SenderEmail { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Body { get; set; } = string.Empty;

        public bool IsRead { get; set; }

        public DateTime SentAt { get; set; }
    }
}
