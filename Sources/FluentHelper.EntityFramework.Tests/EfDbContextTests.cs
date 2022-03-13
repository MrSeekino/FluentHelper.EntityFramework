using FluentHelper.EntityFramework.Common;
using FluentHelper.EntityFramework.Interfaces;
using FluentHelper.EntityFramework.Tests.Support;
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
    internal class EfDbContextTests
    {
        [Test]
        public void Verify_CreateDbContext_IsCalledCorrectly()
        {
            var dbModelMock = new Mock<EfDbModel>();

            bool funcCalled = false;
            Func<string, EfDbModel> createDbContextBehaviour = (cs) =>
            {
                funcCalled = true;

                return dbModelMock.Object;
            };

            var dbContext = new EfDbContext(createDbContextBehaviour);

            dbContext.CreateDbContext();
            Assert.True(funcCalled);
        }

        [Test]
        public void Verify_CreateDbContext_WorksProperly_AfterSetAllProperties()
        {
            string connStringSample = "A_Conn_String";
            Action<string> logAction = (x) => { };

            var dbModel = new EfDbModel(x => { return null; });

            bool funcCalledCorrecly = false;
            Func<string, EfDbModel> createDbContextBehaviour = (cs) =>
            {
                if (cs == connStringSample)
                    funcCalledCorrecly = true;

                return dbModel;
            };

            var dbContext = new EfDbContext(createDbContextBehaviour)
                                .SetConnectionString(connStringSample);

            dbContext.CreateNewContext();
            Assert.True(funcCalledCorrecly);

            dbContext.AddMappingFromAssemblyOf<TestEntityMap>();
            Assert.That(dbModel.MappingAssemblies.Count, Is.EqualTo(1));
        }

        [Test]
        public void Verify_CreateNewContext_WorksProperly()
        {
            var dbModelMock = new Mock<EfDbModel>();

            bool funcCalled = false;
            Func<string, EfDbModel> createDbContextBehaviour = (cs) =>
            {
                funcCalled = true;

                return dbModelMock.Object;
            };

            var dbContext = new EfDbContext(createDbContextBehaviour);

            dbContext.CreateNewContext();
            Assert.True(funcCalled);
        }

        [Test]
        public void Verify_GetContext_WorksProperly()
        {
            var dbModelMock = new Mock<EfDbModel>();

            bool funcCalled = false;
            Func<string, EfDbModel> createDbContextBehaviour = (cs) =>
            {
                funcCalled = true;

                return dbModelMock.Object;
            };

            var dbContext = new EfDbContext(createDbContextBehaviour);
            dbContext.DbContext = dbModelMock.Object;

            var contextGot = dbContext.GetContext();

            Assert.That(contextGot, Is.Not.Null);
            Assert.That(contextGot, Is.EqualTo(dbModelMock.Object));

            Assert.False(funcCalled);
        }

        [Test]
        public void Verify_QueriesMethods_WorksProperly()
        {
            int setCalls = 0;

            var dbsetMock = new Mock<DbSet<TestEntity>>();

            var dbModelMock = new Mock<EfDbModel>();
            dbModelMock.Setup(x => x.Set<TestEntity>()).Returns(dbsetMock.Object);

            Func<string, EfDbModel> createDbContextBehaviour = (cs) =>
            {
                return dbModelMock.Object;
            };

            var dbContext = new EfDbContext(createDbContextBehaviour);
            dbContext.CreateDbContext();

            var queryable = dbContext.Query<TestEntity>();
            setCalls++;

            dbModelMock.Verify(x => x.Set<TestEntity>(), Times.Exactly(setCalls));

            dbContext.Add(new TestEntity());
            setCalls++;

            dbModelMock.Verify(x => x.Set<TestEntity>(), Times.Exactly(setCalls));
            dbsetMock.Verify(x => x.Add(It.IsAny<TestEntity>()), Times.Once());

            dbContext.AddRange(new List<TestEntity>());
            setCalls++;

            dbModelMock.Verify(x => x.Set<TestEntity>(), Times.Exactly(setCalls));
            dbsetMock.Verify(x => x.AddRange(It.IsAny<List<TestEntity>>()), Times.Once());

            dbContext.Remove(new TestEntity());
            setCalls++;

            dbModelMock.Verify(x => x.Set<TestEntity>(), Times.Exactly(setCalls));
            dbsetMock.Verify(x => x.Remove(It.IsAny<TestEntity>()), Times.Once());

            dbContext.RemoveRange(new List<TestEntity>());
            setCalls++;

            dbModelMock.Verify(x => x.Set<TestEntity>(), Times.Exactly(setCalls));
            dbsetMock.Verify(x => x.RemoveRange(It.IsAny<List<TestEntity>>()), Times.Once());
        }

        [Test]
        public void Verify_SaveChanges_WorksProperly()
        {
            var dbModelMock = new Mock<EfDbModel>();
            dbModelMock.Setup(x => x.SaveChanges()).Returns(0);

            Func<string, EfDbModel> createDbContextBehaviour = (cs) =>
            {
                return dbModelMock.Object;
            };

            var dbContext = new EfDbContext(createDbContextBehaviour);
            dbContext.CreateDbContext();

            int result = dbContext.SaveChanges();
            dbModelMock.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
