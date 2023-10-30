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
    public class PhongsController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;

        public PhongsController(Quan_ly_buong_phongContext context)
        {
            _context = context;
        }

        // GET: Phongs
        public async Task<IActionResult> Index()
        {
              return _context.Phongs != null ? 
                          View(await _context.Phongs.ToListAsync()) :
                          Problem("Entity set 'Quan_ly_buong_phongContext.Phongs'  is null.");
        }

        // GET: Phongs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var phong = await _context.Phongs
                .FirstOrDefaultAsync(m => m.TenPhong == id);
            if (phong == null)
            {
                return NotFound();
            }

            return View(phong);
        }

        // GET: Phongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Phongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenPhong,LoaiPhong,GiaCa,ViewPhong,SoGiuong,LoaiPhongTam,LoaiGiuong,KichThuoc,BanCong")] Phong phong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phong);
        }

        // GET: Phongs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var phong = await _context.Phongs.FindAsync(id);
            if (phong == null)
            {
                return NotFound();
            }
            return View(phong);
        }

        // POST: Phongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenPhong,LoaiPhong,GiaCa,ViewPhong,SoGiuong,LoaiPhongTam,LoaiGiuong,KichThuoc,BanCong")] Phong phong)
        {
            if (id != phong.TenPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhongExists(phong.TenPhong))
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
            return View(phong);
        }

        // GET: Phongs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var phong = await _context.Phongs
                .FirstOrDefaultAsync(m => m.TenPhong == id);
            if (phong == null)
            {
                return NotFound();
            }

            return View(phong);
        }

        // POST: Phongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Phongs == null)
            {
                return Problem("Entity set 'Quan_ly_buong_phongContext.Phongs'  is null.");
            }
            var phong = await _context.Phongs.FindAsync(id);
            if (phong != null)
            {
                _context.Phongs.Remove(phong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhongExists(string id)
        {
          return (_context.Phongs?.Any(e => e.TenPhong == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult GetAvailableRoom(DateTime ngayDen, DateTime ngayDi)
        {
            // Step 1: Find rooms that are already booked
            var bookedRooms = _context.Thuocs
                .Where(t => !(t.NgayDi <= ngayDen || t.NgayDen >= ngayDi))
                .Select(t => t.TenPhong)
                .ToList();

            // Step 2 and 3: Find rooms that are not in the booked list
            var availableRooms = _context.Phongs
        .Where(p => !bookedRooms.Contains(p.TenPhong))
        .ToList();

            return PartialView("_AvailableRoomList", availableRooms);
        }

        public IActionResult BookRoom(string id)
        {
            var maKhachHangs = _context.Khachhangs.Select(kh => kh.MaKhachHang).ToList();
            var viewModel = new BookRoomViewModels
            {
                RoomID = id
            };

            ViewBag.KhachHangs = _context.Khachhangs.ToList();
            return View("BookRoom", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(BookRoomViewModels model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Step 1: Insert into BOOKING
                    var newBooking = new Booking
                    {
                        MaDatPhong = model.MaDatPhong,  // Sử dụng MaDatPhong từ model
                        NgayDat = DateTime.Now,
                        YeuCau = model.YeuCau,
                        SoNguoi = model.SoNguoi
                    };
                    _context.Bookings.Add(newBooking);


                    // Insert into DSKDP
                    var newDskdp = new Dskdp
                    {
                        MaKhachHang = model.MaKhachHang,
                        MaDatPhong = model.MaDatPhong,
                    };
                    _context.Dskdps.Add(newDskdp);

                    // Insert into THUOC
                    var newThuoc = new Thuoc
                    {
                        MaDatPhong = model.MaDatPhong,
                        TenPhong = model.RoomID,
                        NgayDen = model.NgayDen,
                        NgayDi = model.NgayDi
                    };
                    _context.Thuocs.Add(newThuoc);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(new { message = "Đặt phòng thành công" });
                
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return View("Error", new ErrorViewModel { RequestId = "Booking failed: " + e.Message });
                }
            }
        }
    }
}
