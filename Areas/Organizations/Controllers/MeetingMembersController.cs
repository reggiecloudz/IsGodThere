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
    [Route("[area]/Meeting-Members/[action]")]
    public class MeetingMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MeetingMembers
        [Route("/[area]/Meeting-Members")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MeetingMembers.Include(m => m.Meeting).Include(m => m.Member).ThenInclude(m => m!.Identity);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MeetingMembers/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.MeetingMembers == null)
            {
                return NotFound();
            }

            var meetingMember = await _context.MeetingMembers
                .Include(m => m.Meeting)
                .Include(m => m.Member)
                    .ThenInclude(m => m!.Identity)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (meetingMember == null)
            {
                return NotFound();
            }

            return View(meetingMember);
        }

        // GET: MeetingMembers/Create
        public IActionResult Create()
        {
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id");
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            return View();
        }

        // POST: MeetingMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,MeetingId")] MeetingMember meetingMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetingMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id", meetingMember.MeetingId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", meetingMember.MemberId);
            return View(meetingMember);
        }

        // GET: MeetingMembers/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MeetingMembers == null)
            {
                return NotFound();
            }

            var meetingMember = await _context.MeetingMembers.FindAsync(id);
            if (meetingMember == null)
            {
                return NotFound();
            }
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id", meetingMember.MeetingId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", meetingMember.MemberId);
            return View(meetingMember);
        }

        // POST: MeetingMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MemberId,MeetingId")] MeetingMember meetingMember)
        {
            if (id != meetingMember.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingMemberExists(meetingMember.MemberId))
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
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id", meetingMember.MeetingId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", meetingMember.MemberId);
            return View(meetingMember);
        }

        // GET: MeetingMembers/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.MeetingMembers == null)
            {
                return NotFound();
            }

            var meetingMember = await _context.MeetingMembers
                .Include(m => m.Meeting)
                .Include(m => m.Member)
                    .ThenInclude(m => m!.Identity)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (meetingMember == null)
            {
                return NotFound();
            }

            return View(meetingMember);
        }

        // POST: MeetingMembers/Delete/5
        [HttpPost("{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.MeetingMembers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MeetingMembers'  is null.");
            }
            var meetingMember = await _context.MeetingMembers.FindAsync(id);
            if (meetingMember != null)
            {
                _context.MeetingMembers.Remove(meetingMember);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingMemberExists(string id)
        {
          return (_context.MeetingMembers?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
