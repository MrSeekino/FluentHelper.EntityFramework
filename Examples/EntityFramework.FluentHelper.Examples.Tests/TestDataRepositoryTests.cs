using Bogus;
using EntityFramework.FluentHelper.Examples.Models;
using EntityFramework.FluentHelper.Examples.Repositories;
using EntityFramework.FluentHelper.Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace EntityFramework.FluentHelper.Examples.Tests
{
    [TestFixture(Category = "Repository: TestData")]
    public class TestDataRepositoryTests
    {
        [Test]
        public void Can_Get_UserList()
        {
            var dataGenerator = new Faker<TestData>().StrictMode(false)
                                .RuleFor(u => u.Id, f => Guid.NewGuid())
                                .RuleFor(u => u.Name, f => f.Name.FirstName())
                                .RuleFor(u => u.Active, f => true)
                                .RuleFor(u => u.CreationDate, f => DateTime.UtcNow);

            var dataSet = dataGenerator.Generate(3);

            var mockContext = new DbContextMocker();
            mockContext.AddSupportTo(dataSet);

            var repository = new TestDataRepository(mockContext.Object);
            var result = repository.GetAll();

            Assert.AreEqual(3, result.Count());
        }
    }
}
