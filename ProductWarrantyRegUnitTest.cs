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
    public class UnitTestWarrantyReg
    {
        //[TestMethod]
        //public async Task TestGetAllAsyncMethodAsync()
        //{
        //    ActionResult<IEnumerable<ProductWarrantyData>> productWarrantyData = new List<ProductWarrantyData>()
        //    {
        //        new ProductWarrantyData { ProdWarrantyId=1, CustomerId=1, ProductId=1, ProductSerialNumber="123WES345B" },
        //        new ProductWarrantyData { ProdWarrantyId=2, CustomerId=2, ProductId=2, ProductSerialNumber="123WES345A" },
        //        new ProductWarrantyData { ProdWarrantyId=3, CustomerId=3, ProductId=3, ProductSerialNumber="123WES345R" },
        //    };

        //    //Mock IRepository of Customers
        //    Mock<IRepository<ProductWarrantyData>> mockProductWarrantyAPIController = new Mock<IRepository<ProductWarrantyData>>();
        //    mockProductWarrantyAPIController.Setup(b => b.GetAllAsync()).Returns(Task.FromResult(productWarrantyData));

        //    //Pass in the IRepository Customers
        //    ProductWarrantyAPIController productWarrantyAPIController = new ProductWarrantyAPIController(mockProductWarrantyAPIController.Object);
        //    ActionResult<IEnumerable<ProductWarrantyData>> result = await productWarrantyAPIController.GetProductWarrantyDatas() as ActionResult<IEnumerable<ProductWarrantyData>>;
            
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(productWarrantyData, result.Value);
        //}

        [TestMethod]
        public async Task TestFindProductWarrantyIsValid()
        {
            IEnumerable<Product> product = new List<Product>()
            {
                new Product { ProductId=1,ProductName="monitor", Manufacturer="testCompany", ProductSerialNumber="123WES345B1" },
                new Product { ProductId=2,ProductName="monitor2", Manufacturer="testCompany2", ProductSerialNumber="123WES345B2" },
                new Product { ProductId=3,ProductName="monitor3", Manufacturer="testCompany3", ProductSerialNumber="123WES345B3" }
            };

            //Mock IRepository of Product
            Mock<IRepository<Product>> mockproductsAPIController = new Mock<IRepository<Product>>();
            mockproductsAPIController.Setup(b => b.GetBySerialNumberAsync(It.IsAny<string>())).Returns(Task.FromResult(product));
            
            IEnumerable<Customer> customers = new List<Customer>()
            {
                new Customer { CustomerId = 1,FirstName="Sam",LastName="Antwi", Address = "314 Some Place", City ="Dayton",CompanyName="Test",ZipCode="45424",PhoneNumber="937-444-0000",State="New York" },
                new Customer { CustomerId = 2,FirstName="Sam2",LastName="Antwi2", Address = "314 Some Place2", City ="Dayton2",CompanyName="Test2",ZipCode="45422",PhoneNumber="937-444-0002",State="New York2" },
                new Customer { CustomerId = 3,FirstName="Sam3",LastName="Antwi3", Address = "314 Some Place3", City ="Dayton3",CompanyName="Test3",ZipCode="45423",PhoneNumber="937-444-0003",State="New York3" },
            };

            //Mock IRepository of Customers
            Mock<IRepository<Customer>> mockcustomersAPIController = new Mock<IRepository<Customer>>();
            mockcustomersAPIController.Setup(b => b.GetBySerialNumberAsync(It.IsAny<string>())).Returns(Task.FromResult(customers));

            ProductWarrantyData productWarrantyData = new ProductWarrantyData { ProdWarrantyId = 1, CustomerId = 1, ProductId = 1, ProductSerialNumber = "123WES345B" };

            //Mock IRepository of ProductWarranty
            Mock<IRepository<ProductWarrantyData>> mockProductWarrantyAPIController = new Mock<IRepository<ProductWarrantyData>>();
            mockProductWarrantyAPIController.Setup(b => b.GetByIDAsync(It.IsAny<int>())).Returns(Task.FromResult(productWarrantyData));

            //Pass in the IRepository Customers
            ProductWarrantyAPIController productWarrantyAPIController = new ProductWarrantyAPIController(mockProductWarrantyAPIController.Object, mockcustomersAPIController.Object, mockproductsAPIController.Object);
            var result = await productWarrantyAPIController.IsProductWarrantyValid(productWarrantyData);

            Assert.IsNotNull(result);
            Assert.AreEqual(productWarrantyData, result);
        }


    }
}