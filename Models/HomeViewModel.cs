namespace PortfolioWebsite.Models
{
    /// <summary>Aggregates everything the public homepage needs in one query set.</summary>
    public class HomeViewModel
    {
        public PortfolioProfile? Profile { get; set; }
        public List<Skill> Skills { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
        public List<Event> Events { get; set; } = new();
        public List<Experience> Experiences { get; set; } = new();
        public List<Education> Education { get; set; } = new();
        public List<Certification> Certifications { get; set; } = new();
        public Message ContactForm { get; set; } = new();
    }
}
