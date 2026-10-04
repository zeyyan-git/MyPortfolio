/* =========================================================================
   Zeyyan Najeeb - Dynamic Portfolio Website
   Database creation + seed script  (v2 — fresh database, new tables)
   Run this ENTIRE script in SQL Server Management Studio (SSMS 22)
   ========================================================================= */

IF DB_ID('ZeyyanNajeebDB') IS NULL
BEGIN
    CREATE DATABASE ZeyyanNajeebDB;
END
GO

USE ZeyyanNajeebDB;
GO

/* ---------------------------------------------------------------------
   1. Profile  (single-row table holding the owner's public info)
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Profile', 'U') IS NOT NULL DROP TABLE dbo.Profile;
GO
CREATE TABLE dbo.Profile (
    ProfileId        INT IDENTITY(1,1) PRIMARY KEY,
    FullName         NVARCHAR(100)  NOT NULL,
    JobTitle         NVARCHAR(150)  NULL,
    Bio              NVARCHAR(MAX)  NULL,
    Email            NVARCHAR(100)  NULL,
    Phone            NVARCHAR(20)   NULL,
    Address          NVARCHAR(200)  NULL,
    ProfileImageUrl  NVARCHAR(300)  NULL,
    ResumeUrl        NVARCHAR(300)  NULL,
    LinkedInUrl      NVARCHAR(300)  NULL,
    GitHubUrl        NVARCHAR(300)  NULL,
    TwitterUrl       NVARCHAR(300)  NULL,
    InstagramUrl     NVARCHAR(300)  NULL,
    UpdatedAt        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

/* ---------------------------------------------------------------------
   2. Experience  (work history)
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Experience', 'U') IS NOT NULL DROP TABLE dbo.Experience;
GO
CREATE TABLE dbo.Experience (
    ExperienceId  INT IDENTITY(1,1) PRIMARY KEY,
    JobTitle      NVARCHAR(150)  NOT NULL,
    Company       NVARCHAR(150)  NOT NULL,
    Description   NVARCHAR(MAX)  NULL,
    StartDate     DATE           NOT NULL,
    EndDate       DATE           NULL,
    IsCurrent     BIT            NOT NULL DEFAULT 0,
    Location      NVARCHAR(150)  NULL,
    CreatedAt     DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

/* ---------------------------------------------------------------------
   3. Education
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Education', 'U') IS NOT NULL DROP TABLE dbo.Education;
GO
CREATE TABLE dbo.Education (
    EducationId   INT IDENTITY(1,1) PRIMARY KEY,
    Degree        NVARCHAR(150)  NOT NULL,
    Institution   NVARCHAR(150)  NOT NULL,
    Details       NVARCHAR(300)  NULL,
    StartDate     DATE           NOT NULL,
    EndDate       DATE           NULL,
    IsCurrent     BIT            NOT NULL DEFAULT 0,
    CreatedAt     DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

/* ---------------------------------------------------------------------
   4. Events  (talks, hackathons, competitions, workshops attended/hosted)
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Events', 'U') IS NOT NULL DROP TABLE dbo.Events;
GO
CREATE TABLE dbo.Events (
    EventId      INT IDENTITY(1,1) PRIMARY KEY,
    Title        NVARCHAR(150)  NOT NULL,
    Description  NVARCHAR(MAX)  NULL,
    EventDate    DATETIME       NOT NULL,
    Location     NVARCHAR(150)  NULL,
    ImageUrl     NVARCHAR(300)  NULL,
    IsFeatured   BIT            NOT NULL DEFAULT 0,
    CreatedAt    DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

/* ---------------------------------------------------------------------
   5. Certifications
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Certifications', 'U') IS NOT NULL DROP TABLE dbo.Certifications;
GO
CREATE TABLE dbo.Certifications (
    CertificationId       INT IDENTITY(1,1) PRIMARY KEY,
    Title                 NVARCHAR(150)  NOT NULL,
    IssuingOrganization   NVARCHAR(150)  NOT NULL,
    IssueDate             DATE           NOT NULL,
    ExpiryDate            DATE           NULL,
    CredentialId          NVARCHAR(100)  NULL,
    CredentialUrl         NVARCHAR(300)  NULL,
    ImageUrl              NVARCHAR(300)  NULL,
    CreatedAt             DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

/* ---------------------------------------------------------------------
   6. Skills
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Skills', 'U') IS NOT NULL DROP TABLE dbo.Skills;
GO
CREATE TABLE dbo.Skills (
    SkillId           INT IDENTITY(1,1) PRIMARY KEY,
    Name              NVARCHAR(100)  NOT NULL,
    Category          NVARCHAR(100)  NULL,
    ProficiencyLevel  INT            NOT NULL DEFAULT 80,
    IconClass         NVARCHAR(100)  NULL
);
GO

/* ---------------------------------------------------------------------
   7. Projects
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL DROP TABLE dbo.Projects;
GO
CREATE TABLE dbo.Projects (
    ProjectId    INT IDENTITY(1,1) PRIMARY KEY,
    Title        NVARCHAR(150)  NOT NULL,
    Description  NVARCHAR(MAX)  NULL,
    TechStack    NVARCHAR(300)  NULL,
    ProjectUrl   NVARCHAR(300)  NULL,
    GitHubUrl    NVARCHAR(300)  NULL,
    ImageUrl     NVARCHAR(300)  NULL,
    CreatedAt    DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

/* ---------------------------------------------------------------------
   8. Admins  (used for the admin dashboard login)
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Admins', 'U') IS NOT NULL DROP TABLE dbo.Admins;
GO
CREATE TABLE dbo.Admins (
    AdminId       INT IDENTITY(1,1) PRIMARY KEY,
    Username      NVARCHAR(50)   NOT NULL UNIQUE,
    PasswordHash  NVARCHAR(300)  NOT NULL,
    Email         NVARCHAR(100)  NULL,
    CreatedAt     DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

/* ---------------------------------------------------------------------
   9. Messages  (contact form submissions from site visitors)
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Messages', 'U') IS NOT NULL DROP TABLE dbo.Messages;
GO
CREATE TABLE dbo.Messages (
    MessageId     INT IDENTITY(1,1) PRIMARY KEY,
    SenderName    NVARCHAR(100)  NOT NULL,
    SenderEmail   NVARCHAR(100)  NOT NULL,
    Subject       NVARCHAR(200)  NULL,
    Body          NVARCHAR(MAX)  NOT NULL,
    IsRead        BIT            NOT NULL DEFAULT 0,
    SentAt        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

/* =========================================================================
   SEED DATA — populated from Zeyyan Najeeb's resume
   ========================================================================= */

