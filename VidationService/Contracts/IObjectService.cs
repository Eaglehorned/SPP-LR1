using ValidationService.ValidationAttributes;
using System.Collections.Generic;
using System.Reflection;

namespace ValidationService.Contracts
{
    public interface IObjectService
    {
        IEnumerable<MyValidationAttribute> GetMyValidationAttributes(PropertyInfo property);

        IEnumerable<PropertyInfo> GetPropertiesToValidate<T>(T obj) where T : class;

        ICollection<KeyValuePair<PropertyInfo, object>> GetPropertiesValues(IEnumerable<PropertyInfo> properties, object instance);
    }
}
