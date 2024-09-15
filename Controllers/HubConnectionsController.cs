using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myapp.Data;
using myapp.Models;

namespace myapp.Controllers
{
    public class HubConnectionsController : Controller
    {
        private readonly AppDbContext _context;

        public HubConnectionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: HubConnections
        public async Task<IActionResult> Index()
        {
            return View(await _context.HubConnections.ToListAsync());
        }

        // GET: HubConnections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hubConnection = await _context.HubConnections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hubConnection == null)
            {
                return NotFound();
            }

            return View(hubConnection);
        }

        // GET: HubConnections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HubConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConnectionId,UserName")] HubConnection hubConnection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hubConnection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hubConnection);
        }

        // GET: HubConnections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hubConnection = await _context.HubConnections.FindAsync(id);
            if (hubConnection == null)
            {
                return NotFound();
            }
            return View(hubConnection);
        }

        // POST: HubConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConnectionId,UserName")] HubConnection hubConnection)
        {
            if (id != hubConnection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hubConnection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HubConnectionExists(hubConnection.Id))
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
            return View(hubConnection);
        }

        // GET: HubConnections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hubConnection = await _context.HubConnections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hubConnection == null)
            {
                return NotFound();
            }

            return View(hubConnection);
        }

        // POST: HubConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hubConnection = await _context.HubConnections.FindAsync(id);
            if (hubConnection != null)
            {
                _context.HubConnections.Remove(hubConnection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HubConnectionExists(int id)
        {
            return _context.HubConnections.Any(e => e.Id == id);
        }
    }
}
