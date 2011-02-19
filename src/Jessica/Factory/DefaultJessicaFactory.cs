using System;

namespace Jessica.Factory
{
    public class DefaultJessicaFactory : IJessicaFactory
    {
        public JessicaModule CreateInstance(Type moduleType)
        {
            return Activator.CreateInstance(moduleType) as JessicaModule;
        }
    }
}
