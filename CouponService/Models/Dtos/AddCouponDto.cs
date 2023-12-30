using System.ComponentModel.DataAnnotations;

namespace CouponService.Models.Dtos
{
    public class AddCouponDto
    {
        [Required]
        public string CouponCode { get; set; } = String.Empty;
        [Required]
        [Range(100, 100000)]
        public int CouponAmount { get; set; }
        [Required]
        public int CouponMinAmount { get; set; }
    }
}
