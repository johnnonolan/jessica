namespace Jessica.Extensions
{
    public static class StringExtensions
    {
        public static string With(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
