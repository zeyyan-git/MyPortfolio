using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly PortfolioDbContext _context;

        public HomeController(PortfolioDbContext context)
        {
            _context = context;
        }

        // GET /  -> full public portfolio page
        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                Profile = await _context.Profiles.FirstOrDefaultAsync(),
                Skills = await _context.Skills.OrderByDescending(s => s.ProficiencyLevel).ToListAsync(),
                Projects = await _context.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync(),
                Events = await _context.Events.OrderByDescending(e => e.EventDate).ToListAsync(),
                Experiences = await _context.Experiences.OrderByDescending(e => e.StartDate).ToListAsync(),
                Education = await _context.Education.OrderByDescending(e => e.StartDate).ToListAsync(),
                Certifications = await _context.Certifications.OrderByDescending(c => c.IssueDate).ToListAsync()
            };

            return View(vm);
        }

        // POST /Home/Contact -> visitor submits the contact form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Message contactForm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ContactError"] = "Please fill in all required fields correctly.";
                return Redirect(Url.Action(nameof(Index)) + "#contact");
            }

            contactForm.SentAt = DateTime.Now;
            contactForm.IsRead = false;
            _context.Messages.Add(contactForm);
            await _context.SaveChangesAsync();

            TempData["ContactSuccess"] = "Thanks for reaching out! I'll get back to you soon.";
            return Redirect(Url.Action(nameof(Index)) + "#contact");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
