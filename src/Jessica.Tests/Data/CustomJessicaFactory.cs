using System;
using Jessica.Factory;

namespace Jessica.Tests.Data
{
    public class CustomJessicaFactory : IJessicaFactory
    {
        public JessicaModule CreateInstance(Type moduleType)
        {
            throw new NotImplementedException();
        }
    }
}
