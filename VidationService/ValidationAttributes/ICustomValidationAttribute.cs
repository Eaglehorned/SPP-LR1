namespace ValidationService.ValidationAttributes
{
    public interface ICustomValidationAttribute
    {
        string ErrorMessage { get; }

        bool IsValid(object value);
    }

    public interface ICustomValidationAttribute<T> : ICustomValidationAttribute
    {
        new string ErrorMessage { get; }

        bool IsValid(T value);
    }

    public interface IIntRangeValidationAttribute : ICustomValidationAttribute<int>
    {
        new bool IsValid(int value);
    }

    public interface IStringMaxLengthValidationAttribute : ICustomValidationAttribute<string>
    {
        new bool IsValid(string value);
    }
}
