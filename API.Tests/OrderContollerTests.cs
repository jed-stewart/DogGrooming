using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Interface;
using Moq;
using Data.Domain;
using API.Controllers.V1;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Tests
{
    [TestClass]
    public class OrderContollerTests
    {
        [TestMethod]
        public async Task Add_Order_ReturnsOk()
        {
            var order = new Order
            {
                Id = 1,
                OrderNumber = "Test"
            };
            //Arrange
            var mock = new Mock<IOrderService>();
            mock.Setup(o => o.AddAsync(order)).ReturnsAsync(order);
            OrderController orderController = new OrderController(mock.Object);
            //Action
            var result = await orderController.AddOrderAsync(order);
            //Assert
            Assert.IsInstanceOfType(result,new OkResult().GetType());
        }
    }
}