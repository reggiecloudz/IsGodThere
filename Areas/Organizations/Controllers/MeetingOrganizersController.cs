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
    [Route("[area]/Meeting-Organizers/[action]")]
    public class MeetingOrganizersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingOrganizersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MeetingOrganizers
        [Route("/[area]/Meeting-Organizers")]
        [Route("/[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MeetingOrganizers.Include(m => m.Meeting).Include(m => m.Organizer).ThenInclude(m => m!.Identity);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MeetingOrganizers/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.MeetingOrganizers == null)
            {
                return NotFound();
            }

            var meetingOrganizer = await _context.MeetingOrganizers
                .Include(m => m.Meeting)
                .Include(m => m.Organizer)
                    .ThenInclude(m => m!.Identity)
                .FirstOrDefaultAsync(m => m.MeetingId == id);
            if (meetingOrganizer == null)
            {
                return NotFound();
            }

            return View(meetingOrganizer);
        }

        // GET: MeetingOrganizers/Create
        public IActionResult Create()
        {
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id");
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Id");
            return View();
        }

        // POST: MeetingOrganizers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsLead,OrganizerId,MeetingId")] MeetingOrganizer meetingOrganizer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetingOrganizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id", meetingOrganizer.MeetingId);
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Id", meetingOrganizer.OrganizerId);
            return View(meetingOrganizer);
        }

        // GET: MeetingOrganizers/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.MeetingOrganizers == null)
            {
                return NotFound();
            }

            var meetingOrganizer = await _context.MeetingOrganizers.FindAsync(id);
            if (meetingOrganizer == null)
            {
                return NotFound();
            }
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id", meetingOrganizer.MeetingId);
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Id", meetingOrganizer.OrganizerId);
            return View(meetingOrganizer);
        }

        // POST: MeetingOrganizers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IsLead,OrganizerId,MeetingId")] MeetingOrganizer meetingOrganizer)
        {
            if (id != meetingOrganizer.MeetingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingOrganizer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingOrganizerExists(meetingOrganizer.MeetingId))
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
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id", meetingOrganizer.MeetingId);
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Id", meetingOrganizer.OrganizerId);
            return View(meetingOrganizer);
        }

        // GET: MeetingOrganizers/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.MeetingOrganizers == null)
            {
                return NotFound();
            }

            var meetingOrganizer = await _context.MeetingOrganizers
                .Include(m => m.Meeting)
                .Include(m => m.Organizer)
                    .ThenInclude(m => m!.Identity)
                .FirstOrDefaultAsync(m => m.MeetingId == id);
            if (meetingOrganizer == null)
            {
                return NotFound();
            }

            return View(meetingOrganizer);
        }

        // POST: MeetingOrganizers/Delete/5
        [HttpPost("{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.MeetingOrganizers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MeetingOrganizers'  is null.");
            }
            var meetingOrganizer = await _context.MeetingOrganizers.FindAsync(id);
            if (meetingOrganizer != null)
            {
                _context.MeetingOrganizers.Remove(meetingOrganizer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingOrganizerExists(long id)
        {
          return (_context.MeetingOrganizers?.Any(e => e.MeetingId == id)).GetValueOrDefault();
        }
    }
}
