using Product.Management.Data.Models;
using System;
using System.Collections.Generic;

namespace Product.Management.Business.Repository.Abstract
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Kategorileri listeleyen data table için kullanılır
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="orderBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        Tuple<IEnumerable<Categories>, int> GetCategoryList(int start, int length, string orderBy, string search);
        /// <summary>
        /// Yeni kategori oluşturur
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool InsertCategory(Categories form);
        /// <summary>
        /// Kategori günceller
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool UpdateCategory(Categories form);
        /// <summary>
        /// Kategori detaylarını getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Categories DetailCategory(int id);
    }
}
