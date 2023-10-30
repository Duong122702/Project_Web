using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
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
        [Required]
        public string MaDatPhong { get; set; } = null!;
        [Required(ErrorMessage = "Yêu cầu nhập ngày")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime NgayDat { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-z0-9]+$")]
        public string? YeuCau { get; set; }
        [Required]
        [Range(0,20)]
        public int SoNguoi { get; set; }

        public virtual ICollection<Chitietdv> Chitietdvs { get; set; }
        public virtual ICollection<Dskdp> Dskdps { get; set; }
        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Thuoc> Thuocs { get; set; }
    }
}
