using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Quan_ly_buong_phong.Models;

namespace Project_Quan_ly_buong_phong.Controllers
{
    public class BookingsController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public BookingsController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Bookings
        public async Task<IActionResult> Index()
        {
              return _context.Bookings != null ? 
                          View(await _context.Bookings.ToListAsync()) :
                          Problem("Entity set 'Quan_ly_buong_phongContext.Bookings'  is null.");
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDatPhong,NgayDat,YeuCau,SoNguoi")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDatPhong,NgayDat,YeuCau,SoNguoi")] Booking booking)
        {
            if (id != booking.MaDatPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.MaDatPhong))
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
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Bookings'  is null.");
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(string id)
        {
          return (_context.Bookings?.Any(e => e.MaDatPhong == id)).GetValueOrDefault();
        }

        public IActionResult Pay(string id)
        {
            // var maDatPhong = _context.Bookings.Select(dp => dp.MaDatPhong);
            var ngayDen = _context.Thuocs
                     .Where(t => t.MaDatPhong == id)
                     .Select(t => t.NgayDen)
                     .FirstOrDefault();
            var ngayDi = _context.Thuocs
                    .Where(t => t.MaDatPhong == id)
                    .Select(t => t.NgayDi)
                    .FirstOrDefault();
            var viewModel = new Pay
            {
                MaDatPhong = id,
                NgayDen = ngayDen,
                NgayDi = ngayDi,
            };

            //ViewBag.KhachHangs = _context.Khachhangs.ToList();
            return View("Pay", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> PayConfirmed(Pay model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tạo hóa đơn mới
                    var newHoaDon = new Hoadon
                    {
                        MaHoaDon = model.MaHoaDon,
                        MaDatPhong = model.MaDatPhong,
                        NgayThanhToan = model.NgayThanhToan,
                        HìnhThucThanhToan = model.HinhThucThanhToan,
                        TongTien = null // sẽ cập nhật sau
                    };

                    _context.Hoadons.Add(newHoaDon);

                    var roomcost = _context.Phongs
                        .Join(_context.Thuocs, p => p.TenPhong, t => t.TenPhong, (p, t) => new { p.GiaCa, t.MaDatPhong })
                        .Where(x => x.MaDatPhong == model.MaDatPhong)
                        .Select(x => x.GiaCa)
                        .FirstOrDefault();

                    TimeSpan timeSpan = model.NgayDi - model.NgayDen;
                    int numberOfDays = timeSpan.Days;
                    var totalServiceCost = _context.Chitietdvs
                        .Join(_context.Dichvus, cdv => cdv.MaDichVu, dv => dv.MaDichVu, (cdv, dv) => new { cdv.SoLuong, dv.GiaDichVu, cdv.MaDatPhong })
                        .Where(x => x.MaDatPhong == model.MaDatPhong)
                        .Select(x => x.SoLuong * x.GiaDichVu)
                        .Sum();

                    newHoaDon.TongTien = roomcost * numberOfDays + totalServiceCost;
                    // Tính tổng tiền và cập nhật vào hóa đơn
                    // Đoạn code này có thể tương tự như câu lệnh SQL bạn muốn sử dụng
                    // ...

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(new { Message = "Phòng đã được trả và hóa đơn đã được tạo." });
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new { Message = "Trả phòng thất bại", Error = e.Message });
                }
            }
        }

    }
}
