using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
{
    public partial class Thuoc
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaDatPhong { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string TenPhong { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayDen { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayDi { get; set; }

        public virtual Booking MaDatPhongNavigation { get; set; } = null!;
        public virtual Phong TenPhongNavigation { get; set; } = null!;
    }
}
