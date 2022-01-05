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
    public class VisitContollerTests
    {
        [TestMethod]
        public async Task Add_Visit_ReturnsOk()
        {
            var visit = new Visit
            {
                Id = 1,
                VisitNumber = "Test"
            };
            //Arrange
            var mock = new Mock<IVisitService>();
            mock.Setup(o => o.AddAsync(visit)).ReturnsAsync(visit);
            VisitController visitController = new VisitController(mock.Object);
            //Action
            var result = await visitController.AddVisitAsync(visit);
            //Assert
            Assert.IsInstanceOfType(result,new OkResult().GetType());
        }
    }
}