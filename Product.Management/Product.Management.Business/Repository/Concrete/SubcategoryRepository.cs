using Product.Management.Business.Repository.Abstract;
using Product.Management.Data.Models;
using Product.Management.Data.SQLHelper;
using System;
using System.Collections.Generic;

namespace Product.Management.Business.Repository.Concrete
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly SubcategorySql _sqlHelper;
        public SubcategoryRepository(SubcategorySql sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }
        public Tuple<IEnumerable<Subcategories>, int> GetSubcategoriesList(int categoryID, int start, int length, string orderBy, string search)
        {
            try { return _sqlHelper.GetSubcategories(categoryID, start, length, orderBy, search); }
            catch (Exception ex) { string exx = ex.Message; return null; }
        }
        public IEnumerable<Categories> GetSelectListCategories()
        {
            try { return _sqlHelper.GetSelectListCategories(); }
            catch (Exception ex) { string exx = ex.Message; return null; }
        }
        public bool InsertSubcategory(Subcategories form)
        {
            try { return _sqlHelper.SubcategoryAdd(form); }
            catch (Exception ex) { string exx = ex.Message; return false; }
        }
        public bool UpdateSubcategory(Subcategories form)
        {
            try { return _sqlHelper.SubcategoryUpdate(form); }
            catch (Exception ex) { string exx = ex.Message; return false; }
        }
        public Subcategories DetailSubcategory(int id)
        {
            try { return _sqlHelper.SubcategoryDetail(id); }
            catch (Exception ex) { string exx = ex.Message; return null; }
        }
    }
}
