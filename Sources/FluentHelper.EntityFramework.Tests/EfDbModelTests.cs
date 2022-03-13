using FluentHelper.EntityFramework.Common;
using FluentHelper.EntityFramework.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentHelper.EntityFramework.Tests
{
    [TestFixture]
    internal class EfDbModelTests
    {
        [Test]
        public void Verify_AddMappingAssembly_WorksProperly()
        {
            var dbModel = new EfDbModel(x => { return null; });

            dbModel.AddMappingAssembly(Assembly.GetExecutingAssembly());

            Assert.That(dbModel.MappingAssemblies.Count, Is.EqualTo(1));
        }

        [Test]
        public void Verify_CreateModel_WorksProperly()
        {
            var mockModelBuilder = new Mock<DbModelBuilder>();
            var mockOfDbMap = new Mock<IDbMap>();

            Func<Type, IDbMap> getInstanceOfDbMapBehaviour = (x) =>
            {
                return mockOfDbMap.Object;
            };

            var dbModel = new EfDbModel(getInstanceOfDbMapBehaviour);

            dbModel.AddMappingAssembly(Assembly.GetExecutingAssembly());

            dbModel.CreateModel(mockModelBuilder.Object);

            mockOfDbMap.Verify(x => x.SetModelBuilder(mockModelBuilder.Object), Times.Once());
            mockOfDbMap.Verify(x => x.Map(), Times.Once());
        }
    }
}
