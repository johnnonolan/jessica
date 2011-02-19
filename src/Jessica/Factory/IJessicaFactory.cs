using System;

namespace Jessica.Factory
{
    public interface IJessicaFactory
    {
        JessicaModule CreateInstance(Type moduleType);
    }
}
