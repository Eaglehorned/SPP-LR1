using System;
using System.ComponentModel.DataAnnotations;

namespace ValidationService.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class StringLengthAttribute : NotNullAttribute
    {
        public int MaxLength { get; private set; }

        public StringLengthAttribute(int maxLength)
        {
            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException("maxLength cant be less or equal to 0.");
            }

            MaxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            if (!base.IsValid(value))
            {
                return false;
            }

            if (value == null)
            {
                ErrorMessage = "Value cant be null.";

                return false;
            }

            if (((string)value).Length > MaxLength)
            {
                ErrorMessage = $"Value length must be less then {MaxLength}.";

                return false;
            }

            return true;
        }
    }
}
