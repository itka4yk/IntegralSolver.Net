using IntegralsSolver.Helpers;
using System.Threading.Tasks;
using IntegralsSolver.Extensions;
using Math = org.mariuszgromada.math.mxparser;

namespace IntegralsSolver.Algorithms
{
    [AlgorithmName("Trapezoid Method")]
    public class TrapezoidAlgorithm: IIntegralStrategy
    {
        public async Task<double> Calculate(double from, double to, int n, Math.Expression func)
        {
            var area = 0.0;
            var dx = (to - from) / n;

            await Task.Run(() =>
            {
                area += func.CalculateWith(from) / 2;
                area += func.CalculateWith(to) / 2;
                for (var i = 1; i < n; i++)
                    area += func.CalculateWith(from + dx * i);
            });

            return dx * area;
        }
    }


}
