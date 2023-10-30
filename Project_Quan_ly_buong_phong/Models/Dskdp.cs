using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
{
    public partial class Dskdp
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaKhachHang { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaDatPhong { get; set; } = null!;
        public int Id { get; set; }

        public virtual Booking MaDatPhongNavigation { get; set; } = null!;
        public virtual Khachhang MaKhachHangNavigation { get; set; } = null!;
    }
}
