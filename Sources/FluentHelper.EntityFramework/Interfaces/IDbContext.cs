using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace FluentHelper.EntityFramework.Interfaces
{
    public interface IDbContext : IDisposable
    {
        IDbContext SetConnectionString(string nameOrConnectionString);
        IDbContext AddMappingFromAssemblyOf<T>();
        IDbContext SetLogAction(Action<string> logAction);

        DbContext GetContext();
        DbContext CreateNewContext();

        IQueryable<T> Query<T>() where T : class;
        void Add<T>(T inputData) where T : class;
        void AddRange<T>(IEnumerable<T> inputData) where T : class;
        void Remove<T>(T inputData) where T : class;
        void RemoveRange<T>(IEnumerable<T> inputData) where T : class;

        IQueryable<T> ExecuteQuery<T>(string sqlQuery, params object[] sqlParams) where T : class;
        int ExecuteCommand(string sqlQuery, params object[] sqlParams);

        int SaveChanges();

        DbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
        void UseTransaction(DbTransaction dbTransaction);
    }
}