INSERT INTO dbo.Profile (FullName, JobTitle, Bio, Email, Phone, Address,
    ProfileImageUrl, ResumeUrl, LinkedInUrl, GitHubUrl, TwitterUrl, InstagramUrl)
VALUES (
    N'Zeyyan Najeeb',
    N'Full Stack Developer | MERN & React Native',
    N'Full Stack Developer with expertise in the MERN stack and React Native. I enjoy building responsive applications, REST APIs and modern, polished UIs — and I bring the same energy to customer support and software engineering fundamentals. Currently a Software Engineering student, I like turning ideas into real, working products end to end.',
    N'Najeebzeyyan@gmail.com',
    N'+92 340 2414292',
    N'Marghzar Colony, Lahore, Pakistan',
    N'/images/profile-placeholder.png',
    N'/files/Zeyyan_Najeeb_Resume.pdf',
    N'https://www.linkedin.com/in/zeyyan-najeeb-b2906238b/',
    N'https://github.com/zeyyan-najeeb',
    NULL,
    NULL
);
GO

/* ---- Skills — Technical + Interpersonal, from resume ---- */
INSERT INTO dbo.Skills (Name, Category, ProficiencyLevel, IconClass) VALUES
(N'MongoDB', N'Technical', 85, N'bi-database'),
(N'Express.js', N'Technical', 82, N'bi-hdd-network'),
(N'React.js', N'Technical', 90, N'bi-braces'),
(N'Node.js', N'Technical', 85, N'bi-diagram-3'),
(N'React Native', N'Technical', 83, N'bi-phone'),
(N'JavaScript', N'Technical', 90, N'bi-filetype-js'),
(N'Java', N'Technical', 70, N'bi-cup-hot'),
(N'C++ / C', N'Technical', 72, N'bi-code-slash'),
(N'HTML5 / Tailwind CSS', N'Technical', 88, N'bi-filetype-html'),
(N'MySQL', N'Technical', 78, N'bi-server'),
(N'REST API Development', N'Technical', 85, N'bi-plug'),
(N'WordPress Development', N'Technical', 70, N'bi-wordpress'),
(N'Data Structures & Algorithms', N'Technical', 80, N'bi-diagram-2'),
(N'OOP & UML / Software Architecture', N'Technical', 80, N'bi-boxes'),
(N'Communication Skills', N'Interpersonal', 90, N'bi-chat-dots'),
(N'Team Collaboration', N'Interpersonal', 90, N'bi-people'),
(N'Problem Solving', N'Interpersonal', 88, N'bi-lightbulb'),
(N'Time Management', N'Interpersonal', 85, N'bi-clock-history'),
(N'Leadership & Coordination', N'Interpersonal', 82, N'bi-flag');
GO

