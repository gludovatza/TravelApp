using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    
    public class TripController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TripController(ApplicationDbContext c)
        {
            _context = c;
        }
        //CREATE ; DETAILS ; DELETE ; INDEX ; EDIT

        //INDEX: GET /Trip

        //DETAILS : GET /Trip/Details/1

        //CREATE : GET, POST /Trip/Create

        //EDIT : GET, POST /Trip/Edit/1

        //DELETE : POST /Trip/Delete/1

        public async Task<IActionResult> Index()
        {
            var trips = await (from t in _context.Trips select t).ToListAsync();
            return View(trips);
        }
    
        public async Task<IActionResult> Details(int id)
        {
            var trip = await (from t in _context.Trips where t.Id == id select t).FirstOrDefaultAsync();
            return View(trip);
        }

        // [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Trip trip)
        {
            // ha érvényes...
            if(ModelState.IsValid)
            {
                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // ha elbukik a validáció...
            return View(trip);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var trip = await(from t in _context.Trips where t.Id == id select t).FirstOrDefaultAsync();
            if (trip == null) 
                return NotFound();
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Trip trip)
        {
            // ha érvényes...
            if (ModelState.IsValid)
            {
                _context.Update(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // ha elbukik a validáció...
            return View(trip);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var trip = await (from t in _context.Trips where t.Id == id select t).FirstOrDefaultAsync();
            if (trip == null)
                return NotFound();
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
