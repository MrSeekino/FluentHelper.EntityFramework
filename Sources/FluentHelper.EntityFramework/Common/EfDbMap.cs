using FluentHelper.EntityFramework.Interfaces;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace FluentHelper.EntityFramework.Common
{
    public abstract class EfDbMap
    {
        DbModelBuilder ModelBuilder { get; set; }

        public DbModelBuilder GetModelBuilder()
        {
            return ModelBuilder;
        }

        public EfDbMap SetModelBuilder(DbModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;

            return this;
        }
    }

    public abstract class EfDbMap<T> : EfDbMap, IDbMap where T : class
    {
        public EntityTypeConfiguration<T> Entity
        {
            get
            {
                return GetModelBuilder().Entity<T>();
            }
        }

        public abstract void Map();
    }
}
