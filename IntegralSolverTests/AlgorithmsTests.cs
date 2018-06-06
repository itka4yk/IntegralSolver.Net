using IntegralsSolver.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Math = org.mariuszgromada.math.mxparser;

namespace IntegralSolverTests
{
    [TestClass]
    public class AlgorithmsTests
    {
        [DataRow(0, 10, 10, "x^2+x+1", 340)]
        [DataRow(-10, 10, 10, "x^2+x+1", 680)]
        [DataRow(-10, 10, 10, "sin(x)", 1.088042222)]
        [TestMethod]
        public async Task ItShouldFindIntegralByLeftSumMethod(double from, double to, int n, string func, double result)
            => Assert.AreEqual(result, await new LeftSumAlgorithm().Calculate(from, to, n, new Math.Expression(func)), 1);

        [DataRow(0, 10, 10, "x^2+x+1", 450)]
        [DataRow(-10, 10, 10, "x^2+x+1", 720)]
        [DataRow(-10, 10, 10, "sin(x)", -1.088042222)]
        [TestMethod]
        public async Task ItShouldFindIntegralByRightSumMethod(double from, double to, int n, string func, double result)
    => Assert.AreEqual(result, await new RightSumAlgorithm().Calculate(from, to, n, new Math.Expression(func)), 1);

        [DataRow(0, 10, 10, "x^2+x+1", 395)]
        [DataRow(-10, 10, 10, "x^2+x+1", 700)]
        [DataRow(-10, 10, 10, "sin(x)", 0)]
        [TestMethod]
        public async Task ItShouldFindIntegralByTrapezoidMethod(double from, double to, int n, string func, double result)
    => Assert.AreEqual(result, await new TrapezoidAlgorithm().Calculate(from, to, n, new Math.Expression(func)), 1);
    }
}
