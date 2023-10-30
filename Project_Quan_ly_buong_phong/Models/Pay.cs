using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
{
    public class Pay
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaHoaDon { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaDatPhong { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayThanhToan { get; set; }
        [Required]
        [StringLength(255)]
        public string HinhThucThanhToan { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayDen { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayDi { get; set; }
    }
}
