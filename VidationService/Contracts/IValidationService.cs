namespace ValidationService.Contracts
{
    public interface IValidationService
    {
        /// <summary>
        /// Validate passed object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        ValidationResult Validate<T>(T value) where T : class;
    }
}
