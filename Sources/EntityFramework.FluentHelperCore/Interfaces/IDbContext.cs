using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework.FluentHelperCore.Interfaces
{
    public interface IDbContext : IDisposable
    {
        IDbContext SetConnectionString(string nameOrConnectionString);
        IDbContext UseLazyLoadingProxies();
        IDbContext AddMappingFromAssemblyOf<T>();

        IDbContext SetLogAction(Action<string> logAction);
        IDbContext SetLogAction(Action<string> logAction, bool enableSensitiveDataLogging);
        IDbContext SetLogAction(Action<string> logAction, Func<EventId, LogLevel, bool> logFilter);
        IDbContext SetLogAction(Action<string> logAction, bool enableSensitiveDataLogging, Func<EventId, LogLevel, bool> logFilter);

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
