using EntityFramework.FluentHelper.Common;
using EntityFramework.FluentHelper.Examples.Mappings;
using EntityFramework.FluentHelper.Interfaces;
using System;

namespace EntityFramework.FluentHelper.Examples.Dao
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
