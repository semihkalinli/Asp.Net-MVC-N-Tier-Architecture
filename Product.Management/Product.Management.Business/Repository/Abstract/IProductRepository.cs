using Product.Management.Data.Models;
using System;
using System.Collections.Generic;

namespace Product.Management.Business.Repository.Abstract
{
    public interface IProductRepository
    {
        /// <summary>
        /// Ürünleri datatable için getiren dinamik bir fonksiyon
        /// </summary>
        /// <param name="catId"></param>
        /// <param name="subcatId"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="orderBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        Tuple<IEnumerable<Products>, int> GetProductList(int catId, int subcatId, int start, int length, string orderBy, string search);
        /// <summary>
        /// Selectbox için kategori isimleri ve id'leri getirir
        /// </summary>
        /// <returns></returns>
        IEnumerable<Categories> GetSelectListCategories();
        /// <summary>
        /// SelectBox için gönderilen category id'sine göre alt kategori isimleri ve id'leri getirir
        /// </summary>
        /// <param name="catId"></param>
        /// <returns></returns>
        IEnumerable<Subcategories> GetSelectListSubcategories(int catId);
        /// <summary>
        /// Ürün ekle işlemi
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool InsertProduct(Products form);
        /// <summary>
        /// Ürün güncelleme işlemi
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool UpdateProduct(Products form);
        /// <summary>
        /// Ürün detayını getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Products DetailProduct(int id);
    }
}
