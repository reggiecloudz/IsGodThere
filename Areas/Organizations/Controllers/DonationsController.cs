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
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Donations
        [Route("/[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Donations.Include(d => d.Campaign).Include(d => d.Donor).ThenInclude(i => i!.Identity);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Donations/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Donations == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .Include(d => d.Campaign)
                .Include(d => d.Donor)
                    .ThenInclude(i => i!.Identity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Id");
            ViewData["DonorId"] = new SelectList(_context.Members, "Id", "Id");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Message,DonorId,CampaignId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Id", donation.CampaignId);
            ViewData["DonorId"] = new SelectList(_context.Members, "Id", "Id", donation.DonorId);
            return View(donation);
        }

        // GET: Donations/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Donations == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Id", donation.CampaignId);
            ViewData["DonorId"] = new SelectList(_context.Members, "Id", "Id", donation.DonorId);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Amount,Message,DonorId,CampaignId")] Donation donation)
        {
            if (id != donation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Id))
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
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Id", donation.CampaignId);
            ViewData["DonorId"] = new SelectList(_context.Members, "Id", "Id", donation.DonorId);
            return View(donation);
        }

        // GET: Donations/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Donations == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .Include(d => d.Campaign)
                .Include(d => d.Donor)
                    .ThenInclude(i => i!.Identity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost("{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Donations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Donations'  is null.");
            }
            var donation = await _context.Donations.FindAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(long id)
        {
          return (_context.Donations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
