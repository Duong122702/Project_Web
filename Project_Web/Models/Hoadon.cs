using System;
using System.Collections.Generic;

namespace Project_Web.Models
{
    public partial class Hoadon
    {
        public string MaHoaDon { get; set; } = null!;
        public string MaDatPhong { get; set; } = null!;
        public DateTime NgayThanhToan { get; set; }
        public string HìnhThucThanhToan { get; set; } = null!;
        public int? TongTien { get; set; }

        public virtual Booking MaDatPhongNavigation { get; set; } = null!;
    }
}
