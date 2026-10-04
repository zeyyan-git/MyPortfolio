using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EventsController : Controller
    {
        private readonly PortfolioDbContext _context;

        public EventsController(PortfolioDbContext context)
        {
            _context = context;
        }

        // GET /Admin/Events
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.OrderByDescending(e => e.EventDate).ToListAsync();
            return View(events);
        }

        // GET /Admin/Events/Create
        public IActionResult Create() => View(new Event { EventDate = DateTime.Today });

        // POST /Admin/Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event model)
        {
            if (!ModelState.IsValid) return View(model);

            model.CreatedAt = DateTime.Now;
            _context.Events.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Event added successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET /Admin/Events/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return NotFound();
            return View(evt);
        }

        // POST /Admin/Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event model)
        {
            if (id != model.EventId) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return NotFound();

            evt.Title = model.Title;
            evt.Description = model.Description;
            evt.EventDate = model.EventDate;
            evt.Location = model.Location;
            evt.ImageUrl = model.ImageUrl;
            evt.IsFeatured = model.IsFeatured;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Event updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET /Admin/Events/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return NotFound();
            return View(evt);
        }

        // POST /Admin/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt != null)
            {
                _context.Events.Remove(evt);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Event removed.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
