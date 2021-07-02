using Product.Management.Data.Models;
using System.Collections.Generic;

namespace Product.Management.UI.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Products> Products { get; set; }
        public Products Product { get; set; }
        public IEnumerable<Categories> Categories { get; set; }
        public IEnumerable<Subcategories> Subcategories { get; set; }
    }
}