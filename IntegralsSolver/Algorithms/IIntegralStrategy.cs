using System;
using Math = org.mariuszgromada.math.mxparser;

namespace IntegralsSolver.Algorithms
{
    public interface IIntegralStrategy
    {
        double Calculate(double from, double to, int n, Math.Expression func);
    }
}
