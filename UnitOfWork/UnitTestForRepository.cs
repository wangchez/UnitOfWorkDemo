using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UnitOfWork
{
    [TestClass]
    public class UnitTestForRepository
    {
        private IUnitOfWork unitOfWork;

        private static IQueryable<Employee> fakeExistingEmployees = new List<Employee>() {
            new Employee() { Id = Guid.NewGuid(), Name = "Paul" },
            new Employee() { Id = Guid.NewGuid(), Name = "Tom" },
            new Employee() { Id = Guid.NewGuid(), Name = "Ken" },
            new Employee() { Id = Guid.NewGuid(), Name = "Bob" },
            new Employee() { Id = Guid.NewGuid(), Name = "John" }
        }.AsQueryable();

        public UnitTestForRepository()
        {
            var mockSet = new Mock<DbSet<Employee>>(); 
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(fakeExistingEmployees.Provider); 
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(fakeExistingEmployees.Expression); 
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(fakeExistingEmployees.ElementType); 
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(() => fakeExistingEmployees.GetEnumerator()); 
 
            var mockContext = new Mock<EFContext>(); 
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);
            unitOfWork = new UnitOfWork(mockContext.Object);
        }


        [TestMethod]
        public void TestGetTopEmployees_orderby_name_success()
        {
            IEnumerable<Employee> actual;

            actual = unitOfWork.Employees.GetTopEmployees(3);


            var expected = 3;

            Assert.IsTrue(actual.Count() == expected);
        }
    }
}
