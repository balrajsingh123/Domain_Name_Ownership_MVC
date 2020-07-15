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
    public class DomainsController : Controller
    {
        private readonly Domain_Name_Ownership_DbContext _context;

        public DomainsController(Domain_Name_Ownership_DbContext context)
        {
            _context = context;
        }

        // GET: Domains
        public async Task<IActionResult> Index()
        {
            var domain_Name_Ownership_DbContext = _context.Domain.Include(d => d.DomainHost);
            return View(await domain_Name_Ownership_DbContext.ToListAsync());
        }

        // GET: Domains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await _context.Domain
                .Include(d => d.DomainHost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domain == null)
            {
                return NotFound();
            }

            return View(domain);
        }
        [Authorize]
        // GET: Domains/Create
        public IActionResult Create()
        {
            ViewData["DomainHostId"] = new SelectList(_context.Set<DomainHost>(), "Id", "Id");
            return View();
        }

        // POST: Domains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DomainName,ContactPhone,DomainHostId")] Domain domain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(domain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DomainHostId"] = new SelectList(_context.Set<DomainHost>(), "Id", "Id", domain.DomainHostId);
            return View(domain);
        }
        [Authorize]
        // GET: Domains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await _context.Domain.FindAsync(id);
            if (domain == null)
            {
                return NotFound();
            }
            ViewData["DomainHostId"] = new SelectList(_context.Set<DomainHost>(), "Id", "Id", domain.DomainHostId);
            return View(domain);
        }

        // POST: Domains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DomainName,ContactPhone,DomainHostId")] Domain domain)
        {
            if (id != domain.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(domain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DomainExists(domain.Id))
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
            ViewData["DomainHostId"] = new SelectList(_context.Set<DomainHost>(), "Id", "Id", domain.DomainHostId);
            return View(domain);
        }
        [Authorize]
        // GET: Domains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await _context.Domain
                .Include(d => d.DomainHost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domain == null)
            {
                return NotFound();
            }

            return View(domain);
        }

        // POST: Domains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domain = await _context.Domain.FindAsync(id);
            _context.Domain.Remove(domain);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DomainExists(int id)
        {
            return _context.Domain.Any(e => e.Id == id);
        }
    }
}
