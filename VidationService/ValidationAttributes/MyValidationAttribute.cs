using System;

namespace ValidationService.ValidationAttributes
{
    public abstract class MyValidationAttribute : Attribute
    {
        public string ErrorMessage { get; set; }

        public abstract bool IsValid(object value);
    }
}
