using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Web.Models;

namespace Project_Web.Controllers
{
    public class ChitietdvsController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public ChitietdvsController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        // GET: Chitietdvs
        public async Task<IActionResult> Index()
        {
            var quan_ly_buong_phongContext = _context.Chitietdvs.Include(c => c.MaDatPhongNavigation).Include(c => c.MaDichVuNavigation);
            return View(await quan_ly_buong_phongContext.ToListAsync());
        }

        // GET: Chitietdvs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Chitietdvs == null)
            {
                return NotFound();
            }

            var chitietdv = await _context.Chitietdvs
                .Include(c => c.MaDatPhongNavigation)
                .Include(c => c.MaDichVuNavigation)
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);
            if (chitietdv == null)
            {
                return NotFound();
            }

            return View(chitietdv);
        }

        // GET: Chitietdvs/Create
        public IActionResult Create()
        {
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong");
            ViewData["MaDichVu"] = new SelectList(_context.Dichvus, "MaDichVu", "MaDichVu");
            return View();
        }

        // POST: Chitietdvs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDichVu,SoLuong,MaDatPhong")] Chitietdv chitietdv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chitietdv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", chitietdv.MaDatPhong);
            ViewData["MaDichVu"] = new SelectList(_context.Dichvus, "MaDichVu", "MaDichVu", chitietdv.MaDichVu);
            return View(chitietdv);
        }

        // GET: Chitietdvs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Chitietdvs == null)
            {
                return NotFound();
            }

            var chitietdv = await _context.Chitietdvs.FindAsync(id);
            if (chitietdv == null)
            {
                return NotFound();
            }
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", chitietdv.MaDatPhong);
            ViewData["MaDichVu"] = new SelectList(_context.Dichvus, "MaDichVu", "MaDichVu", chitietdv.MaDichVu);
            return View(chitietdv);
        }

        // POST: Chitietdvs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDichVu,SoLuong,MaDatPhong")] Chitietdv chitietdv)
        {
            if (id != chitietdv.MaDatPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chitietdv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChitietdvExists(chitietdv.MaDatPhong))
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
            ViewData["MaDatPhong"] = new SelectList(_context.Bookings, "MaDatPhong", "MaDatPhong", chitietdv.MaDatPhong);
            ViewData["MaDichVu"] = new SelectList(_context.Dichvus, "MaDichVu", "MaDichVu", chitietdv.MaDichVu);
            return View(chitietdv);
        }

        // GET: Chitietdvs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Chitietdvs == null)
            {
                return NotFound();
            }

            var chitietdv = await _context.Chitietdvs
                .Include(c => c.MaDatPhongNavigation)
                .Include(c => c.MaDichVuNavigation)
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);
            if (chitietdv == null)
            {
                return NotFound();
            }

            return View(chitietdv);
        }

        // POST: Chitietdvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Chitietdvs == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Chitietdvs'  is null.");
            }
            var chitietdv = await _context.Chitietdvs.FindAsync(id);
            if (chitietdv != null)
            {
                _context.Chitietdvs.Remove(chitietdv);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChitietdvExists(string id)
        {
          return (_context.Chitietdvs?.Any(e => e.MaDatPhong == id)).GetValueOrDefault();
        }
    }
}
