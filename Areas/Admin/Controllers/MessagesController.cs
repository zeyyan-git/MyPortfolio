using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly PortfolioDbContext _context;

        public MessagesController(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _context.Messages.OrderByDescending(m => m.SentAt).ToListAsync();
            return View(messages);
        }

        public async Task<IActionResult> Details(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null) return NotFound();

            if (!message.IsRead)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Message deleted.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
