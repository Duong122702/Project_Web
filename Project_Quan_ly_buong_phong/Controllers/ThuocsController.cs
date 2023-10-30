using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Quan_ly_buong_phong.Models;

namespace Project_Quan_ly_buong_phong.Controllers
{
    public class ThuocsController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public ThuocsController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        // GET: Thuocs
        public async Task<IActionResult> Index()
        {
            var quan_ly_buong_phongContext = _context.Thuocs.Include(t => t.MaDatPhongNavigation).Include(t => t.TenPhongNavigation);
            return View(await quan_ly_buong_phongContext.ToListAsync());
        }

        // GET: Thuocs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Thuocs == null)
            {
                return NotFound();
            }

            var thuoc = await _context.Thuocs
                .Include(t => t.MaDatPhongNavigation)
                .Include(t => t.TenPhongNavigation)
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);
            if (thuoc == null)
            {
                return NotFound();
            }

            return View(thuoc);
        }

        // GET: Thuocs/Create
        public IActionResult Create()
        {
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong");
            ViewData["TenPhong"] = new SelectList(_context.Phongs, "TenPhong", "TenPhong");
            return View();
        }

        // POST: Thuocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDatPhong,TenPhong,NgayDen,NgayDi")] Thuoc thuoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", thuoc.MaDatPhong);
            ViewData["TenPhong"] = new SelectList(_context.Phongs, "TenPhong", "TenPhong", thuoc.TenPhong);
            return View(thuoc);
        }

        // GET: Thuocs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Thuocs == null)
            {
                return NotFound();
            }

            var thuoc = await _context.Thuocs.FindAsync(id);
            if (thuoc == null)
            {
                return NotFound();
            }
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", thuoc.MaDatPhong);
            ViewData["TenPhong"] = new SelectList(_context.Phongs, "TenPhong", "TenPhong", thuoc.TenPhong);
            return View(thuoc);
        }

        // POST: Thuocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDatPhong,TenPhong,NgayDen,NgayDi")] Thuoc thuoc)
        {
            if (id != thuoc.MaDatPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThuocExists(thuoc.MaDatPhong))
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
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", thuoc.MaDatPhong);
            ViewData["TenPhong"] = new SelectList(_context.Phongs, "TenPhong", "TenPhong", thuoc.TenPhong);
            return View(thuoc);
        }

        // GET: Thuocs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Thuocs == null)
            {
                return NotFound();
            }

            var thuoc = await _context.Thuocs
                .Include(t => t.MaDatPhongNavigation)
                .Include(t => t.TenPhongNavigation)
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);
            if (thuoc == null)
            {
                return NotFound();
            }

            return View(thuoc);
        }

        // POST: Thuocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Thuocs == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Thuocs'  is null.");
            }
            var thuoc = await _context.Thuocs.FindAsync(id);
            if (thuoc != null)
            {
                _context.Thuocs.Remove(thuoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThuocExists(string id)
        {
          return (_context.Thuocs?.Any(e => e.MaDatPhong == id)).GetValueOrDefault();
        }
    }
}
