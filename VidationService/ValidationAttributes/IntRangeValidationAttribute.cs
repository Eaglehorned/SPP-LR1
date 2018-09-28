using System;

namespace ValidationService.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IntRangeAttribute : Attribute, IIntRangeValidationAttribute, ICustomValidationAttribute<int>
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public string ErrorMessage { get; }

        public IntRangeAttribute(int min, int max)
        {
            Min = min;
            Max = max;

            ErrorMessage = Resources.Resource.IntRangeAttributeError;
        }

        public bool IsValid(object value)
        {
            int castedValue;

            try
            {
                castedValue = Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return false;
            }

            return IsValid(castedValue);
        }

        public bool IsValid(int value)
        {
            return Min < value && value < Max;
        }
    }
}
