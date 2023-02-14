using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IsGodThere.Data;
using IsGodThere.Domain;

namespace IsGodThere.Areas.Organizations.Controllers
{
    [Area("Organizations")]
    [Route("[area]/[controller]/[action]")]
    public class ForumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forums
        [Route("/[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
              return _context.Forums != null ? 
                          View(await _context.Forums.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Forums'  is null.");
        }

        // GET: Forums/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        // GET: Forums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Forums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forum);
        }

        // GET: Forums/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums.FindAsync(id);
            if (forum == null)
            {
                return NotFound();
            }
            return View(forum);
        }

        // POST: Forums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description")] Forum forum)
        {
            if (id != forum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumExists(forum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(forum);
        }

        // GET: Forums/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        // POST: Forums/Delete/5
        [HttpPost("{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Forums == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Forums'  is null.");
            }
            var forum = await _context.Forums.FindAsync(id);
            if (forum != null)
            {
                _context.Forums.Remove(forum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumExists(long id)
        {
          return (_context.Forums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
