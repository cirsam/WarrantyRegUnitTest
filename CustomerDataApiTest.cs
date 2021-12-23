using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WarrantyRegistrationApp.Controllers.Api;
using WarrantyRegistrationApp.Models;
using WarrantyRegistrationApp.Repository;

namespace WarrantyRegUnitTest
{
    [TestClass]
    public class CustomerData
    {
        private Mock<IRepository<Customer>> _mockcustomersAPIController;

        [TestInitialize]
        public void TestInitializer()
        {
            _mockcustomersAPIController = new Mock<IRepository<Customer>>();
        }

        [TestMethod]
        public async Task TestGetAllAsyncMethodAsync()
        {
            ActionResult<IEnumerable<Customer>> customers = new List<Customer>()
            {
                new Customer { CustomerId = 1,FirstName="Sam",LastName="Antwi", Address = "314 Some Place", City ="Dayton",CompanyName="Test",ZipCode="45424",PhoneNumber="937-444-0000",State="New York" },
                new Customer { CustomerId = 2,FirstName="Sam2",LastName="Antwi2", Address = "314 Some Place2", City ="Dayton2",CompanyName="Test2",ZipCode="45422",PhoneNumber="937-444-0002",State="New York2" },
                new Customer { CustomerId = 3,FirstName="Sam3",LastName="Antwi3", Address = "314 Some Place3", City ="Dayton3",CompanyName="Test3",ZipCode="45423",PhoneNumber="937-444-0003",State="New York3" },
            };

            //Mock IRepository of Customers
            _mockcustomersAPIController.Setup(b => b.GetAllAsync()).Returns(Task.FromResult(customers));

            //Pass in the IRepository Customers
            CustomersAPIController customersAPIController = new CustomersAPIController(_mockcustomersAPIController.Object);
            ActionResult<IEnumerable<Customer>> result = await customersAPIController.GetCustomers();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestGetByIDAsyncMethodAsync()
        {
            Customer customers = new Customer { CustomerId = 1, FirstName = "Sam", LastName = "Antwi", Address = "314 Some Place", City = "Dayton", CompanyName = "Test", ZipCode = "45424", PhoneNumber = "937-444-0000", State = "New York" };

            //Mock IRepository of Customer
            Mock<IRepository<Customer>> mockcustomersAPIController = new Mock<IRepository<Customer>>();
            mockcustomersAPIController.Setup(b => b.GetByIDAsync(It.IsAny<int>())).Returns(Task.FromResult(customers));

            //Pass in the IRepository Customer
            CustomersAPIController customersAPIController = new CustomersAPIController(mockcustomersAPIController.Object);
            ActionResult<Customer> result = await customersAPIController.GetCustomer(1) as ActionResult<Customer>;

            Assert.AreEqual(customers, result.Value);
        }

        [TestMethod]
        public async Task TestUpdateAsyncMethodAsync()
        {
            Customer customers = new Customer { CustomerId = 1, FirstName = "Sam", LastName = "Antwi", Address = "314 Some Place", City = "Dayton", CompanyName = "Test", ZipCode = "45424", PhoneNumber = "937-444-0000", State = "New York" };

            //Mock IRepository of Customer
            _mockcustomersAPIController.Setup(b => b.UpdateAsync(It.IsAny<Customer>())).Callback<Customer>(s => customers = s);

            //Pass in the IRepository Customer
            CustomersAPIController customersAPIController = new CustomersAPIController(_mockcustomersAPIController.Object);
            var result = await customersAPIController.PutCustomer(customers.CustomerId, customers) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(204, result.StatusCode);
        }

        [TestMethod]
        public async Task TestUpdateAsyncMethodAsyncBadRequest()
        {
            Customer customers = new Customer { CustomerId = 2, FirstName = "Sam", LastName = "Antwi", Address = "314 Some Place", City = "Dayton", CompanyName = "Test", ZipCode = "45424", PhoneNumber = "937-444-0000", State = "New York" };

            //Mock IRepository of Customer
            _mockcustomersAPIController.Setup(b => b.UpdateAsync(It.IsAny<Customer>())).Callback<Customer>(s => customers = s);

            //Pass in the IRepository Customer
            CustomersAPIController customersAPIController = new CustomersAPIController(_mockcustomersAPIController.Object);
            var result = await customersAPIController.PutCustomer(3,customers) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task TestInsertAsyncMethodAsync()
        {
            Customer customers = new Customer { CustomerId = 2, FirstName = "Sam", LastName = "Antwi", Address = "314 Some Place", City = "Dayton", CompanyName = "Test", ZipCode = "45424", PhoneNumber = "937-444-0000", State = "New York" };

            //Mock IRepository of Customer
            _mockcustomersAPIController.Setup(b => b.InsertAsync(It.IsAny<Customer>())).Returns(Task.FromResult(customers));

            //Pass in the IRepository Customer
            CustomersAPIController customersAPIController = new CustomersAPIController(_mockcustomersAPIController.Object);
            var result = await customersAPIController.PostCustomer(customers) as ActionResult<Customer>;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestDeleteAsyncMethodAsync()
        {
            Customer customers = new Customer { CustomerId = 2, FirstName = "Sam", LastName = "Antwi", Address = "314 Some Place", City = "Dayton", CompanyName = "Test", ZipCode = "45424", PhoneNumber = "937-444-0000", State = "New York" };

            //Mock IRepository of Customer
            _mockcustomersAPIController.Setup(b => b.GetByIDAsync(It.IsAny<int>())).Returns(Task.FromResult(customers));
            _mockcustomersAPIController.Setup(b => b.Delete(It.IsAny<Customer>()));

            //Pass in the IRepository Customer
            CustomersAPIController customersAPIController = new CustomersAPIController(_mockcustomersAPIController.Object);
            var result = await customersAPIController.DeleteCustomer(customers.CustomerId) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(204, result.StatusCode);
        }
    }
}
