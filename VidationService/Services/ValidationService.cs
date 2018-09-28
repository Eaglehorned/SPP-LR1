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
            IEnumerable<PropertyInfo> properties = instance.GetPropertiesToValidate();

            ICollection<KeyValuePair<PropertyInfo, object>> propertyValuePairs = properties.GetPropertiesValues(instance);

            IEnumerable<AttributeValidationResult> attributeValidationResults = GetAttributesValidationResults(propertyValuePairs);

            return new ValidationResult(
                IsValidationResultOk(attributeValidationResults),
                GetAllErrors(attributeValidationResults)
            );
        }

        //return all validation results of all validation attribtes of all properties
        //метод, который возвращает результаты всех валидаций всех валидационных атрибутов у всех свойств
        private IEnumerable<AttributeValidationResult> GetAttributesValidationResults(IEnumerable<KeyValuePair<PropertyInfo, object>> propertyValuePairs)
        {
            List<AttributeValidationResult> attributeValidationResults = new List<AttributeValidationResult>();

            foreach (var propertyValuePair in propertyValuePairs)
            {
                attributeValidationResults.AddRange(ValidateProperty(propertyValuePair.Key.GetCustomValidationAttributes(), propertyValuePair.Value));
            }

            return attributeValidationResults;
        }

        //return all validation results of all validation attributes of passed property
        //метод, который возвращает резултаты валидации всех валидационных атрибутов переданного свойста
        private IEnumerable<AttributeValidationResult> ValidateProperty<T>(IEnumerable<ICustomValidationAttribute> attributes, T value)
        {
            List<AttributeValidationResult> attributeValidationResults = new List<AttributeValidationResult>();

            foreach (ICustomValidationAttribute attribute in attributes)
            {
                attributeValidationResults.Add(ValidateAttribute(attribute, value));
            }

            return attributeValidationResults;
        }

        //return 
        //метод, который проверяет валидно ли переданное значение в рамках переданного атрибута
        private AttributeValidationResult ValidateAttribute<T>(ICustomValidationAttribute attribute, T value)
        {
            
            AttributeValidationResult result = new AttributeValidationResult(attribute.IsValid(value));

            if (!result.Ok)
            {
                result.Error = attribute.ErrorMessage;
            }

            return result;
        }

        //валиден ли объект
        private bool IsValidationResultOk(IEnumerable<AttributeValidationResult> attributeValidationResults)
        {
            return !attributeValidationResults.Any(el => !el.Ok);
        }

        //возвращает из всех результатов валидаций только ошибки
        private IEnumerable<string> GetAllErrors(IEnumerable<AttributeValidationResult> attributeValidationResults)
        {
            List<string> errors = new List<string>();

            foreach (AttributeValidationResult attr in attributeValidationResults)
            {
                if (!attr.Ok)
                {
                    logger.LogWarn(attr.Error);
                    errors.Add(attr.Error);
                }
            }

            return errors;
        }
    }
}
