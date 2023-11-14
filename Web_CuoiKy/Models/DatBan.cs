using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_CuoiKy.Models
{
    public class DatBan
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Liên kết với người dùng đã đăng nhập
        [ForeignKey("UserId")]
        [ValidateNever]
        [Required]
        public ApplicationUser applications { get; set; }
        public DateTime NgayDat { get; set; }
        public string TenNguoiDat { get; set; }
        public string SDT { get; set; }
        public string GhiChu { get; set; }
        public string Email { get; set; }
        public int Soluong {  get; set; }   
        
        // Các trường thông tin khác về đặt bàn, chẳng hạn như số lượng người, thời gian, vv.

        // Khóa ngoại đến người dùng
   
    }
}
