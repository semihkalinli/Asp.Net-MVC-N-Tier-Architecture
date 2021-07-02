using System.ComponentModel.DataAnnotations;

namespace Product.Management.UI.ViewModels
{
    public class CategoryFormEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
    }
}