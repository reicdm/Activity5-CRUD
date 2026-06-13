using Microsoft.AspNetCore.Mvc;
using Activity5_CRUD.Data;
using Activity5_CRUD.Models;
using Activity5_CRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Activity5_CRUD.Controllers
{
    public class ArtController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ArtController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Entries()
        {
            var viewModel = new ArtViewModel
            {
                ArtList = await dbContext.Arts.ToListAsync(),
                AddEntry = new AddEntryViewModel()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Entries(ArtViewModel viewModel)
        {
            string imageUrl = "/images/sidebar/gallery_icon.png";

            if (viewModel.AddEntry.ImageUrl != null && viewModel.AddEntry.ImageUrl.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(viewModel.AddEntry.ImageUrl.FileName).ToLower();

                if (allowedExtensions.Contains(extension))
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid() + extension;
                    var savePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await viewModel.AddEntry.ImageUrl.CopyToAsync(stream);
                    }

                    imageUrl = "/images/uploads/" + fileName;
                }
            }

            var art = new Art()
            {
                Id = Guid.NewGuid(),
                ArtName = viewModel.AddEntry.ArtName,
                ImageUrl = imageUrl,
                Medium = viewModel.AddEntry.Medium,
                Date = viewModel.AddEntry.Date
            };

            await dbContext.Arts.AddAsync(art);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Entries");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var art = await dbContext.Arts.FindAsync(id);
            if (art != null)
            {
                dbContext.Arts.Remove(art);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Entries");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArtViewModel viewModel)
        {
            var entry = viewModel.EditEntry;
            var art = await dbContext.Arts.FindAsync(entry.Id);

            if (art == null) return NotFound();

            art.ArtName = entry.ArtName;
            art.Medium = entry.Medium;
            art.Date = entry.Date;

            if (entry.ImageUrl != null && entry.ImageUrl.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(entry.ImageUrl.FileName).ToLower();

                if (allowedExtensions.Contains(extension))
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid() + extension;
                    var savePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await entry.ImageUrl.CopyToAsync(stream);
                    }

                    art.ImageUrl = "/images/uploads/" + fileName;
                }
            }
            else
            {
                // Keep existing image if no new file uploaded
                art.ImageUrl = entry.ExistingImageUrl;
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction("Entries");
        }
    }
}