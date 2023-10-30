using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
{
    public partial class Chitietdv
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaDichVu { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue)]
        public int SoLuong { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaDatPhong { get; set; } = null!;
        public int Id { get; set; }

        public virtual Booking MaDatPhongNavigation { get; set; } = null!;
        public virtual Dichvu MaDichVuNavigation { get; set; } = null!;
    }
}
