using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Dskdps = new HashSet<Dskdp>();
        }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaKhachHang { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string Ho { get; set; } = null!;
        [StringLength(255)]
        public string? Dem { get; set; }
        [Required]
        [StringLength(255)]
        public string Ten { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$")]
        public string Cccd { get; set; } = null!;
        public bool GioiTinh { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PhanLoai { get; set; }
        [Required]
        [RegularExpression(@"^\+?[0-9]+$")]
        public string Sdt { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }

        public virtual ICollection<Dskdp> Dskdps { get; set; }
    }
}
