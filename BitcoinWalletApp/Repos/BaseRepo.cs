using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Linq;
using DbUpdateConcurrencyException = Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;
using System.IO;
using BitcoinWalletApp.Models;
using SQLite;
using System.Threading.Tasks;

namespace BitcoinWalletApp.Repos
{
    public class BaseRepo<T> : IRepo<T> where T : EntityBase, new()
    {
        private readonly SQLiteAsyncConnection _db;

        public BaseRepo(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            App.Database.Database.EnsureCreated();
            _db.CreateTableAsync<T>().Wait();
        }


        internal int SaveChanges()
        {
            try
            {
                return App.Database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.InnerException.Message);
            }
            catch (CommitFailedException ex)
            {
                throw new CommitFailedException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Add(T entity) => _db.InsertAsync(entity).Result;

        public int AddRange(IList<T> entities) => _db.InsertAllAsync(entities).Result;

        public int Update(T entity) => _db.UpdateAsync(entity).Result;

        public int UpdateRange(IList<T> entities) => _db.UpdateAllAsync(entities).Result;

        public int Delete(T entity) => _db.DeleteAsync(entity).Result;

        public T GetOne(int? id) => _db.GetAsync<T>(id).Result;
        public List<T> GetAll() => _db.Table<T>().ToListAsync().Result;
      //  public List<T> GetSome(Expression<Func<T, bool>> where) => _db.ExecuteScalarAsync<T>()

        public List<T> ExecuteQuery(string sql) => _db.ExecuteScalarAsync<T>(sql).Result as List<T>;
        public List<T> ExecuteQuery(string sql, object[] parameters) => _db.ExecuteScalarAsync<T>(sql, parameters).Result as List<T>;
    }
}
