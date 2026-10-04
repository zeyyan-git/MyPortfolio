using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SkillsController : Controller
    {
        private readonly PortfolioDbContext _context;

        public SkillsController(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var skills = await _context.Skills.OrderByDescending(s => s.ProficiencyLevel).ToListAsync();
            return View(skills);
        }

        public IActionResult Create() => View(new Skill { ProficiencyLevel = 80 });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.Skills.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Skill added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Skill model)
        {
            if (id != model.SkillId) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();

            skill.Name = model.Name;
            skill.Category = model.Category;
            skill.ProficiencyLevel = model.ProficiencyLevel;
            skill.IconClass = model.IconClass;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Skill updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            return View(skill);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Skill removed.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
