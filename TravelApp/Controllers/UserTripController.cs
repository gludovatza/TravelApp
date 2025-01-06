using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class UserTripController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserTripController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserTrip
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserTrips.Include(u => u.Trip).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserTrip/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTrip = await _context.UserTrips
                .Include(u => u.Trip)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTrip == null)
            {
                return NotFound();
            }

            return View(userTrip);
        }

        // GET: UserTrip/Create
        public IActionResult Create()
        {
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: UserTrip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,TripId,Date")] UserTrip userTrip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTrip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Description", userTrip.TripId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userTrip.UserId);
            return View(userTrip);
        }

        // GET: UserTrip/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTrip = await _context.UserTrips.FindAsync(id);
            if (userTrip == null)
            {
                return NotFound();
            }
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Description", userTrip.TripId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userTrip.UserId);
            return View(userTrip);
        }

        // POST: UserTrip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TripId,Date")] UserTrip userTrip)
        {
            if (id != userTrip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTrip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTripExists(userTrip.Id))
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
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Description", userTrip.TripId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userTrip.UserId);
            return View(userTrip);
        }

        // GET: UserTrip/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTrip = await _context.UserTrips
                .Include(u => u.Trip)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTrip == null)
            {
                return NotFound();
            }

            return View(userTrip);
        }

        // POST: UserTrip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTrip = await _context.UserTrips.FindAsync(id);
            if (userTrip != null)
            {
                _context.UserTrips.Remove(userTrip);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTripExists(int id)
        {
            return _context.UserTrips.Any(e => e.Id == id);
        }
    }
}
