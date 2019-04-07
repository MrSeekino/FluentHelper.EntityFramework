using EntityFramework.FluentHelper.Common;
using EntityFramework.FluentHelper.Examples.Models;

namespace EntityFramework.FluentHelper.Examples.Mappings
{
    public class TestDataMap : EfDbMap<TestData>
    {
        public override void Map()
        {
            Entity.ToTable("TestDataTable");

            Entity.HasKey(e => e.Id);

            Entity.Property(e => e.Id);
            Entity.Property(e => e.Name);
            Entity.Property(e => e.CreationDate);
            Entity.Property(e => e.Active);
        }
    }
}
