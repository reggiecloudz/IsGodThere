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
    public class CausesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CausesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Causes
        [Route("/[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
              return _context.Causes != null ? 
                          View(await _context.Causes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Causes'  is null.");
        }

        // GET: Causes/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cause == null)
            {
                return NotFound();
            }

            return View(cause);
        }

        // GET: Causes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Causes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Details")] Cause cause)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cause);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cause);
        }

        // GET: Causes/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }
            return View(cause);
        }

        // POST: Causes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Details")] Cause cause)
        {
            if (id != cause.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cause);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseExists(cause.Id))
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
            return View(cause);
        }

        // GET: Causes/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cause == null)
            {
                return NotFound();
            }

            return View(cause);
        }

        // POST: Causes/Delete/5
        [HttpPost("{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Causes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Causes'  is null.");
            }
            var cause = await _context.Causes.FindAsync(id);
            if (cause != null)
            {
                _context.Causes.Remove(cause);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CauseExists(long id)
        {
          return (_context.Causes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
