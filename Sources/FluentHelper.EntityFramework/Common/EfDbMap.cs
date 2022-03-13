using FluentHelper.EntityFramework.Interfaces;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace FluentHelper.EntityFramework.Common
{
    public abstract class EfDbMap<T> : IDbMap where T : class
    {
        DbModelBuilder ModelBuilder { get; set; }

        public DbModelBuilder GetModelBuilder()
        {
            return ModelBuilder;
        }

        public void SetModelBuilder(DbModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }

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
