using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace UnitOfWork
{
    [TestClass]
    public class IntegratedTestForRepository
    {
        private static IUnitOfWork unitOfWork = new UnitOfWork(new EFContext());

        private static List<Employee> fakeExistingEmployees = new List<Employee>() {
            new Employee() { Id = Guid.NewGuid(), Name = "Paul" },
            new Employee() { Id = Guid.NewGuid(), Name = "Tom" },
            new Employee() { Id = Guid.NewGuid(), Name = "Ken" },
            new Employee() { Id = Guid.NewGuid(), Name = "Bob" },
            new Employee() { Id = Guid.NewGuid(), Name = "John" }
        };

        private static Employee testNewEmployee = new Employee() { Id = Guid.NewGuid(), Name = "Mary" };

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize]
        public static void BeforeTestting(TestContext context)
        {
            fakeExistingEmployees.ForEach(e => unitOfWork.Employees.Add(e));
            unitOfWork.Complete();
        }

        [TestMethod]
        public void TestAddEmploye_success()
        {
            Employee actual;

            int expected = 1;

            actual = unitOfWork.Employees.Add(testNewEmployee);

            expected = unitOfWork.Complete();


            Assert.IsNotNull(actual);
            Assert.IsTrue(expected == 1);
        }

        [TestMethod]
        public void TestGetEmployee_by_id_success()
        {
            Employee actual;

            actual = unitOfWork.Employees.Get(testNewEmployee.Id);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void TestRemoveEmployee_success()
        {
            Employee actual;

            int expected = 1;


            actual = unitOfWork.Employees.Remove(testNewEmployee);

            expected = unitOfWork.Complete();

            Assert.IsNotNull(actual);
            Assert.IsTrue(expected == 1);
        }

        [TestMethod]
        public void TestFindEmployees_by_name_looklike_success()
        {
            Expression<Func<Employee, bool>> predicate = e => e.Name.Contains("o");

            IEnumerable<Employee> actual;


            actual = unitOfWork.Employees.Find(predicate);


            Assert.IsTrue(actual.All(predicate.Compile()));
        }


        [TestMethod]
        public void TestGetTopEmployees_orderby_name_success()
        {
            IEnumerable<Employee> actual;

            actual = unitOfWork.Employees.GetTopEmployees(5);


            var expected = 5;

            Assert.IsTrue(actual.Count() == expected);
        }


        [ClassCleanup]
        public static void AfterTestting()
        {
            fakeExistingEmployees.ForEach(e => unitOfWork.Employees.Remove(e));
            unitOfWork.Complete();
            unitOfWork.Dispose();
        }
    }
}
