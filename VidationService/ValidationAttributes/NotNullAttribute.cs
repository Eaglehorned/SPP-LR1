using System;

namespace ValidationService.ValidationAttributes
{
    public class NotNullAttribute : MyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                ErrorMessage = "Value cant be null.";

                return false;
            }

            return true;
        }
    }
}
