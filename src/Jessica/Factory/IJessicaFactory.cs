using System;

namespace Jessica.Factory
{
    public interface IJessicaFactory
    {
        JessModule CreateInstance(Type moduleType);
    }
}
