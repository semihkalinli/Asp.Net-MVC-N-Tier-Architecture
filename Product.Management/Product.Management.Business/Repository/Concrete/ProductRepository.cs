using Product.Management.Business.Repository.Abstract;
using Product.Management.Data.Models;
using Product.Management.Data.SQLHelper;
using System;
using System.Collections.Generic;

namespace Product.Management.Business.Repository.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductSql _sqlHelper;
        public ProductRepository()
        {
            _sqlHelper = new ProductSql();
        }
        public Tuple<IEnumerable<Products>, int> GetProductList(int catId, int subcatId, int start, int length, string orderBy, string search)
        {
            try { return _sqlHelper.GetProducts(catId, subcatId, start,length,orderBy,search); }
            catch (Exception ex) { string exx = ex.Message; return null; }
        }
        public IEnumerable<Categories> GetSelectListCategories()
        {
            try { return _sqlHelper.GetSelectListCategories(); }
            catch (Exception ex) { string exx = ex.Message; return null; }
        }
        public IEnumerable<Subcategories> GetSelectListSubcategories(int catId)
        {
            try { return _sqlHelper.GetSelectListSubcategories(catId); }
            catch (Exception ex) { string exx = ex.Message; return null; }
        }
        public bool InsertProduct(Products form)
        {
            try { return _sqlHelper.ProductAdd(form); }
            catch (Exception ex) { string exx = ex.Message; return false; }
        }
        public bool UpdateProduct(Products form)
        {
            try { return _sqlHelper.ProductUpdate(form); }
            catch (Exception ex) { string exx = ex.Message; return false; }
        }
        public Products DetailProduct(int id)
        {
            try { return _sqlHelper.ProductDetail(id); }
            catch (Exception ex) { string exx = ex.Message; return null; }
        }
    }
}
