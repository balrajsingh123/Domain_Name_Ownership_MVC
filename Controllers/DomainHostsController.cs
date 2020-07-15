using Domain_Name_Ownership_MVC.Data;
using Domain_Name_Ownership_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Domain_Name_Ownership_MVC.Controllers
{
    public class DomainHostsController : Controller
    {
        private readonly Domain_Name_Ownership_DbContext _context;

        public DomainHostsController(Domain_Name_Ownership_DbContext context)
        {
            _context = context;
        }

        // GET: DomainHosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.DomainHost.ToListAsync());
        }

        // GET: DomainHosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domainHost = await _context.DomainHost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domainHost == null)
            {
                return NotFound();
            }

            return View(domainHost);
        }
        [Authorize]
        // GET: DomainHosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DomainHosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DomainTypes")] DomainHost domainHost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(domainHost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(domainHost);
        }
        [Authorize]
        // GET: DomainHosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domainHost = await _context.DomainHost.FindAsync(id);
            if (domainHost == null)
            {
                return NotFound();
            }
            return View(domainHost);
        }

        // POST: DomainHosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DomainTypes")] DomainHost domainHost)
        {
            if (id != domainHost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(domainHost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DomainHostExists(domainHost.Id))
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
            return View(domainHost);
        }
        [Authorize]
        // GET: DomainHosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domainHost = await _context.DomainHost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domainHost == null)
            {
                return NotFound();
            }

            return View(domainHost);
        }

        // POST: DomainHosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domainHost = await _context.DomainHost.FindAsync(id);
            _context.DomainHost.Remove(domainHost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DomainHostExists(int id)
        {
            return _context.DomainHost.Any(e => e.Id == id);
        }
    }
}
