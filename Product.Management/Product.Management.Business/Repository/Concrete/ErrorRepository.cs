using Product.Management.Business.Repository.Abstract;
using Product.Management.Data.Models;
using Product.Management.Data.SQLHelper;
using System;

namespace Product.Management.Business.Repository.Concrete
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly ErrorSql _sqlHelper;
        public ErrorRepository()
        {
            _sqlHelper = new ErrorSql();
        }
        public bool LogInfoAdd(LogInfo form)
        {
            try { return _sqlHelper.LogInfoAdd(form); }
            catch (Exception ex) { string exx = ex.Message; return false; }
        }
    }
}
