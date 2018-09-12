using System;
using ValidationService;
using ValidationService.Tests;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestEntity t = new TestEntity()
            {
                StringTestValue = "asdasdasdasdasdasdasdasdasd",
                IntTestValue = 10,
                NotNullTestValue = "asd"
            };

            ValidationService.Services.ValidationService validationService = new ValidationService.Services.ValidationService(
                new MyLogger.MyLogger(),
                new ValidationService.Services.ObjectService());

            ValidationResult validationResult = validationService.Validate(t);

            Console.WriteLine(validationResult.Ok);
            foreach (var err in validationResult.Errors)
            {
                Console.WriteLine(err);
            }
            Console.ReadLine();
        }
    }
}
