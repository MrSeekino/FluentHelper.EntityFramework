using FluentHelper.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FluentHelper.EntityFramework.Tests")]
namespace FluentHelper.EntityFramework.Common
{
    class EfDbModel : DbContext
    {
        internal List<Assembly> MappingAssemblies { get; set; }

        internal Func<Type, IDbMap> GetInstanceOfDbMapBehaviour { get; set; }

        public EfDbModel(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<EfDbModel>(null);

            MappingAssemblies = new List<Assembly>();

            GetInstanceOfDbMapBehaviour = (x) => { return (IDbMap)Activator.CreateInstance(x); };
        }

        internal EfDbModel(Func<Type, IDbMap> getInstanceOfDbMapBehaviour) 
        {
            GetInstanceOfDbMapBehaviour = getInstanceOfDbMapBehaviour;
        }

        internal EfDbModel() { }

        public void AddMappingAssembly(Assembly mappingAssembly)
        {
            if (MappingAssemblies == null)
                MappingAssemblies = new List<Assembly>();

            MappingAssemblies.Add(mappingAssembly);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            CreateModel(modelBuilder);
        }

        internal void CreateModel(DbModelBuilder modelBuilder)
        {
            var mappings = MappingAssemblies.SelectMany(m => m.GetTypes()).Where(p => p.IsClass && typeof(IDbMap).IsAssignableFrom(p) && !p.IsAbstract).ToList();

            foreach (var m in mappings)
            {
                var objInstance = GetInstanceOfDbMapBehaviour(m);
                objInstance.SetModelBuilder(modelBuilder);
                objInstance.Map();
            }
        }
    }
}
