using System;

namespace ValidationService.ValidationAttributes
{
    public class RequiredValidationAttribute : Attribute, ICustomValidationAttribute<object> 
    {
        public string ErrorMessage { get; }

        public RequiredValidationAttribute()
        {
            ErrorMessage = Resources.Resource.RequiredAttributeError;
        }

        public bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            return true;
        }
    }
}
