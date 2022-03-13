using FluentHelper.EntityFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHelper.EntityFramework.Tests.Support
{
    public class TestEntityMap : EfDbMap<TestEntity>
    {
        public override void Map()
        {
            Entity.ToTable("TestDataTable");

            Entity.HasKey(e => e.Id);

            Entity.Property(e => e.Name);
        }
    }
}
