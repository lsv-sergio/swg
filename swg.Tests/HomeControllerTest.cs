using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using swg.Controllers;
using swg.Core.Creators;
using swg.Core.Dto;
using swg.Core.Operations;
using swg.Core.Services;
using System.Web.Mvc;

namespace swg.Tests {
    [TestClass]
    public class HomeControllerTest {

        private HomeController GetController(Guid resultId) {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Execute(It.IsAny<double>(), It.IsAny<double>())).Returns<double, double>((x, y) => x + y);
            operation.Setup(x => x.OperationName).Returns(() => "fake_operation");
            var creator = new Mock<IOperationCreator>();
            creator.Setup(x => x.CreateOperation()).Returns(() => operation.Object);
            var operationService = new Mock<IOperationStorage>();
            operationService.Setup(x => x.GetCreatorByOperationName(It.IsAny<string>())).Returns(() => creator.Object);
            var resultStorage = new Mock<IResultStorage>();
            
            resultStorage.Setup(x => x.SaveResultToStorage(It.IsAny<double>())).Returns(() => Task.Run(() => resultId));
            resultStorage.Setup(x => x.GetResultByKeyAsync(It.IsAny<Guid>())).Returns(() => Task.Run<double?>(() => 3.0));
            var logger = new Mock<IOperationLogger>();
            logger.Setup(x => x.WriteOperationLogAsync(It.IsAny<OperationLogParameter>())).Returns(() => Task.Run(() => { }));

            return new HomeController(operationService.Object, resultStorage.Object, logger.Object);
        }

        [TestMethod]
        public void MakeOperation_Returns_OperationResultKeyAsync() {
            var resultId = Guid.NewGuid();
            var controller = GetController(resultId);
            var result = controller.MakeOperation("some_operation", 1, 2).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.IsTrue(result.Data.ToString().Contains(resultId.ToString()));
        }

        [TestMethod]
        public void GetResult_Returns_OPerationResult_By_Key() {
            var resultId = Guid.NewGuid();
            var controller = GetController(resultId);
            controller.MakeOperation("some_operation", 1, 2).GetAwaiter();
            var result = controller.GetResult(resultId).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.IsTrue(result.Data.ToString().Contains("3"));
        }
    }
}
