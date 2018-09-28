using System;
using System.Linq;
using System.Reflection;
using ValidationService.ValidationAttributes;
using System.Collections.Generic;
using ValidationService.Contracts;

namespace ValidationService.Services
{
    public static class ReflectionExtenctions
    {
        //return all object properties
        private static IEnumerable<PropertyInfo> GetObjectProperties<T>(this T obj) where T : class
        {
            return obj.GetType().GetProperties();
        }

        //return all properties of attribute
        private static IEnumerable<Attribute> GetAttributes(this PropertyInfo property)
        {
            return property.GetCustomAttributes();
        }

        //return all value of the property from object
        private static object GetPropertyValue(this PropertyInfo property, object obj)
        {
            return obj.GetType().GetProperty(property.Name).GetValue(obj, null);
        }

        //return all custom validation attributes applied to property
        public static IEnumerable<ICustomValidationAttribute> GetCustomValidationAttributes(this PropertyInfo property)
        {
            return property.GetAttributes()
                .Where(el => el is ICustomValidationAttribute)
                .Select(el => el as ICustomValidationAttribute);
        }

        //return all properties to which validation attibutes are applied
        public static IEnumerable<PropertyInfo> GetPropertiesToValidate<T>(this T instance) where T : class
        {
            List<PropertyInfo> requeredProperties = new List<PropertyInfo>();

            foreach (PropertyInfo property in instance.GetObjectProperties())
            {
                List<Attribute> attributes = property.GetAttributes().ToList();

                if (attributes.Any(el => el is ICustomValidationAttribute))
                {
                    requeredProperties.Add(property);
                }
            }

            return requeredProperties;
        }

        //return collection of property - property value from object pairs
        public static ICollection<KeyValuePair<PropertyInfo, object>> GetPropertiesValues(this IEnumerable<PropertyInfo> properties, object instance)
        {
            List<KeyValuePair<PropertyInfo, object>> propertyValuePairs = new List<KeyValuePair<PropertyInfo, object>>();

            foreach (PropertyInfo prop in properties)
            {
                propertyValuePairs.Add(new KeyValuePair<PropertyInfo, object>(prop, prop.GetPropertyValue(instance)));
            }

            return propertyValuePairs;
        }
    }
}
