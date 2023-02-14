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
    public class RepliesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepliesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Replies
        [Route("/[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Replies.Include(r => r.Parent).Include(r => r.Post).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Replies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Replies == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .Include(r => r.Parent)
                .Include(r => r.Post)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // GET: Replies/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Replies, "Id", "Content");
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Content");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Replies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,PostId,ParentId,UserId")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Replies, "Id", "Content", reply.ParentId);
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Content", reply.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reply.UserId);
            return View(reply);
        }

        // GET: Replies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Replies == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Replies, "Id", "Content", reply.ParentId);
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Content", reply.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reply.UserId);
            return View(reply);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Content,PostId,ParentId,UserId")] Reply reply)
        {
            if (id != reply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyExists(reply.Id))
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
            ViewData["ParentId"] = new SelectList(_context.Replies, "Id", "Content", reply.ParentId);
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Content", reply.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reply.UserId);
            return View(reply);
        }

        // GET: Replies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Replies == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .Include(r => r.Parent)
                .Include(r => r.Post)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Replies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Replies'  is null.");
            }
            var reply = await _context.Replies.FindAsync(id);
            if (reply != null)
            {
                _context.Replies.Remove(reply);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReplyExists(long id)
        {
          return (_context.Replies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
