using FluentHelper.EntityFramework.Common;
using FluentHelper.EntityFramework.Examples.Mappings;
using FluentHelper.EntityFramework.Interfaces;
using System;

namespace FluentHelper.EntityFramework.Examples.Dao
{
    public static class DaoInitializer
    {
        public static IDbContext InitializeContext()
        {
            return EfDbContextManager.GenerateContext()
                .SetConnectionString("name=FluentHelperExampleConnectionString")
                .SetLogAction(x => Console.WriteLine(x))
                .AddMappingFromAssemblyOf<TestDataMap>();
        }
    }
}
