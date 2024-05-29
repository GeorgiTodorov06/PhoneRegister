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
    public class PhoneListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhoneListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhoneLists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PhoneLists.Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PhoneLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhoneLists == null)
            {
                return NotFound();
            }

            var phoneList = await _context.PhoneLists
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneList == null)
            {
                return NotFound();
            }

            return View(phoneList);
        }

        // GET: PhoneLists/Create
        public IActionResult Create()
        {
            var users = _context.Users.Select(u => new {
                u.Id,
                DisplayName = "Name: " + u.FirstName + " ,Id: " + u.Id
            }).ToList();

            ViewBag.UserId = new SelectList(users, "Id", "DisplayName");
            return View();
        }

        // POST: PhoneLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserId")] PhoneList phoneList)
        {
            
                _context.Add(phoneList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", phoneList.UserId);
            return View(phoneList);
        }

        // GET: PhoneLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhoneLists == null)
            {
                return NotFound();
            }

            var phoneList = await _context.PhoneLists.FindAsync(id);
            if (phoneList == null)
            {
                return NotFound();
            }

            var users = _context.Users.Select(u => new {
                u.Id,
                DisplayName = "Name: " + u.FirstName + " ,Id: " + u.Id
            }).ToList();

            ViewBag.UserId = new SelectList(users, "Id", "DisplayName");
            return View();
        }

        // POST: PhoneLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId")] PhoneList phoneList)
        {
            if (id != phoneList.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(phoneList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneListExists(phoneList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", phoneList.UserId);
            return View(phoneList);
        }

        // GET: PhoneLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhoneLists == null)
            {
                return NotFound();
            }

            var phoneList = await _context.PhoneLists
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneList == null)
            {
                return NotFound();
            }

            return View(phoneList);
        }

        // POST: PhoneLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhoneLists == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PhoneLists'  is null.");
            }
            var phoneList = await _context.PhoneLists.FindAsync(id);
            if (phoneList != null)
            {
                _context.PhoneLists.Remove(phoneList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneListExists(int id)
        {
          return (_context.PhoneLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
