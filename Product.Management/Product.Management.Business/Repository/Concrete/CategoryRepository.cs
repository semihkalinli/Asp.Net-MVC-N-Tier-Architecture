using Product.Management.Business.Repository.Abstract;
using Product.Management.Data.Models;
using Product.Management.Data.SQLHelper;
using System;
using System.Collections.Generic;

namespace Product.Management.Business.Repository.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategorySql _sqlHelper;
        public CategoryRepository()
        {
            _sqlHelper = new CategorySql();
        }
        public Tuple<IEnumerable<Categories>, int> GetCategoryList(int start, int length, string orderBy, string search)
        {
            try { return _sqlHelper.GetCategories(start, length, orderBy, search); }
            catch (Exception ex) { string exx = ex.Message; return null; }
        }
        public bool InsertCategory(Categories form)
        {
            try { return _sqlHelper.CategoryAdd(form); }
            catch (Exception ex) { string exx = ex.Message; return false; }
        }
        public bool UpdateCategory(Categories form)
        {
            try { return _sqlHelper.CategoryUpdate(form); }
            catch (Exception ex) { string exx = ex.Message; return false; }
        }
        public Categories DetailCategory(int id)
        {
            try { return _sqlHelper.CategoryDetail(id); }
            catch (Exception ex) { string exx = ex.Message; return null; }
        }
        
    }
}
