using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarrantyRegistrationApp.Controllers.Api;
using WarrantyRegistrationApp.Models;
using WarrantyRegistrationApp.Repository;

namespace WarrantyRegUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod()
        {

        }

        [TestMethod]
        public async Task TestGetAllAsyncxMethodAsync()
        {
            ActionResult<IEnumerable<Customer>> customers = new List<Customer>()
            {
                new Customer { CustomerId = 1,FirstName="Sam",LastName="Antwi", Address = "314 Some Place", City ="Dayton",CompanyName="Test",ZipCode="45424",PhoneNumber="937-444-0000",State="New York" },
                new Customer { CustomerId = 2,FirstName="Sam2",LastName="Antwi2", Address = "314 Some Place2", City ="Dayton2",CompanyName="Test2",ZipCode="45422",PhoneNumber="937-444-0002",State="New York2" },
                new Customer { CustomerId = 3,FirstName="Sam3",LastName="Antwi3", Address = "314 Some Place3", City ="Dayton3",CompanyName="Test3",ZipCode="45423",PhoneNumber="937-444-0003",State="New York3" },
            };

            //Mock IRepository of books
            Mock<IRepository<Customer>> mockcustomersAPIController = new Mock<IRepository<Customer>>();
            mockcustomersAPIController.Setup(b => b.GetAllAsync()).Returns(Task.FromResult(customers));

            //Pass in the IRepository books
            CustomersAPIController customersAPIController = new CustomersAPIController(mockcustomersAPIController.Object);
            ActionResult<IEnumerable<Customer>> result = await customersAPIController.GetCustomers() as ActionResult<IEnumerable<Customer>>;
            Assert.IsNotNull(result);
        }
    }
}
