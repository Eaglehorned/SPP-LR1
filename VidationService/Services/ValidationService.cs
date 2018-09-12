using System.Reflection;
using MyLogger;
using System.Linq;
using ValidationService.Contracts;
using ValidationService.ValidationAttributes;
using System.Collections.Generic;

namespace ValidationService.Services
{
    public class ValidationService : IValidationService
    {
        IObjectService objectService;
        ILogger logger;

        public ValidationService(ILogger logger, IObjectService objectService)
        {
            this.logger = logger;
            this.objectService = objectService;
        }

        public ValidationResult Validate<T>(T instance) where T : class
        {
            IEnumerable<PropertyInfo> properties = objectService.GetPropertiesToValidate(instance);

            ICollection<KeyValuePair<PropertyInfo, object>> propertyValuePairs = objectService.GetPropertiesValues(properties, instance);

            IEnumerable<AttributeValidationResult> attributeValidationResults = GetAttributesValidationResults(propertyValuePairs);

            return new ValidationResult
            {
                Ok = IsValidationResultOk(attributeValidationResults),
                Errors = GetAllErrors(attributeValidationResults)
            };
        }

        private bool IsValidationResultOk(IEnumerable<AttributeValidationResult> attributeValidationResults)
        {
            return !attributeValidationResults.Any(el => !el.Ok);
        }

        private IEnumerable<string> GetAllErrors(IEnumerable<AttributeValidationResult> attributeValidationResults)
        {
            List<string> errors = new List<string>();

            foreach (AttributeValidationResult attr in attributeValidationResults)
            {
                if (!attr.Ok)
                {
                    logger.Warn(attr.Error);
                    errors.Add(attr.Error);
                }
            }

            return errors;
        }

        private IEnumerable<AttributeValidationResult> GetAttributesValidationResults(IEnumerable<KeyValuePair<PropertyInfo, object>> propertyValuePairs)
        {
            List<AttributeValidationResult> attributeValidationResults = new List<AttributeValidationResult>();

            foreach (var propertyValuePair in propertyValuePairs)
            {
                attributeValidationResults.AddRange(ValidateProperty(objectService.GetMyValidationAttributes(propertyValuePair.Key), propertyValuePair.Value));
            }

            return attributeValidationResults;
        }

        private IEnumerable<AttributeValidationResult> ValidateProperty<T>(IEnumerable<MyValidationAttribute> attributes, T value)
        {
            List<AttributeValidationResult> attributeValidationResults = new List<AttributeValidationResult>();

            foreach (MyValidationAttribute attribute in attributes)
            {
                attributeValidationResults.Add(ValidateAttribute(attribute, value));
            }

            return attributeValidationResults;
        }

        private AttributeValidationResult ValidateAttribute<T>(MyValidationAttribute attribute, T value)
        {
            return new AttributeValidationResult
            {
                Ok = attribute.IsValid(value),
                Error = attribute.ErrorMessage
            };
        }

    }
}
