namespace Jessica.Extensions
{
    public static class EnumExtensions
    {
        public static int AsInt<T>(this T enumeration)
        {
            return (int)(enumeration as object);
        }
    }
}
