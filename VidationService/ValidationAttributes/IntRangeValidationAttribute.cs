using System;
using System.ComponentModel.DataAnnotations;

namespace ValidationService.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IntRangeAttributeAttribute : NotNullAttribute
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public IntRangeAttributeAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public override bool IsValid(object value)
        {
            if (!base.IsValid(value))
            {
                return false;
            }

            if ((int)value < Min || Max < (int)value)
            {
                ErrorMessage = $"Value should be in range [{Min};{Max}].";

                return false;
            }

            return true;
        }
    }
}
