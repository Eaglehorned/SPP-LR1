using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidationService.Contracts;

namespace ValidationService.Tests
{
    [TestClass]
    public class ValidationServiceTest
    {
        private readonly Services.ValidationService validationService = new Services.ValidationService(new MyLogger.MyLogger(), new Services.ObjectService());

        [TestMethod]
        public void Validate_ValidModel_Valid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "test",
                IntTestValue = 5,
                NotNullTestValue = "as"
            };

            ValidationResult actualResult = validationService.Validate(testEntity);
            Assert.IsTrue(actualResult.Ok);
        }

        [TestMethod]
        public void Validate_StringMoreThanMax_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "testtesttest",
                IntTestValue = 5,
                NotNullTestValue = "as"
            };
            
            ValidationResult actualResult = validationService.Validate(testEntity);
            Assert.IsFalse(actualResult.Ok);
        }

        [TestMethod]
        public void Validate_StringIsNull_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = null,
                IntTestValue = 2,
                NotNullTestValue = "as"
            };

            ValidationResult actualResult = validationService.Validate(testEntity);
            Assert.IsFalse(actualResult.Ok);
        }

        [TestMethod]
        public void Validate_IntMoreThanMax_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "test",
                IntTestValue = 50,
                NotNullTestValue = "as"
            };
            
            ValidationResult actualResult = validationService.Validate(testEntity);
            Assert.IsFalse(actualResult.Ok);
        }

        [TestMethod]
        public void Validate_IntLessThanMin_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "test",
                IntTestValue = 2,
                NotNullTestValue = "as"
            };
            
            ValidationResult actualResult = validationService.Validate(testEntity);
            Assert.IsFalse(actualResult.Ok);
        }

        [TestMethod]
        public void Validate_NotNullIsNull_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "test",
                IntTestValue = 2,
                NotNullTestValue = null
            };

            ValidationResult actualResult = validationService.Validate(testEntity);
            Assert.IsFalse(actualResult.Ok);
        }
    }
}
