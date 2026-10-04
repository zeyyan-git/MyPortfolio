using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EducationController : Controller
    {
        private readonly PortfolioDbContext _context;

        public EducationController(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Education.OrderByDescending(e => e.StartDate).ToListAsync();
            return View(items);
        }

        public IActionResult Create() => View(new Education { StartDate = DateTime.Today });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Education model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.IsCurrent) model.EndDate = null;
            model.CreatedAt = DateTime.Now;
            _context.Education.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Education added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Education.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Education model)
        {
            if (id != model.EducationId) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var item = await _context.Education.FindAsync(id);
            if (item == null) return NotFound();

            item.Degree = model.Degree;
            item.Institution = model.Institution;
            item.Details = model.Details;
            item.StartDate = model.StartDate;
            item.EndDate = model.IsCurrent ? null : model.EndDate;
            item.IsCurrent = model.IsCurrent;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Education updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Education.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Education.FindAsync(id);
            if (item != null)
            {
                _context.Education.Remove(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Education removed.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
