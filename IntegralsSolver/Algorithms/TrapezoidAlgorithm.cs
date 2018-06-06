﻿using Extensions;
using IntegralsSolver.Helpers;
using Math = org.mariuszgromada.math.mxparser;

namespace IntegralsSolver.Algorithms
{
    [AlgorithmName("Trapezoid Method")]
    public class TrapezoidAlgorithm : IIntegralStrategy
    {
        public double Calculate(double from, double to, int n, Math.Expression func)
        {
            double area = 0.0;
            double dx = (to - from) / n;
            area += func.CalculateWith(from) / 2;
            area += func.CalculateWith(to) / 2;

            for (var i = 1; i < n; i++)
                area += func.CalculateWith(from + dx * i);

            return dx * area;
        }
    }


}