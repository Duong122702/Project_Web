using System;
using System.Collections.Generic;

namespace Project_Web.Models
{
    public partial class Chitietdv
    {
        public string MaDichVu { get; set; } = null!;
        public int SoLuong { get; set; }
        public string MaDatPhong { get; set; } = null!;

        public virtual Booking MaDatPhongNavigation { get; set; } = null!;
        public virtual Dichvu MaDichVuNavigation { get; set; } = null!;
    }
}
