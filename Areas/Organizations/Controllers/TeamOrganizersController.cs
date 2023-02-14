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
    public class TeamOrganizersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamOrganizersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamOrganizers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeamOrganizers.Include(t => t.Organizer).Include(t => t.Team);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeamOrganizers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TeamOrganizers == null)
            {
                return NotFound();
            }

            var teamOrganizer = await _context.TeamOrganizers
                .Include(t => t.Organizer)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (teamOrganizer == null)
            {
                return NotFound();
            }

            return View(teamOrganizer);
        }

        // GET: TeamOrganizers/Create
        public IActionResult Create()
        {
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id");
            return View();
        }

        // POST: TeamOrganizers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsLead,OrganizerId,TeamId")] TeamOrganizer teamOrganizer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamOrganizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Id", teamOrganizer.OrganizerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", teamOrganizer.TeamId);
            return View(teamOrganizer);
        }

        // GET: TeamOrganizers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TeamOrganizers == null)
            {
                return NotFound();
            }

            var teamOrganizer = await _context.TeamOrganizers.FindAsync(id);
            if (teamOrganizer == null)
            {
                return NotFound();
            }
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Id", teamOrganizer.OrganizerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", teamOrganizer.TeamId);
            return View(teamOrganizer);
        }

        // POST: TeamOrganizers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IsLead,OrganizerId,TeamId")] TeamOrganizer teamOrganizer)
        {
            if (id != teamOrganizer.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamOrganizer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamOrganizerExists(teamOrganizer.TeamId))
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
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Id", teamOrganizer.OrganizerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", teamOrganizer.TeamId);
            return View(teamOrganizer);
        }

        // GET: TeamOrganizers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TeamOrganizers == null)
            {
                return NotFound();
            }

            var teamOrganizer = await _context.TeamOrganizers
                .Include(t => t.Organizer)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (teamOrganizer == null)
            {
                return NotFound();
            }

            return View(teamOrganizer);
        }

        // POST: TeamOrganizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TeamOrganizers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TeamOrganizers'  is null.");
            }
            var teamOrganizer = await _context.TeamOrganizers.FindAsync(id);
            if (teamOrganizer != null)
            {
                _context.TeamOrganizers.Remove(teamOrganizer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamOrganizerExists(long id)
        {
          return (_context.TeamOrganizers?.Any(e => e.TeamId == id)).GetValueOrDefault();
        }
    }
}
