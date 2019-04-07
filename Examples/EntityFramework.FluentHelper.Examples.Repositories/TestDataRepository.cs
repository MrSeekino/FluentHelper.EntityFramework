using EntityFramework.FluentHelper.Examples.Models;
using EntityFramework.FluentHelper.Interfaces;
using System;
using System.Linq;

namespace EntityFramework.FluentHelper.Examples.Repositories
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

        public void Remove(Guid id)
        {
            var testData = DbContext.Query<TestData>().SingleOrDefault(e => e.Id == id);
            if (testData != null)
            {
                DbContext.Remove(testData);
                DbContext.SaveChanges();
            }
        }
    }
}
