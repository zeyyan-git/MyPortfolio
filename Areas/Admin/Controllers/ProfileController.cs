using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly PortfolioDbContext _context;

        public ProfileController(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Edit()
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync();
            profile ??= new PortfolioProfile { FullName = "Zeyyan Najeeb" };
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PortfolioProfile model)
        {
            if (!ModelState.IsValid) return View(model);

            var profile = await _context.Profiles.FirstOrDefaultAsync();
            if (profile == null)
            {
                model.UpdatedAt = DateTime.Now;
                _context.Profiles.Add(model);
            }
            else
            {
                profile.FullName = model.FullName;
                profile.JobTitle = model.JobTitle;
                profile.Bio = model.Bio;
                profile.Email = model.Email;
                profile.Phone = model.Phone;
                profile.Address = model.Address;
                profile.ProfileImageUrl = model.ProfileImageUrl;
                profile.ResumeUrl = model.ResumeUrl;
                profile.LinkedInUrl = model.LinkedInUrl;
                profile.GitHubUrl = model.GitHubUrl;
                profile.TwitterUrl = model.TwitterUrl;
                profile.InstagramUrl = model.InstagramUrl;
                profile.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Profile updated successfully.";
            return RedirectToAction(nameof(Edit));
        }
    }
}
