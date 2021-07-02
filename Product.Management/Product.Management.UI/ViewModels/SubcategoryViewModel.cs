using Product.Management.Data.Models;
using System.Collections.Generic;

namespace Product.Management.UI.ViewModels
{
    public class SubcategoryViewModel
    {
        public Subcategories Subcategory { get; set; }
        public IEnumerable<Categories> Categories { get; set; }
    }
}