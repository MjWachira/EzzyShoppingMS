using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string Image { get; set; } = string.Empty;
        
        [ForeignKey("ProductId")]
        public Product product { get; set; } = default!;
        public Guid ProductId { get; set; }
    }
}
