using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; // Security
using VintageGameStore.Data;
using VintageGameStore.Models;

namespace VintageGameStore.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games - Public Access
        public async Task<IActionResult> Index(string? searchString)
        {
            var games = _context.Games.Include(g => g.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Null-safe search logic
                games = games.Where(s => s.Title != null && s.Title.Contains(searchString));
            }

            return View(await games.ToListAsync());
        }

        // GET: Games/Details/5 - Public Access
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null) return NotFound();

            return View(game);
        }

        // GET: Games/Create - Protected
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Games/Create - Protected
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,ReleaseYear,CategoryId")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", game.CategoryId);
            return View(game);
        }

        // GET: Games/Edit/5 - Protected
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games.FindAsync(id);
            if (game == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", game.CategoryId);
            return View(game);
        }

        // POST: Games/Edit/5 - Protected
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,ReleaseYear,CategoryId")] Game game)
        {
            if (id != game.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Games.Any(e => e.Id == game.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", game.CategoryId);
            return View(game);
        }

        // GET: Games/Delete/5 - Protected
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null) return NotFound();

            return View(game);
        }

        // POST: Games/Delete/5 - Protected
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}