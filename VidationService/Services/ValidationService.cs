using System.Reflection;
using CustomLogger;
using System.Linq;
using ValidationService.Contracts;
using ValidationService.ValidationAttributes;
using System.Collections.Generic;

namespace ValidationService.Services
{
    public class ValidationService : IValidationService
    {
        ICustomLogger logger;

        private bool resultOk = true;

        private List<string> errorMessages = new List<string>();

        public ValidationService(ICustomLogger logger)
        {
            this.logger = logger;
        }

        public ValidationResult Validate<T>(T instance) where T : class
        {
            ICollection<KeyValuePair<PropertyInfo, object>> propertyValuePairs = instance.GetPropertiesToValidate().GetPropertiesValues(instance);

            foreach (KeyValuePair<PropertyInfo, object> propertyValuePair in propertyValuePairs)
            {
                if (!ValidateProperty(propertyValuePair.Key.GetCustomValidationAttributes(), propertyValuePair.Value))
                {
                    resultOk = false;
                }
            }

            return new ValidationResult(
                resultOk,
                errorMessages
            );
        }

        private bool ValidateProperty<T>(IEnumerable<ICustomValidationAttribute> attributes, T value)
        {
            bool propertyValidationResult = true;

            foreach (ICustomValidationAttribute attribute in attributes)
            {
                if (!ValidateAttribute(attribute, value))
                {
                    propertyValidationResult = false;
                }
            }

            return propertyValidationResult;
        }
        
        private bool ValidateAttribute<T>(ICustomValidationAttribute attribute, T value)
        {
            if (!attribute.IsValid(value))
            {
                logger.LogWarn(attribute.ErrorMessage);
                errorMessages.Add(attribute.ErrorMessage);

                return false;
            }

            return true;
        }
    }
}
