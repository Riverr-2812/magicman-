using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_CuoiKy.Models
{
    public class HoaDon
    {
        [Key] public int Id { get; set; }
        public string ApplicationUserId {  get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser applicationUser { get; set; }
        [ValidateNever]
        public double Total {  get; set; }
        public DateTime OrderDate {  get; set; }
        public int PhoneNumer { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        

    }
}
