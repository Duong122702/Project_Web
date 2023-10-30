using System;
using System.Collections.Generic;

namespace Project_Web.Models
{
    public partial class Phong
    {
        public Phong()
        {
            Thuocs = new HashSet<Thuoc>();
        }

        public string TenPhong { get; set; } = null!;
        public int LoaiPhong { get; set; }
        public int GiaCa { get; set; }
        public string ViewPhong { get; set; } = null!;
        public int SoGiuong { get; set; }
        public string LoaiPhongTam { get; set; } = null!;
        public string LoaiGiuong { get; set; } = null!;
        public int KichThuoc { get; set; }
        public bool BanCong { get; set; }

        public virtual ICollection<Thuoc> Thuocs { get; set; }
    }
}
