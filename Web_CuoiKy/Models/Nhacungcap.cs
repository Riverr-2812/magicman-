using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_CuoiKy.Models
{
    public class Nhacungcap
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
      
         public string? ImageURL { get; set; }
    
      
        
    }
}

