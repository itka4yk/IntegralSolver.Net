using System;
using Math = org.mariuszgromada.math.mxparser;

namespace IntegralsSolver.algorythms
{
    public interface IIntegralStrategy
    {
        double Calculate(double from, double to, int n, Math.Expression func);
    }
}
