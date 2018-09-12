namespace ValidationService.Contracts
{
    public interface IValidationService
    {
        ValidationResult Validate<T>(T value) where T : class;
    }
}
