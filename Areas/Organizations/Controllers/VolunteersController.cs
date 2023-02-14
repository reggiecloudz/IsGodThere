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
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Volunteers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Volunteers.Include(v => v.Member).Include(v => v.Opportunity);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Volunteers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Volunteers == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.Member)
                .Include(v => v.Opportunity)
                .FirstOrDefaultAsync(m => m.OpportunityId == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // GET: Volunteers/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Id");
            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,OpportunityId")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volunteer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", volunteer.MemberId);
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Id", volunteer.OpportunityId);
            return View(volunteer);
        }

        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Volunteers == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", volunteer.MemberId);
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Id", volunteer.OpportunityId);
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MemberId,OpportunityId")] Volunteer volunteer)
        {
            if (id != volunteer.OpportunityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerExists(volunteer.OpportunityId))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", volunteer.MemberId);
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Id", volunteer.OpportunityId);
            return View(volunteer);
        }

        // GET: Volunteers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Volunteers == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.Member)
                .Include(v => v.Opportunity)
                .FirstOrDefaultAsync(m => m.OpportunityId == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Volunteers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Volunteers'  is null.");
            }
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerExists(long id)
        {
          return (_context.Volunteers?.Any(e => e.OpportunityId == id)).GetValueOrDefault();
        }
    }
}
