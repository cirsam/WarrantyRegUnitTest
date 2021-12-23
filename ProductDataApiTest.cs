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
    public class ProductData
    {
        private Mock<IRepository<Product>> _mockProductsAPIController;

        [TestInitialize]
        public void TestInitializer()
        {
            _mockProductsAPIController = new Mock<IRepository<Product>>();
        }

        [TestMethod]
        public async Task TestGetAllAsyncMethodAsync()
        {
            ActionResult<IEnumerable<Product>> product = new List<Product>()
            {
                new Product { ProductId=1,ProductName="monitor", Manufacturer="testCompany", ProductSerialNumber="123WES345B1" },
                new Product { ProductId=2,ProductName="monitor2", Manufacturer="testCompany2", ProductSerialNumber="123WES345B2" },
                new Product { ProductId=3,ProductName="monitor3", Manufacturer="testCompany3", ProductSerialNumber="123WES345B3" }
            };

            //Mock IRepository of Products
            _mockProductsAPIController.Setup(b => b.GetAllAsync()).Returns(Task.FromResult(product));

            //Pass in the IRepository Products
            ProductsAPIController productsAPIController = new ProductsAPIController(_mockProductsAPIController.Object);
            ActionResult<IEnumerable<Product>> result = await productsAPIController.GetProducts();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestGetByIDAsyncMethodAsync()
        {
            Product products = new Product { ProductId = 1, ProductName = "monitor", Manufacturer = "testCompany", ProductSerialNumber = "123WES345B1" };

            //Mock IRepository of Products
            _mockProductsAPIController.Setup(b => b.GetByIDAsync(It.IsAny<int>())).Returns(Task.FromResult(products));

            //Pass in the IRepository Products
            ProductsAPIController productsAPIController = new ProductsAPIController(_mockProductsAPIController.Object);
            ActionResult<Product> result = await productsAPIController.GetProduct(1);

            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public async Task TestUpdateAsyncMethodAsync()
        {
            Product products = new Product { ProductId = 1, ProductName = "monitor", Manufacturer = "testCompany", ProductSerialNumber = "123WES345B1" };

            //Mock IRepository of Products
            _mockProductsAPIController.Setup(b => b.UpdateAsync(It.IsAny<Product>())).Callback<Product>(s => products = s);

            //Pass in the IRepository Products
            ProductsAPIController productsAPIController = new ProductsAPIController(_mockProductsAPIController.Object);
            var result = await productsAPIController.PutProduct(products.ProductId, products) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(204, result.StatusCode);
        }

        [TestMethod]
        public async Task TestUpdateAsyncMethodAsyncBadRequest()
        {
            Product products = new Product { ProductId = 1, ProductName = "monitor", Manufacturer = "testCompany", ProductSerialNumber = "123WES345B1" };

            //Mock IRepository of Products
            _mockProductsAPIController.Setup(b => b.UpdateAsync(It.IsAny<Product>())).Callback<Product>(s => products = s);

            //Pass in the IRepository Products
            ProductsAPIController productsAPIController = new ProductsAPIController(_mockProductsAPIController.Object);
            var result = await productsAPIController.PutProduct(3,products) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task TestInsertAsyncMethodAsync()
        {
            Product products = new Product { ProductId = 1, ProductName = "monitor", Manufacturer = "testCompany", ProductSerialNumber = "123WES345B1" };

            //Mock IRepository of Product
            _mockProductsAPIController.Setup(b => b.InsertAsync(It.IsAny<Product>())).Returns(Task.FromResult(products));

            //Pass in the IRepository Product
            ProductsAPIController productsAPIController = new ProductsAPIController(_mockProductsAPIController.Object);
            var result = await productsAPIController.PostProduct(products);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestDeleteAsyncMethodAsync()
        {
            Product products = new Product { ProductId = 1, ProductName = "monitor", Manufacturer = "testCompany", ProductSerialNumber = "123WES345B1" };

            //Mock IRepository of Product
            _mockProductsAPIController.Setup(b => b.GetByIDAsync(It.IsAny<int>())).Returns(Task.FromResult(products));
            _mockProductsAPIController.Setup(b => b.Delete(It.IsAny<Product>()));

            //Pass in the IRepository Product
            ProductsAPIController productsAPIController = new ProductsAPIController(_mockProductsAPIController.Object);
            var result = await productsAPIController.DeleteProduct(products.ProductId) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(204, result.StatusCode);
        }
    }
}
