using Product.Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Management.Business.Repository.Abstract
{
    public interface ISubcategoryRepository
    {
        /// <summary>
        /// Datatable için dinamik bir şekilde alt kategorileri getirir.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="orderBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        Tuple<IEnumerable<Subcategories>, int> GetSubcategoriesList(int categoryID, int start, int length, string orderBy, string search);
        /// <summary>
        /// Selectbox için tüm kategorileri getirir
        /// </summary>
        /// <returns></returns>
        IEnumerable<Categories> GetSelectListCategories();
        /// <summary>
        /// Alt kategori ekleme işlemi
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool InsertSubcategory(Subcategories form);
        /// <summary>
        /// Alt Kategori güncelleme işlemi
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool UpdateSubcategory(Subcategories form);
        /// <summary>
        /// Alt kategori detay sayfası için bilgilerini getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Subcategories DetailSubcategory(int id);
    }
}
