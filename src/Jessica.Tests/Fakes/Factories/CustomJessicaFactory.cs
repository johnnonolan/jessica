using System;
using Jessica.Factories;

namespace Jessica.Tests.Fakes.Factories
{
    public class CustomJessicaFactory : IJessicaFactory
    {
        public object CreateInstance(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
