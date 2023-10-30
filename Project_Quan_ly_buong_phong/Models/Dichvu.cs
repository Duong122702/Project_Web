using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
{
    public partial class Dichvu
    {
        public Dichvu()
        {
            Chitietdvs = new HashSet<Chitietdv>();
        }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaDichVu { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string TenDichVu { get; set; } = null!;
        [Required]
        [Range(0, int.MaxValue)]
        public int GiaDichVu { get; set; }
        [Required]
        [StringLength(255)]
        public string DonViTinh { get; set; } = null!;

        public virtual ICollection<Chitietdv> Chitietdvs { get; set; }
    }
}
