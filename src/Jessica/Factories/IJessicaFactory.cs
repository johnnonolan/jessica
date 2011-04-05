using System;

namespace Jessica.Factories
{
    public interface IJessicaFactory
    {
        object CreateInstance(Type type);
    }
}
