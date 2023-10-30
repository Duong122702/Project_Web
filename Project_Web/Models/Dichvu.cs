using System;
using System.Collections.Generic;

namespace Project_Web.Models
{
    public partial class Dichvu
    {
        public Dichvu()
        {
            Chitietdvs = new HashSet<Chitietdv>();
        }

        public string MaDichVu { get; set; } = null!;
        public string TenDichVu { get; set; } = null!;
        public int GiaDichVu { get; set; }
        public string DonViTinh { get; set; } = null!;

        public virtual ICollection<Chitietdv> Chitietdvs { get; set; }
    }
}
