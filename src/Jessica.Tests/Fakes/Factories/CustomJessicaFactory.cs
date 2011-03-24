using System;
using Jessica.Factories;

namespace Jessica.Tests.Fakes.Factories
{
    public class CustomJessicaFactory : IJessicaFactory
    {
        public JessModule CreateInstance(Type moduleType)
        {
            throw new NotImplementedException();
        }
    }
}
