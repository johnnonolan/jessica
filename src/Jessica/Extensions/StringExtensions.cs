using System.Collections.Generic;
using System.Web;

namespace Jessica.Extensions
{
    public static class StringExtensions
    {
        public static string With(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string UrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        public static string Join<T>(this string join, IEnumerable<T> collection)
        {
            return string.Join(join, collection);
        }
    }
}
