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
    public class DskdpsController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public DskdpsController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        // GET: Dskdps
        public async Task<IActionResult> Index()
        {
            var quan_ly_buong_phongContext = _context.Dskdps.Include(d => d.MaDatPhongNavigation).Include(d => d.MaKhachHangNavigation);
            return View(await quan_ly_buong_phongContext.ToListAsync());
        }

        // GET: Dskdps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dskdps == null)
            {
                return NotFound();
            }

            var dskdp = await _context.Dskdps
                .Include(d => d.MaDatPhongNavigation)
                .Include(d => d.MaKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dskdp == null)
            {
                return NotFound();
            }

            return View(dskdp);
        }

        // GET: Dskdps/Create
        public IActionResult Create()
        {
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong");
            ViewData["MaKhachHang"] = new SelectList(_context.Khachhangs, "MaKhachHang", "MaKhachHang");
            return View();
        }

        // POST: Dskdps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKhachHang,MaDatPhong,Id")] Dskdp dskdp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dskdp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", dskdp.MaDatPhong);
            ViewData["MaKhachHang"] = new SelectList(_context.Khachhangs, "MaKhachHang", "MaKhachHang", dskdp.MaKhachHang);
            return View(dskdp);
        }

        // GET: Dskdps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dskdps == null)
            {
                return NotFound();
            }

            var dskdp = await _context.Dskdps.FindAsync(id);
            if (dskdp == null)
            {
                return NotFound();
            }
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", dskdp.MaDatPhong);
            ViewData["MaKhachHang"] = new SelectList(_context.Khachhangs, "MaKhachHang", "MaKhachHang", dskdp.MaKhachHang);
            return View(dskdp);
        }

        // POST: Dskdps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKhachHang,MaDatPhong,Id")] Dskdp dskdp)
        {
            if (id != dskdp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dskdp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DskdpExists(dskdp.Id))
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
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", dskdp.MaDatPhong);
            ViewData["MaKhachHang"] = new SelectList(_context.Khachhangs, "MaKhachHang", "MaKhachHang", dskdp.MaKhachHang);
            return View(dskdp);
        }

        // GET: Dskdps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dskdps == null)
            {
                return NotFound();
            }

            var dskdp = await _context.Dskdps
                .Include(d => d.MaDatPhongNavigation)
                .Include(d => d.MaKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dskdp == null)
            {
                return NotFound();
            }

            return View(dskdp);
        }

        // POST: Dskdps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dskdps == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Dskdps'  is null.");
            }
            var dskdp = await _context.Dskdps.FindAsync(id);
            if (dskdp != null)
            {
                _context.Dskdps.Remove(dskdp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DskdpExists(int id)
        {
          return (_context.Dskdps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
