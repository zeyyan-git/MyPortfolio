using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CertificationsController : Controller
    {
        private readonly PortfolioDbContext _context;

        public CertificationsController(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var certs = await _context.Certifications.OrderByDescending(c => c.IssueDate).ToListAsync();
            return View(certs);
        }

        public IActionResult Create() => View(new Certification { IssueDate = DateTime.Today });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Certification model)
        {
            if (!ModelState.IsValid) return View(model);

            model.CreatedAt = DateTime.Now;
            _context.Certifications.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Certification added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cert = await _context.Certifications.FindAsync(id);
            if (cert == null) return NotFound();
            return View(cert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Certification model)
        {
            if (id != model.CertificationId) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var cert = await _context.Certifications.FindAsync(id);
            if (cert == null) return NotFound();

            cert.Title = model.Title;
            cert.IssuingOrganization = model.IssuingOrganization;
            cert.IssueDate = model.IssueDate;
            cert.ExpiryDate = model.ExpiryDate;
            cert.CredentialId = model.CredentialId;
            cert.CredentialUrl = model.CredentialUrl;
            cert.ImageUrl = model.ImageUrl;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Certification updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cert = await _context.Certifications.FindAsync(id);
            if (cert == null) return NotFound();
            return View(cert);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cert = await _context.Certifications.FindAsync(id);
            if (cert != null)
            {
                _context.Certifications.Remove(cert);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Certification removed.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
