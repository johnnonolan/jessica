using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jessica.Helpers
{
    public static class Mapper<T>
        where T : class
    {
        static Dictionary<string, PropertyInfo> _propertyMap;

        static Mapper()
        {
            _propertyMap = typeof(T)
                .GetProperties()
                .ToDictionary(p => p.Name.ToLower(), p => p);
        }

        public static void Map(IEnumerable<KeyValuePair<string, object>> source, T destination)
        {
            foreach (var kv in source)
            {
                PropertyInfo prop;

                if (!_propertyMap.TryGetValue(kv.Key.ToLower(), out prop))
                {
                    continue;
                }

                if (kv.Value.GetType() != prop.PropertyType)
                {
                    try
                    {
                        var converted = Convert.ChangeType(kv.Value, prop.PropertyType);
                        prop.SetValue(destination, converted, null);
                    }
                    catch (Exception)
                    { }
                }
                else
                {
                    prop.SetValue(destination, kv.Value, null);
                }
            }
        }

        public static T Map(IEnumerable<KeyValuePair<string, object>> source)
        {
            var model = Activator.CreateInstance<T>();
            Map(source, model);
            return model;
        }
    }
}
