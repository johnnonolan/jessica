using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Jessica.Extensions;

namespace Jessica.Helpers
{
    public class Url
    {
        public static string For(string name, object parameters = null)
        {
            if (!Jess.NamedRoutes.ContainsKey(name))
            {
                throw new Exception("Named route '{0}' does not exist".With(name));
            }

            var route = Jess.NamedRoutes[name];
            return "/" + (parameters == null ? route : ReplaceUrlParameters(route, parameters));
        }

        private static string ReplaceUrlParameters(string route, object defaults)
        {
            var properties = defaults.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var unreplaced = new NameValueCollection();

            foreach (var prop in properties)
            {
                var name = prop.Name;
                var value = prop.GetValue(defaults, null).ToString();
                var propertyReplaced = false;

                route = RecursiveParameterReplace(route, name, value, ref propertyReplaced);

                if (!propertyReplaced)
                {
                    unreplaced.Add(name, value);
                }
            }

            return route + (unreplaced.Count > 0 ? BuildQueryString(unreplaced) : string.Empty);
        }

        private static string RecursiveParameterReplace(string route, string name, string value, ref bool propertyReplaced)
        {
            while (route.Contains("{{{0}}}".With(name)))
            {
                route = route.Replace("{{{0}}}".With(name), "{0}".With(value.UrlEncode()));
                propertyReplaced = true;
            }

            return route;
        }

        public static string BuildQueryString(NameValueCollection parameters)
        {
            var pairs = parameters.AllKeys.Select(key => "{0}={1}".With(key.UrlEncode(), parameters[key].UrlEncode()));
            return "?" + "&".Join(pairs);
        }
    }
}
