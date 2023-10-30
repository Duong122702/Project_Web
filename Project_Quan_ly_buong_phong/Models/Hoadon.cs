using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
{
    public partial class Hoadon
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaHoaDon { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaDatPhong { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayThanhToan { get; set; }
        [Required]
        [StringLength(255)]
        public string HìnhThucThanhToan { get; set; } = null!;
        [Range(0, int.MaxValue)]
        public int? TongTien { get; set; }

        public virtual Booking MaDatPhongNavigation { get; set; } = null!;
    }
}
