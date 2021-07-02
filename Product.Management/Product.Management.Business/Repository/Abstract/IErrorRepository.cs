using Product.Management.Data.Models;

namespace Product.Management.Business.Repository.Abstract
{
    public interface IErrorRepository
    {
        /// <summary>
        /// Log kaydını db ye kayıt eder
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool LogInfoAdd(LogInfo form);
    }
}
