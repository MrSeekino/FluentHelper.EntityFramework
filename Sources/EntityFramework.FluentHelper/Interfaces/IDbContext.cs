using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EntityFramework.FluentHelper.Interfaces
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

        int SaveChanges();
    }
}
