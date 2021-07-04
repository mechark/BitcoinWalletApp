using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinWalletApp
{
    public interface IRepo<T>
    {
        int Add(T entity);
        int AddRange(IList<T> entities);
        int Delete(T entity);
        T GetOne(int? id);
        List<T> GetAll();

        List<T> ExecuteQuery(string sql);
        List<T> ExecuteQuery(string sql, object[] sqlParametersObjects);
    }
}
