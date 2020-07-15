using Domain_Name_Ownership_MVC.Data;
using Domain_Name_Ownership_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Domain_Name_Ownership_MVC.Controllers
{
    [Authorize]
    public class OwnershipsController : Controller
    {
        private readonly Domain_Name_Ownership_DbContext _context;

        public OwnershipsController(Domain_Name_Ownership_DbContext context)
        {
            _context = context;
        }

        // GET: Ownerships
        public async Task<IActionResult> Index()
        {
            var domain_Name_Ownership_DbContext = _context.Ownership.Include(o => o.Domain).Include(o => o.Owner);
            return View(await domain_Name_Ownership_DbContext.ToListAsync());
        }

        // GET: Ownerships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownership = await _context.Ownership
                .Include(o => o.Domain)
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ownership == null)
            {
                return NotFound();
            }

            return View(ownership);
        }

        // GET: Ownerships/Create
        public IActionResult Create()
        {
            ViewData["DomainId"] = new SelectList(_context.Domain, "Id", "Id");
            ViewData["OwnerId"] = new SelectList(_context.Owner, "Id", "Id");
            return View();
        }

        // POST: Ownerships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerId,DomainId")] Ownership ownership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ownership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DomainId"] = new SelectList(_context.Domain, "Id", "Id", ownership.DomainId);
            ViewData["OwnerId"] = new SelectList(_context.Owner, "Id", "Id", ownership.OwnerId);
            return View(ownership);
        }

        // GET: Ownerships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownership = await _context.Ownership.FindAsync(id);
            if (ownership == null)
            {
                return NotFound();
            }
            ViewData["DomainId"] = new SelectList(_context.Domain, "Id", "Id", ownership.DomainId);
            ViewData["OwnerId"] = new SelectList(_context.Owner, "Id", "Id", ownership.OwnerId);
            return View(ownership);
        }

        // POST: Ownerships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,DomainId")] Ownership ownership)
        {
            if (id != ownership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ownership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnershipExists(ownership.Id))
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
            ViewData["DomainId"] = new SelectList(_context.Domain, "Id", "Id", ownership.DomainId);
            ViewData["OwnerId"] = new SelectList(_context.Owner, "Id", "Id", ownership.OwnerId);
            return View(ownership);
        }

        // GET: Ownerships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownership = await _context.Ownership
                .Include(o => o.Domain)
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ownership == null)
            {
                return NotFound();
            }

            return View(ownership);
        }

        // POST: Ownerships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ownership = await _context.Ownership.FindAsync(id);
            _context.Ownership.Remove(ownership);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnershipExists(int id)
        {
            return _context.Ownership.Any(e => e.Id == id);
        }
    }
}
