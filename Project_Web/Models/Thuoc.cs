using System;
using System.Collections.Generic;

namespace Project_Web.Models
{
    public partial class Thuoc
    {
        public string MaDatPhong { get; set; } = null!;
        public string TenPhong { get; set; } = null!;
        public DateTime NgayDen { get; set; }
        public DateTime NgayDi { get; set; }

        public virtual Booking MaDatPhongNavigation { get; set; } = null!;
        public virtual Phong TenPhongNavigation { get; set; } = null!;
    }
}
