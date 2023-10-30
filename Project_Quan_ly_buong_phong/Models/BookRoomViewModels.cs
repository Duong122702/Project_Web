using System.ComponentModel.DataAnnotations;

namespace Project_Quan_ly_buong_phong.Models
{
    public class BookRoomViewModels
    {
        [Required]
        public string RoomID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaDatPhong { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string MaKhachHang { get; set; }
        [Required]
        public DateTime NgayDen { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayDi { get; set; }
        [StringLength(50)]
        public string YeuCau { get; set; }
        [Required]
        [Range(1,20)]
        public int SoNguoi { get; set; }
    }
}
