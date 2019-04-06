using EntityFramework.FluentHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace EntityFramework.FluentHelper.Common
{
    class EfDbModel : DbContext
    {
        List<Assembly> MappingAssemblies { get; set; }

        public EfDbModel(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            MappingAssemblies = new List<Assembly>();
        }

        public void AddMappingAssembly(Assembly mappingAssembly)
        {
            MappingAssemblies.Add(mappingAssembly);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var mappings = MappingAssemblies.SelectMany(m => m.GetTypes()).Where(p => p.IsClass && typeof(IDbMap).IsAssignableFrom(p) && !p.IsAbstract).ToList();

            foreach (var m in mappings)
                ((IDbMap)((EfDbMap)Activator.CreateInstance(m)).SetModelBuilder(modelBuilder)).Map();
        }
    }
}
