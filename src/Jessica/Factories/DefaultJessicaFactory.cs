using System;

namespace Jessica.Factories
{
    public class DefaultJessicaFactory : IJessicaFactory
    {
        public JessModule CreateInstance(Type moduleType)
        {
            return Activator.CreateInstance(moduleType) as JessModule;
        }
    }
}
