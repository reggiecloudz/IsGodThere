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
    [Route("[area]/Team-Members/[action]")]
    public class TeamMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamMembers
        [Route("/[area]/Team-Members")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeamMembers.Include(t => t.Member).Include(t => t.Team);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeamMembers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TeamMembers == null)
            {
                return NotFound();
            }

            var teamMember = await _context.TeamMembers
                .Include(t => t.Member)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // GET: TeamMembers/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id");
            return View();
        }

        // POST: TeamMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,TeamId")] TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", teamMember.MemberId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", teamMember.TeamId);
            return View(teamMember);
        }

        // GET: TeamMembers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TeamMembers == null)
            {
                return NotFound();
            }

            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", teamMember.MemberId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", teamMember.TeamId);
            return View(teamMember);
        }

        // POST: TeamMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MemberId,TeamId")] TeamMember teamMember)
        {
            if (id != teamMember.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamMemberExists(teamMember.TeamId))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", teamMember.MemberId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", teamMember.TeamId);
            return View(teamMember);
        }

        // GET: TeamMembers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TeamMembers == null)
            {
                return NotFound();
            }

            var teamMember = await _context.TeamMembers
                .Include(t => t.Member)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // POST: TeamMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TeamMembers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TeamMembers'  is null.");
            }
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember != null)
            {
                _context.TeamMembers.Remove(teamMember);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamMemberExists(long id)
        {
          return (_context.TeamMembers?.Any(e => e.TeamId == id)).GetValueOrDefault();
        }
    }
}
