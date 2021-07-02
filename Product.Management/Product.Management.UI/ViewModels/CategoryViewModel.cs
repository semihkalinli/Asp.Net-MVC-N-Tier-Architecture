using Product.Management.Data.Models;
using System.Collections.Generic;

namespace Product.Management.UI.ViewModels
{
    public class CategoryViewModel
    {
        public Categories Category { get; set; }
        public IEnumerable<Categories> Categories { get; set; }
    }
}