using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly PortfolioDbContext _context;

        public ProjectsController(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync();
            return View(projects);
        }

        public IActionResult Create() => View(new Project());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project model)
        {
            if (!ModelState.IsValid) return View(model);

            model.CreatedAt = DateTime.Now;
            _context.Projects.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Project added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project model)
        {
            if (id != model.ProjectId) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            project.Title = model.Title;
            project.Description = model.Description;
            project.TechStack = model.TechStack;
            project.ProjectUrl = model.ProjectUrl;
            project.GitHubUrl = model.GitHubUrl;
            project.ImageUrl = model.ImageUrl;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Project updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Project removed.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
