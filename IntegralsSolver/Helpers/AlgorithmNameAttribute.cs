using System;

namespace IntegralsSolver.Helpers
{
    public class AlgorithmName : Attribute
    {
        public readonly string Name;

        public AlgorithmName(string algorithmName) => Name = algorithmName;
    }
}
