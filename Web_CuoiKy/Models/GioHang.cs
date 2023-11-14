using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_CuoiKy.Models
{
    public class GioHang
    {
        [Key]
        public int Id { get; set; }
        public int sanphamId { get; set; }
        [ForeignKey("sanphamId")]
        [ValidateNever]
        public Sanpham sanphams { get; set; }
        public int Quanlity { get; set; }
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        [ValidateNever]
        public ApplicationUser applications { get; set; }
        public string Username { get; set; }
        [NotMapped]
        public double giatien { get; set; }
    }
}
