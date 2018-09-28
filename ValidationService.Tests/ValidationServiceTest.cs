using System.Linq;
using Moq;
using CustomLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationService.Tests
{
    [TestClass]
    public class ValidationServiceTest
    {
        private readonly Mock<ICustomLogger> moq;
        private readonly Services.ValidationService validationService;


        public ValidationServiceTest()
        {
            moq = new Mock<ICustomLogger>();
            validationService = new Services.ValidationService(moq.Object);
        }

        
        [TestMethod]
        public void Validate_ValidModel_Valid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "test",
                IntTestValue = 5,
                RequiredValue = "as"
            };

            ValidationResult actualResult = validationService.Validate(testEntity);
            
            moq.Verify(logger => logger.LogWarn(It.IsAny<string>()), Times.Never);
            Assert.IsTrue(actualResult.Ok);
        }
        

        [TestMethod]
        public void Validate_StringMoreThanMax_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "testtesttest",
                IntTestValue = 5,
                RequiredValue = "as"
            };
            
            ValidationResult actualResult = validationService.Validate(testEntity);

            moq.Verify(logger => logger.LogWarn(It.IsAny<string>()), Times.Exactly(actualResult.Errors.Count()));
            Assert.IsFalse(actualResult.Ok);
        }

        [TestMethod]
        public void Validate_StringIsNull_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = null,
                IntTestValue = 4,
                RequiredValue = "as"
            };

            ValidationResult actualResult = validationService.Validate(testEntity);

            moq.Verify(logger => logger.LogWarn(It.IsAny<string>()), Times.Exactly(actualResult.Errors.Count()));
            Assert.IsFalse(actualResult.Ok);
        }

        [TestMethod]
        public void Validate_IntMoreThanMax_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "test",
                IntTestValue = 50,
                RequiredValue = "as"
            };
            
            ValidationResult actualResult = validationService.Validate(testEntity);

            moq.Verify(logger => logger.LogWarn(It.IsAny<string>()), Times.Exactly(actualResult.Errors.Count()));
            Assert.IsFalse(actualResult.Ok);
        }

        [TestMethod]
        public void Validate_IntLessThanMin_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "test",
                IntTestValue = 2,
                RequiredValue = "as"
            };
            
            ValidationResult actualResult = validationService.Validate(testEntity);

            moq.Verify(logger => logger.LogWarn(It.IsAny<string>()), Times.Exactly(actualResult.Errors.Count()));
            Assert.IsFalse(actualResult.Ok);
        }

        [TestMethod]
        public void Validate_Required_Invalid()
        {
            TestEntity testEntity = new TestEntity()
            {
                StringTestValue = "test",
                IntTestValue = 3,
                RequiredValue = null
            };

            ValidationResult actualResult = validationService.Validate(testEntity);

            moq.Verify(logger => logger.LogWarn(It.IsAny<string>()), Times.Exactly(actualResult.Errors.Count()));
            Assert.IsFalse(actualResult.Ok);
        }
    }
}
