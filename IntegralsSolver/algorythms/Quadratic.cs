using System;
using Math = org.mariuszgromada.math.mxparser;

namespace IntegralsSolver.algorythms
{
    /// <summary>
    /// Calkowanie numeryczne - metoda prostokatow
    /// </summary>
    public class QuadraticAlgorythm: IIntegralStrategy
    {        /// <summary>
        /// Oblicza calke metoda prostokatow w przedziale od xp do xk z dokladnoscia n dla funkcji fun
        /// </summary>
        /// <param name="from">poczatek przedzialu calkowania</param>
        /// <param name="to">koniec przedzialu calkowania</param>
        /// <param name="n">dokladnosc calkowania</param>
        /// <param name="func">funkcja calkowana</param>
        /// <returns></returns>
        public double Calculate(double from, double to, int n, Math.Expression func)
        {
            var dx = (to - from) / (double)n;

            var calka = 0.0;
            for (int i = 1; i <= n; i++)
            {
                func.addArguments(new Math.Argument($"x = {from + i * dx}"));
                calka += func.calculate();
                func.removeAllArguments();
            }
            calka *= dx;

            return calka;
        }
    
    }
}