using System.ComponentModel.DataAnnotations;

namespace Product.Management.UI.ViewModels
{
    public class ProductFormEditViewModel
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        [Range(1, int.MaxValue)]
        public int SubcategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 99999.99)]
        public decimal Price { get; set; }
        [Range(0, 100)]
        public decimal KdvRate { get; set; }
    }
}