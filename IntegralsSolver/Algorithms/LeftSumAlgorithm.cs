using Extensions;
using IntegralsSolver.Helpers;
using org.mariuszgromada.math.mxparser;

namespace IntegralsSolver.Algorithms
{
    [AlgorithmName("Left sum Method")]
    public class LeftSumAlgorithm : IIntegralStrategy
    {
        public double Calculate(double from, double to, int n, Expression func)
        {
            var dx = (to - from) / n;

            var sum = 0.0;
            for (int i = 1; i <= n; i++)
                sum += func.CalculateWith(from + (i - 1) * dx);

            sum *= dx;
            return sum;
        }
    }
}