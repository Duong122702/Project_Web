using System;
using System.Collections.Generic;

namespace Project_Web.Models
{
    public partial class Dskdp
    {
        public string MaKhachHang { get; set; } = null!;
        public string MaDatPhong { get; set; } = null!;
        public int Id { get; set; }

        public virtual Booking MaDatPhongNavigation { get; set; } = null!;
        public virtual Khachhang MaKhachHangNavigation { get; set; } = null!;
    }
}
