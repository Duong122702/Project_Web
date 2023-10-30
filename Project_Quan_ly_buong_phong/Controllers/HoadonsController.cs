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
    public class HoadonsController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public HoadonsController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        // GET: Hoadons
        public async Task<IActionResult> Index()
        {
            var quan_ly_buong_phongContext = _context.Hoadons.Include(h => h.MaDatPhongNavigation);
            return View(await quan_ly_buong_phongContext.ToListAsync());
        }

        // GET: Hoadons/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Hoadons == null)
            {
                return NotFound();
            }

            var hoadon = await _context.Hoadons
                .Include(h => h.MaDatPhongNavigation)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (hoadon == null)
            {
                return NotFound();
            }

            return View(hoadon);
        }

        // GET: Hoadons/Create
        public IActionResult Create()
        {
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong");
            return View();
        }

        // POST: Hoadons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHoaDon,MaDatPhong,NgayThanhToan,HìnhThucThanhToan,TongTien")] Hoadon hoadon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoadon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", hoadon.MaDatPhong);
            return View(hoadon);
        }

        // GET: Hoadons/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Hoadons == null)
            {
                return NotFound();
            }

            var hoadon = await _context.Hoadons.FindAsync(id);
            if (hoadon == null)
            {
                return NotFound();
            }
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", hoadon.MaDatPhong);
            return View(hoadon);
        }

        // POST: Hoadons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHoaDon,MaDatPhong,NgayThanhToan,HìnhThucThanhToan,TongTien")] Hoadon hoadon)
        {
            if (id != hoadon.MaHoaDon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoadon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoadonExists(hoadon.MaHoaDon))
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
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", hoadon.MaDatPhong);
            return View(hoadon);
        }

        // GET: Hoadons/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Hoadons == null)
            {
                return NotFound();
            }

            var hoadon = await _context.Hoadons
                .Include(h => h.MaDatPhongNavigation)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (hoadon == null)
            {
                return NotFound();
            }

            return View(hoadon);
        }

        // POST: Hoadons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Hoadons == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Hoadons'  is null.");
            }
            var hoadon = await _context.Hoadons.FindAsync(id);
            if (hoadon != null)
            {
                _context.Hoadons.Remove(hoadon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoadonExists(string id)
        {
          return (_context.Hoadons?.Any(e => e.MaHoaDon == id)).GetValueOrDefault();
        }
    }
}
