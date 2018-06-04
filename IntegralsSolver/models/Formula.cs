using System;

namespace IntegralsSolver.models
{
    public class Formula
    {
        public Guid Id = Guid.NewGuid();
        public string Representation;
        public char[] VariableNames;
    }
}
