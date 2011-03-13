using System;
using Jessica.Factory;

namespace Jessica.Tests.Data
{
    public class CustomJessicaFactory : IJessicaFactory
    {
        public JessModule CreateInstance(Type moduleType)
        {
            throw new NotImplementedException();
        }
    }
}
