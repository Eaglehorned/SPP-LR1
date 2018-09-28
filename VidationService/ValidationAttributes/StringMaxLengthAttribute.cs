using System;

namespace ValidationService.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class StringMaxLengthAttribute : Attribute, IStringMaxLengthValidationAttribute ,ICustomValidationAttribute<string>
    {
        public int MaxLength { get; private set; }

        public string ErrorMessage { get; }

        public StringMaxLengthAttribute(int maxLength)
        {
            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException("maxLength cant be less or equal to 0.");
            }

            MaxLength = maxLength;

            ErrorMessage = Resources.Resource.StringMaxLengthAttributeError;
        }

        public bool IsValid(object value)
        {
            string castedValue = (string)value;

            if (castedValue == null)
            {
                return false;
            }

            return IsValid(castedValue);
        }

        public bool IsValid(string value)
        {
            if (value.Length > MaxLength)
            { 
                return false;
            }

            return true;
        }
    }
}
