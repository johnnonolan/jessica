using System;

namespace Jessica.Factories
{
    public interface IJessicaFactory
    {
        JessModule CreateInstance(Type moduleType);
    }
}
