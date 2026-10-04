using Microsoft.EntityFrameworkCore;

namespace PortfolioWebsite.Models
{
    /// <summary>
    /// Database-First EF Core context for ZeyyanNajeebDB.
    ///
    /// This mirrors what `Scaffold-DbContext` generates when you run it
    /// against the database created by Database/PortfolioDB_Script.sql:
    ///
    ///   Scaffold-DbContext "Server=YOUR_SERVER;Database=ZeyyanNajeebDB;
    ///     Trusted_Connection=True;TrustServerCertificate=True;"
    ///     Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    ///     -Context PortfolioDbContext -Force
    ///
    /// It is hand-written here so the project compiles and runs immediately,
    /// but it is 1:1 with the real database schema, so re-running the
    /// scaffold command above will regenerate an equivalent file.
    /// </summary>
    public partial class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PortfolioProfile> Profiles { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Experience> Experiences { get; set; } = null!;
        public virtual DbSet<Education> Education { get; set; } = null!;
        public virtual DbSet<Certification> Certifications { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioProfile>(entity =>
            {
                entity.ToTable("Profile");
                entity.HasKey(e => e.ProfileId);
                entity.Property(e => e.ProfileId).HasColumnName("ProfileId").ValueGeneratedOnAdd();
                entity.Property(e => e.FullName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.JobTitle).HasMaxLength(150);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.ProfileImageUrl).HasMaxLength(300);
                entity.Property(e => e.ResumeUrl).HasMaxLength(300);
                entity.Property(e => e.LinkedInUrl).HasMaxLength(300);
                entity.Property(e => e.GitHubUrl).HasMaxLength(300);
                entity.Property(e => e.TwitterUrl).HasMaxLength(300);
                entity.Property(e => e.InstagramUrl).HasMaxLength(300);
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Events");
                entity.HasKey(e => e.EventId);
                entity.Property(e => e.Title).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Location).HasMaxLength(150);
                entity.Property(e => e.ImageUrl).HasMaxLength(300);
                entity.Property(e => e.IsFeatured).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.ToTable("Experience");
                entity.HasKey(e => e.ExperienceId);
                entity.Property(e => e.JobTitle).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Company).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Location).HasMaxLength(150);
                entity.Property(e => e.IsCurrent).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("Education");
                entity.HasKey(e => e.EducationId);
                entity.Property(e => e.Degree).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Institution).HasMaxLength(150).IsRequired();
                entity.Property(e => e.IsCurrent).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Certification>(entity =>
            {
                entity.ToTable("Certifications");
                entity.HasKey(e => e.CertificationId);
                entity.Property(e => e.Title).HasMaxLength(150).IsRequired();
                entity.Property(e => e.IssuingOrganization).HasMaxLength(150).IsRequired();
                entity.Property(e => e.CredentialId).HasMaxLength(100);
                entity.Property(e => e.CredentialUrl).HasMaxLength(300);
                entity.Property(e => e.ImageUrl).HasMaxLength(300);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skills");
                entity.HasKey(e => e.SkillId);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.ProficiencyLevel).HasDefaultValue(80);
                entity.Property(e => e.IconClass).HasMaxLength(100);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(e => e.ProjectId);
                entity.Property(e => e.Title).HasMaxLength(150).IsRequired();
                entity.Property(e => e.TechStack).HasMaxLength(300);
                entity.Property(e => e.ProjectUrl).HasMaxLength(300);
                entity.Property(e => e.GitHubUrl).HasMaxLength(300);
                entity.Property(e => e.ImageUrl).HasMaxLength(300);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admins");
                entity.HasKey(e => e.AdminId);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PasswordHash).HasMaxLength(300).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages");
                entity.HasKey(e => e.MessageId);
                entity.Property(e => e.SenderName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.SenderEmail).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Subject).HasMaxLength(200);
                entity.Property(e => e.IsRead).HasDefaultValue(false);
                entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
