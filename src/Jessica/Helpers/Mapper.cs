using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Jessica.Helpers
{
    public static class Mapper<T>
        where T : class
    {
        private static Dictionary<string, PropertyInfo> PropertyMap;

        static Mapper()
        {
            PropertyMap = typeof(T)
                .GetProperties()
                .ToDictionary(p => p.Name.ToLower(), p => p);
        }

        public static void Map(ExpandoObject source, T destination)
        {
            foreach (var kv in source)
            {
                PropertyInfo prop;

                if (!PropertyMap.TryGetValue(kv.Key.ToLower(), out prop))
                {
                    continue;
                }

                if (kv.Value.GetType() != prop.PropertyType)
                {
                    continue;
                }

                prop.SetValue(destination, kv.Value, null);
            }
        }
    }
}
