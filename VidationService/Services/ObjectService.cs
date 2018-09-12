using System;
using System.Linq;
using System.Reflection;
using ValidationService.ValidationAttributes;
using System.Collections.Generic;
using ValidationService.Contracts;

namespace ValidationService.Services
{
    public class ObjectService : IObjectService
    {
        private IEnumerable<PropertyInfo> GetObjectProperties<T>(T obj) where T : class
        {
            return obj.GetType().GetProperties();
        }

        private IEnumerable<Attribute> GetAttributes(PropertyInfo property)
        {
            return property.GetCustomAttributes();
        }
        
        public IEnumerable<MyValidationAttribute> GetMyValidationAttributes(PropertyInfo property)
        {
            return GetAttributes(property)
                .Where(el => el as MyValidationAttribute != null)
                .Select(el => el as MyValidationAttribute);
        }

        private object GetPropertyValue(PropertyInfo property, object obj)
        {
            return obj.GetType().GetProperty(property.Name).GetValue(obj, null);
        }
        
        public IEnumerable<PropertyInfo> GetPropertiesToValidate<T>(T instance) where T : class
        {
            List<PropertyInfo> requeredProperties = new List<PropertyInfo>();

            foreach (PropertyInfo property in GetObjectProperties(instance))
            {
                List<Attribute> attributes = GetAttributes(property).ToList();

                if (attributes.Any(el => el as MyValidationAttribute != null))
                {
                    requeredProperties.Add(property);
                }
            }

            return requeredProperties;
        }
        
        public ICollection<KeyValuePair<PropertyInfo, object>> GetPropertiesValues(IEnumerable<PropertyInfo> properties, object instance)
        {
            List<KeyValuePair<PropertyInfo, object>> propertyValuePairs = new List<KeyValuePair<PropertyInfo, object>>();

            foreach (PropertyInfo prop in properties)
            {
                propertyValuePairs.Add(new KeyValuePair<PropertyInfo, object>(prop, GetPropertyValue(prop, instance)));
            }

            return propertyValuePairs;
        }
    }
}
