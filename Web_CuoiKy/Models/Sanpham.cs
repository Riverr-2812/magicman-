using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_CuoiKy.Areas.Customer.Controllers;
using Web_CuoiKy.Data;
using Web_CuoiKy.Models;

namespace Web_CuoiKy.Models
{
    public class Sanpham
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
    
        [Required] public string? ImageURL { get; set; }
        [Required] public string? Gia { get; set; }
        [Required]
        public int NhacungcapId { get; set; } // Thay vì TheloaiId
        [ForeignKey("NhacungcapId")]
        [ValidateNever]
        public Nhacungcap nhacungcaps { get; set; }
    }
}

