using System;

namespace IntegralsSolver.Models
{
    public class Formula
    {
        public Guid Id = Guid.NewGuid();
        public string Representation;
        public char[] VariableNames;
    }
}
