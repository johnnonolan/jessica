using System;
using Jessica.Factory;

namespace Jessica.Tests.Fakes.Factory
{
    public class CustomJessicaFactory : IJessicaFactory
    {
        public object CreateInstance(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
