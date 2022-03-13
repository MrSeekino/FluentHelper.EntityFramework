using System.Data.Entity;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FluentHelper.EntityFramework.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace FluentHelper.EntityFramework.Interfaces
{
    internal interface IDbMap
    {
        DbModelBuilder GetModelBuilder();

        void SetModelBuilder(DbModelBuilder modelBuilder);

        void Map();
    }
}
