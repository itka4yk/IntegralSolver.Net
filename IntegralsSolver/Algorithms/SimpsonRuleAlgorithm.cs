using IntegralsSolver.Extensions;
using IntegralsSolver.Helpers;
using org.mariuszgromada.math.mxparser;
using System.Threading.Tasks;

namespace IntegralsSolver.Algorithms
{
    [AlgorithmName("Simpson rule")]
    public class SimpsonRuleAlgorithm : IIntegralStrategy
    {
        public async Task<double> Calculate(double from, double to, int n, Expression func)
        {
            var dx = (to - from) / n;
            var sum = func.CalculateWith((from));
            await Task.Run(() => {
                for(var i = 1; i < n; i += 2)
                {
                    sum += 4 * func.CalculateWith(from + i * dx);
                    if (i + 1 >= n) break;
                    sum += 2 * func.CalculateWith(from + (i + 1) * dx);
                }
            });
            sum += func.CalculateWith((from + n * dx));
            return dx / 3 * sum;
        }
    }
}
