using System;
using Math = org.mariuszgromada.math.mxparser;

namespace metoda_prostokatow_cs
{
    /// <summary>
    /// Calkowanie numeryczne - metoda prostokatow
    /// </summary>
    class Metoda_prostokatow
    {        /// <summary>
        /// Oblicza calke metoda prostokatow w przedziale od xp do xk z dokladnoscia n dla funkcji fun
        /// </summary>
        /// <param name="xp">poczatek przedzialu calkowania</param>
        /// <param name="xk">koniec przedzialu calkowania</param>
        /// <param name="n">dokladnosc calkowania</param>
        /// <param name="func">funkcja calkowana</param>
        /// <returns></returns>
        private static double calculate(double xp, double xk, int n, Math.Expression func)
        {
            double dx, calka;

            dx = (xk - xp) / (double)n;

            calka = 0;
            for (int i = 1; i <= n; i++)
            {
                calka += func.calculate(xp + i * dx);
            }
            calka *= dx;

            return calka;
        }
    
    }
}