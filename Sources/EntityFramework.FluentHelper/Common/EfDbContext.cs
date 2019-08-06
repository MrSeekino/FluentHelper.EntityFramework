using EntityFramework.FluentHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace EntityFramework.FluentHelper.Common
{
    class EfDbContext : IDbContext
    {
        DbContext DbContext { get; set; }

        string NameOrConnectionString { get; set; }
        Action<string> LogAction { get; set; }

        public EfDbContext()
        {
            DbContext = null;
            NameOrConnectionString = null;
            LogAction = null;
        }

        void CreateDbContext()
        {
            DbContext?.Dispose();
            DbContext = new EfDbModel(NameOrConnectionString);

            if (LogAction != null)
                DbContext.Database.Log = LogAction;
        }

        public IDbContext SetConnectionString(string nameOrConnectionString)
        {
            NameOrConnectionString = nameOrConnectionString;
            return this;
        }

        public IDbContext AddMappingFromAssemblyOf<T>()
        {
            var mappingAssembly = Assembly.GetAssembly(typeof(T));
            ((EfDbModel)GetContext()).AddMappingAssembly(mappingAssembly);

            return this;
        }

        public IDbContext SetLogAction(Action<string> logAction)
        {
            LogAction = logAction;
            return this;
        }

        public DbContext GetContext()
        {
            if (DbContext == null)
                CreateDbContext();

            return DbContext;
        }

        public DbContext CreateNewContext()
        {
            Dispose();

            return GetContext();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return GetContext().Set<T>().AsQueryable();
        }

        public void Add<T>(T inputData) where T : class
        {
            GetContext().Set<T>().Add(inputData);
        }

        public void AddRange<T>(IEnumerable<T> inputData) where T : class
        {
            GetContext().Set<T>().AddRange(inputData);
        }

        public void Remove<T>(T inputData) where T : class
        {
            GetContext().Set<T>().Remove(inputData);
        }

        public void RemoveRange<T>(IEnumerable<T> inputData) where T : class
        {
            GetContext().Set<T>().RemoveRange(inputData);
        }

        public int SaveChanges()
        {
            return GetContext().SaveChanges();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