/* ---- Projects, from resume ---- */
INSERT INTO dbo.Projects (Title, Description, TechStack, ProjectUrl, GitHubUrl, ImageUrl) VALUES
(N'Library Management System', N'Backend of a full library management system built on the MERN stack — REST APIs, authentication and secure data handling with Node.js and Express.js, and a MongoDB database storing book and student information.', N'Node.js, Express.js, MongoDB, REST API', N'#', N'https://github.com/zeyyan-najeeb', N'/images/project-placeholder.png'),
(N'Multi-Platform Frontend Suite', N'Ongoing set of responsive, user-friendly interfaces built with React.js and React Native, using reusable components and modern UI design practices across web and mobile.', N'React.js, React Native, JavaScript', N'#', N'https://github.com/zeyyan-najeeb', N'/images/project-placeholder.png'),
(N'AI Models Portal', N'AI-powered web application built with Python, Flask, Scikit-learn and Hugging Face Transformers. Ships DBSCAN, K-Means, CNN image classification, sentiment analysis, question answering, text generation, translation, NER and the Apriori algorithm, with a Flask backend for datasets, images and text, plus audio I/O and interactive charts.', N'Python, Flask, Scikit-learn, Hugging Face Transformers', N'#', N'https://github.com/zeyyan-najeeb', N'/images/project-placeholder.png');
GO

/* ---- Experience, from resume ---- */
INSERT INTO dbo.Experience (JobTitle, Company, Description, StartDate, EndDate, IsCurrent, Location) VALUES
(N'eBay Dropshipping Manager', N'Self-Employed / eBay', N'Managing product research, listings, pricing strategies and order fulfillment. Handling customer support and optimizing store performance.', '2026-02-01', NULL, 1, N'Remote'),
(N'Customer Service Representative', N'Next Level Transportation', N'Assisted drivers and managed dispatch communication. Coordinated operations for timely deliveries and support.', '2022-12-01', '2023-05-31', 0, N'Lahore, Pakistan');
GO

/* ---- Education, from resume ---- */
INSERT INTO dbo.Education (Degree, Institution, Details, StartDate, EndDate, IsCurrent) VALUES
(N'BS Software Engineering (BSSE)', N'University of Central Punjab, Lahore', N'6th Semester | CGPA: 3.3', '2023-10-01', NULL, 1),
(N'Intermediate in Computer Science (ICS)', N'Punjab College, Lahore', N'Marks: 906 / 1100', '2020-02-01', '2022-04-30', 0);
GO

/* Certifications & Events are left empty — add real ones any time from
   the admin dashboard at /Admin/Certifications and /Admin/Events. */

/* Default admin login
   Username: zeyyan
   Password: Admin@123   (change this after first login!)
   PasswordHash below is SHA-256 of "Admin@123" */
INSERT INTO dbo.Admins (Username, PasswordHash, Email) VALUES
(N'zeyyan', N'e86f78a8a3caf0b60d8e74e5942aa6d86dc150cd3c03338aef25b7d2d7e3acc7', N'Najeebzeyyan@gmail.com');
GO

PRINT 'ZeyyanNajeebDB created and seeded successfully.';
