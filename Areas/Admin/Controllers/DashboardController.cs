using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly PortfolioDbContext _context;

        public DashboardController(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.EventCount = await _context.Events.CountAsync();
            ViewBag.ExperienceCount = await _context.Experiences.CountAsync();
            ViewBag.EducationCount = await _context.Education.CountAsync();
            ViewBag.CertificationCount = await _context.Certifications.CountAsync();
            ViewBag.SkillCount = await _context.Skills.CountAsync();
            ViewBag.ProjectCount = await _context.Projects.CountAsync();
            ViewBag.UnreadMessageCount = await _context.Messages.CountAsync(m => !m.IsRead);
            ViewBag.AdminUsername = User.Identity?.Name;

            return View();
        }
    }
}
