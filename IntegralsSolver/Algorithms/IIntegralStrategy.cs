using System.Threading.Tasks;
using Math = org.mariuszgromada.math.mxparser;

namespace IntegralsSolver.Algorithms
{
    public interface IIntegralStrategy
    {
        Task<double> Calculate(double from, double to, int n, Math.Expression func);
    }
}
