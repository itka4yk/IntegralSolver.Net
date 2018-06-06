using Extensions;
using IntegralsSolver.Helpers;
using org.mariuszgromada.math.mxparser;
using System.Threading;
using System.Threading.Tasks;

namespace IntegralsSolver.Algorithms
{
    [AlgorithmName("Left sum Method")]
    public class LeftSumAlgorithm : IIntegralStrategy
    {
        public async Task<double> Calculate(double from, double to, int n, Expression func)
        {
            var dx = (to - from) / n;

            var sum = 0.0;
            await Task.Run(() =>
            {
                for (int i = 1; i <= n; i++)
                    sum += func.CalculateWith(from + (i - 1) * dx);

            });
            
            sum *= dx;
            return sum;
        }
    }
}