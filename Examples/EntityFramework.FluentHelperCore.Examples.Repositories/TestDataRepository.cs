using EntityFramework.FluentHelperCore.Examples.Models;
using EntityFramework.FluentHelperCore.Interfaces;
using System;
using System.Linq;

namespace EntityFramework.FluentHelperCore.Examples.Repositories
{
    public class TestDataRepository : BaseRepository
    {
        public TestDataRepository() : base() { }

        public TestDataRepository(IDbContext dbContext) : base(dbContext) { }

        public IQueryable<TestData> GetAll()
        {
            return DbContext.Query<TestData>();
        }

        public void Add(TestData testData)
        {
            DbContext.Add(testData);
            DbContext.SaveChanges();
        }

        public void Update(TestData testData)
        {
            var testDataInstance = DbContext.Query<TestData>().SingleOrDefault(x => x.Id == testData.Id);
            if (testDataInstance != null)
            {
                testDataInstance.Name = testData.Name;
                testDataInstance.Active = testData.Active;

                DbContext.SaveChanges();
            }
        }

        public void Remove(Guid id)
        {
            var testDataInstance = DbContext.Query<TestData>().SingleOrDefault(e => e.Id == id);
            if (testDataInstance != null)
            {
                DbContext.Remove(testDataInstance);
                DbContext.SaveChanges();
            }
        }
    }
}
