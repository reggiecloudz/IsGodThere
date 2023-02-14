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
    public class OrganizersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organizers
        [Route("/[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Organizers.Include(o => o.Identity).Include(o => o.Organization);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Organizers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Organizers == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers
                .Include(o => o.Identity)
                .Include(o => o.Organization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View(organizer);
        }

        // GET: Organizers/Create
        public IActionResult Create()
        {
            ViewData["IdentityId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Id");
            return View();
        }

        // POST: Organizers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Role,OrganizationId,Id,IdentityId")] Organizer organizer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityId"] = new SelectList(_context.Users, "Id", "Id", organizer.IdentityId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Id", organizer.OrganizationId);
            return View(organizer);
        }

        // GET: Organizers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Organizers == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer == null)
            {
                return NotFound();
            }
            ViewData["IdentityId"] = new SelectList(_context.Users, "Id", "Id", organizer.IdentityId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Id", organizer.OrganizationId);
            return View(organizer);
        }

        // POST: Organizers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Role,OrganizationId,Id,IdentityId")] Organizer organizer)
        {
            if (id != organizer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizerExists(organizer.Id))
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
            ViewData["IdentityId"] = new SelectList(_context.Users, "Id", "Id", organizer.IdentityId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Id", organizer.OrganizationId);
            return View(organizer);
        }

        // GET: Organizers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Organizers == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers
                .Include(o => o.Identity)
                .Include(o => o.Organization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View(organizer);
        }

        // POST: Organizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Organizers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Organizers'  is null.");
            }
            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer != null)
            {
                _context.Organizers.Remove(organizer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizerExists(string id)
        {
          return (_context.Organizers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
