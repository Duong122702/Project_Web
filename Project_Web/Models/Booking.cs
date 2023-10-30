using System;
using System.Collections.Generic;

namespace Project_Web.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Chitietdvs = new HashSet<Chitietdv>();
            Dskdps = new HashSet<Dskdp>();
            Hoadons = new HashSet<Hoadon>();
            Thuocs = new HashSet<Thuoc>();
        }

        public string MaDatPhong { get; set; } = null!;
        public DateTime NgayDat { get; set; }
        public string? YeuCau { get; set; }
        public int SoNguoi { get; set; }

        public virtual ICollection<Chitietdv> Chitietdvs { get; set; }
        public virtual ICollection<Dskdp> Dskdps { get; set; }
        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Thuoc> Thuocs { get; set; }
    }
}
