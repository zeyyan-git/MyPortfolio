using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ExperienceController : Controller
    {
        private readonly PortfolioDbContext _context;

        public ExperienceController(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Experiences.OrderByDescending(e => e.StartDate).ToListAsync();
            return View(items);
        }

        public IActionResult Create() => View(new Experience { StartDate = DateTime.Today });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Experience model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.IsCurrent) model.EndDate = null;
            model.CreatedAt = DateTime.Now;
            _context.Experiences.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Experience added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Experiences.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Experience model)
        {
            if (id != model.ExperienceId) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var item = await _context.Experiences.FindAsync(id);
            if (item == null) return NotFound();

            item.JobTitle = model.JobTitle;
            item.Company = model.Company;
            item.Description = model.Description;
            item.StartDate = model.StartDate;
            item.EndDate = model.IsCurrent ? null : model.EndDate;
            item.IsCurrent = model.IsCurrent;
            item.Location = model.Location;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Experience updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Experiences.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Experiences.FindAsync(id);
            if (item != null)
            {
                _context.Experiences.Remove(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Experience removed.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
