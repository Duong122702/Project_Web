using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
{
    public partial class Phong
    {
        public Phong()
        {
            Thuocs = new HashSet<Thuoc>();
        }
        [Required]
        [StringLength(255)]
        public string TenPhong { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue)]
        public int LoaiPhong { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int GiaCa { get; set; }
        [Required]
        [StringLength(255)]
        public string ViewPhong { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue)]
        public int SoGiuong { get; set; }
        [Required]
        [StringLength(255)]
        public string LoaiPhongTam { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string LoaiGiuong { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue)]
        public int KichThuoc { get; set; }
        public bool BanCong { get; set; }

        public virtual ICollection<Thuoc> Thuocs { get; set; }
    }
}
