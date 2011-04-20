using System;
using Jessica.Factory;

namespace Jessica.Specs.Fakes.Factory
{
    public class CustomJessicaFactory : IJessicaFactory
    {
        public object CreateInstance(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
