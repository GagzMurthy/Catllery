using Microsoft.AspNetCore.Mvc;
using Catllery.Data;
using Catllery.Models;
using Microsoft.EntityFrameworkCore;

namespace Catllery.Controllers
{
    public class CatPhotosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatPhotosController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {
            var catPhotos = await _context.CatPhotos.ToListAsync();
            return View(catPhotos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,Caption")] CatPhoto catPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catPhoto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catPhoto = await _context.CatPhotos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catPhoto == null)
            {
                return NotFound();
            }

            return View(catPhoto);
        }

                
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catPhoto = await _context.CatPhotos.FindAsync(id);
            if (catPhoto == null)
            {
                return NotFound();
            }

            _context.CatPhotos.Remove(catPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catPhoto = await _context.CatPhotos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catPhoto == null)
            {
                return NotFound();
            }

            return View(catPhoto);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Caption")] CatPhoto catPhoto)
        {
            if (id != catPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingCatPhoto = await _context.CatPhotos.FindAsync(id);
                if (existingCatPhoto == null)
                {
                    return NotFound();
                }

                // Update the Caption property
                existingCatPhoto.Caption = catPhoto.Caption;

                // Update the context with the modified object
                //_context.Update(existingCatPhoto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Log ModelState errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                // Log or inspect the error
                Console.WriteLine(error.ErrorMessage);
            }

            // If we got this far, something failed; redisplay the form with the current model
            return View(catPhoto);
        }


    }
}
