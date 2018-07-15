using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using swg.Core.Creators;
using swg.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace swg.Tests {
    [TestClass]
    public class OperationStorageTest {

        private IOperationStorage _storage;

        private IOperationCreator GetFakeCreator(string name) {
            var fakeOperationCreator = new Mock<IOperationCreator>();
            fakeOperationCreator.Setup(x => x.GetOperationName()).Returns(() => name);
            return fakeOperationCreator.Object;
        }

        [TestInitialize]
        public void Init() {
            _storage = OperationStorage.GetInstance();
        }

        [TestMethod]
        public void OperationStorage_Can_Register_OperationCreator() {

            var fakeCreator = GetFakeCreator("fake_operation1");
            var count = _storage.GetAllOperationNames().Count();
            _storage.AddOperationCreator(fakeCreator);
            var newCount = _storage.GetAllOperationNames().Count();

            Assert.AreEqual(newCount, count + 1);
        }

        [TestMethod]
        public void OperationStorage_Can_Register_Array_Of_OperationCreators() {

            var operators = new List<IOperationCreator>() {
               GetFakeCreator("fake_operation2"),
               GetFakeCreator("fake_operation3")
            };
            var count = _storage.GetAllOperationNames().Count();
            _storage.AddOperationCreators(operators);
            var newCount = _storage.GetAllOperationNames().Count();

            Assert.AreEqual(newCount, count + operators.Count);
        }

        [TestMethod]
        public void OperationStorage_throw_Exception_When_Parameters_Is_Null() {

            Assert.ThrowsException<ArgumentNullException>(() => _storage.AddOperationCreator(null));
            Assert.ThrowsException<ArgumentNullException>(() => _storage.AddOperationCreators(null));

        }

        [TestMethod]
        public void OperationStorage_Return_Instance_Of_IOperationCreator_By_OparationName() {

            var creatorName = "fake_operation4";
            var fakeCreator = GetFakeCreator(creatorName);
            _storage.AddOperationCreator(fakeCreator);
            var creator = _storage.GetCreatorByOperationName(creatorName);

            Assert.IsNotNull(creator);
            Assert.IsInstanceOfType(creator, typeof(IOperationCreator));

        }

        [TestMethod]
        public void OperationStorage_Returns_All_Registered_Operation_Names() {

            var creatorName = "fake_operation5";
            var fakeCreator = GetFakeCreator(creatorName);
            _storage.AddOperationCreator(fakeCreator);
            var allOperationNames = _storage.GetAllOperationNames();

            Assert.IsTrue(allOperationNames.Contains(creatorName));

        }
    }
}
