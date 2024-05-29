using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhoneRegister.Data;
using PhoneRegister.Models;

namespace PhoneRegister.Controllers
{
    public class PhoneEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhoneEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhoneEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PhoneEntries.Include(p => p.PhoneList);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PhoneEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhoneEntries == null)
            {
                return NotFound();
            }

            var phoneEntry = await _context.PhoneEntries
                .Include(p => p.PhoneList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneEntry == null)
            {
                return NotFound();
            }

            return View(phoneEntry);
        }

        // GET: PhoneEntries/Create
        public IActionResult Create()
        {
            ViewData["PhoneListId"] = new SelectList(_context.PhoneLists, "Id", "Id");
            return View();
        }

        // POST: PhoneEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContactName,ContactNumber,LastCallDate,PhoneListId")] PhoneEntry phoneEntry)
        {
            if (!phoneEntry.ContactNumber.All(char.IsDigit) || int.Parse(phoneEntry.ContactNumber) < 0)
            {
                return NotFound();
            }

            if (phoneEntry.LastCallDate > DateTime.Now)
            {
                return NotFound();
            }

            _context.Add(phoneEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["PhoneListId"] = new SelectList(_context.PhoneLists, "Id", "Id", phoneEntry.PhoneListId);
            return View(phoneEntry);
        }

        // GET: PhoneEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhoneEntries == null)
            {
                return NotFound();
            }

            var phoneEntry = await _context.PhoneEntries.FindAsync(id);
            if (phoneEntry == null)
            {
                return NotFound();
            }
            ViewData["PhoneListId"] = new SelectList(_context.PhoneLists, "Id", "Id", phoneEntry.PhoneListId);
            return View(phoneEntry);
        }

        // POST: PhoneEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContactName,ContactNumber,LastCallDate,PhoneListId")] PhoneEntry phoneEntry)
        {
            if (id != phoneEntry.Id)
            {
                return NotFound();
            }
            if (!phoneEntry.ContactNumber.All(char.IsDigit) || int.Parse(phoneEntry.ContactNumber) < 0)
            {
                return NotFound();
            }

            if (phoneEntry.LastCallDate > DateTime.Now)
            {
                return NotFound();
            }

            try
                {
                    _context.Update(phoneEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneEntryExists(phoneEntry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["PhoneListId"] = new SelectList(_context.PhoneLists, "Id", "Id", phoneEntry.PhoneListId);
            return View(phoneEntry);
        }

        // GET: PhoneEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhoneEntries == null)
            {
                return NotFound();
            }

            var phoneEntry = await _context.PhoneEntries
                .Include(p => p.PhoneList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneEntry == null)
            {
                return NotFound();
            }

            return View(phoneEntry);
        }

        // POST: PhoneEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhoneEntries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PhoneEntries'  is null.");
            }
            var phoneEntry = await _context.PhoneEntries.FindAsync(id);
            if (phoneEntry != null)
            {
                _context.PhoneEntries.Remove(phoneEntry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneEntryExists(int id)
        {
          return (_context.PhoneEntries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
