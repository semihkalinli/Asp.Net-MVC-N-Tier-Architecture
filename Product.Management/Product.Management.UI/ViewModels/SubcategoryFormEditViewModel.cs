using System.ComponentModel.DataAnnotations;

namespace Product.Management.UI.ViewModels
{
    public class SubcategoryFormEditViewModel
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
    }
}