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
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applicants
        [Route("/[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Applicants.Include(a => a.Opportunity).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Applicants/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(a => a.Opportunity)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // GET: Applicants/Create
        public IActionResult Create()
        {
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IsAccepted,Qualifications,UserId,OpportunityId")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Id", applicant.OpportunityId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", applicant.UserId);
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Id", applicant.OpportunityId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", applicant.UserId);
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IsAccepted,Qualifications,UserId,OpportunityId")] Applicant applicant)
        {
            if (id != applicant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.Id))
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
            ViewData["OpportunityId"] = new SelectList(_context.Opportunities, "Id", "Id", applicant.OpportunityId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", applicant.UserId);
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(a => a.Opportunity)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost("{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Applicants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Applicants'  is null.");
            }
            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant != null)
            {
                _context.Applicants.Remove(applicant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(long id)
        {
          return (_context.Applicants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
