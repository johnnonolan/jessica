using System;

namespace Jessica.Factory
{
    public class DefaultJessicaFactory : IJessicaFactory
    {
        public JessModule CreateInstance(Type moduleType)
        {
            return Activator.CreateInstance(moduleType) as JessModule;
        }
    }
}
