using System;

namespace Jessica.Factory
{
    public class DefaultJessicaFactory : IJessicaFactory
    {
        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}