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
                StringTestValue = "ad",
                IntTestValue = 100,
                RequiredValue = null
            };

            ValidationService.Services.ValidationService validationService = new ValidationService.Services.ValidationService(
                new CustomLogger.CustomLogger());

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
