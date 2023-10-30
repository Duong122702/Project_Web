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
    public class DichvusController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public DichvusController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        // GET: Dichvus
        public async Task<IActionResult> Index()
        {
            var maDatPhong = _context.Bookings.Select(kh => kh.MaDatPhong).ToList();

            ViewBag.Booking = _context.Bookings.ToList();

            return _context.Dichvus != null ? 
                          View(await _context.Dichvus.ToListAsync()) :
                          Problem("Entity set 'Quan_ly_buong_phongContext.Dichvus'  is null.");
        }

        // GET: Dichvus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Dichvus == null)
            {
                return NotFound();
            }

            var dichvu = await _context.Dichvus
                .FirstOrDefaultAsync(m => m.MaDichVu == id);
            if (dichvu == null)
            {
                return NotFound();
            }

            return View(dichvu);
        }

        // GET: Dichvus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dichvus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDichVu,TenDichVu,GiaDichVu,DonViTinh")] Dichvu dichvu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dichvu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dichvu);
        }

        // GET: Dichvus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Dichvus == null)
            {
                return NotFound();
            }

            var dichvu = await _context.Dichvus.FindAsync(id);
            if (dichvu == null)
            {
                return NotFound();
            }
            return View(dichvu);
        }

        // POST: Dichvus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDichVu,TenDichVu,GiaDichVu,DonViTinh")] Dichvu dichvu)
        {
            if (id != dichvu.MaDichVu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dichvu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DichvuExists(dichvu.MaDichVu))
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
            return View(dichvu);
        }

        // GET: Dichvus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Dichvus == null)
            {
                return NotFound();
            }

            var dichvu = await _context.Dichvus
                .FirstOrDefaultAsync(m => m.MaDichVu == id);
            if (dichvu == null)
            {
                return NotFound();
            }

            return View(dichvu);
        }

        // POST: Dichvus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Dichvus == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Dichvus'  is null.");
            }
            var dichvu = await _context.Dichvus.FindAsync(id);
            if (dichvu != null)
            {
                _context.Dichvus.Remove(dichvu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DichvuExists(string id)
        {
          return (_context.Dichvus?.Any(e => e.MaDichVu == id)).GetValueOrDefault();
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmService(DatDichVuViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tạo chi tiết dịch vụ mới
                    var newChiTietDV = new Chitietdv
                    {
                        MaDichVu = model.MaDichVu,
                        MaDatPhong = model.MaDatPhong,
                        SoLuong = model.SoLuong
                    };

                    _context.Chitietdvs.Add(newChiTietDV);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(new { Message = "Đặt dịch vụ thành công" });
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new { Message = "Đặt dịch vụ thất bại", Error = e.Message });
                }
            }
        }
    }
}
