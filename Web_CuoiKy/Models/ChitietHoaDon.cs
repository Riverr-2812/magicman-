using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_CuoiKy.Models
{
    public class ChitietHoaDon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int HoaDonId {  get; set; }
        [ForeignKey("HoaDonId")]
        public HoaDon hoaDons {  get; set; }
        [Required]
        public int sanphamId { get; set; }
        [ForeignKey("sanphamId")]
        [ValidateNever]
        public Sanpham sanphams { get; set; }
        public int Quality {  get; set; }
        public double Price {  get; set; }  
    }
}
