using System;

namespace Jessica.Factory
{
    public interface IJessicaFactory
    {
        object CreateInstance(Type type);
    }
}