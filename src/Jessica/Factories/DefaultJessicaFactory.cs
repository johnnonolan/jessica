using System;

namespace Jessica.Factories
{
    public class DefaultJessicaFactory : IJessicaFactory
    {
        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
