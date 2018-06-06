using IntegralsSolver.Models;
using System.Collections.Generic;

namespace IntegralsSolver
{
    public interface IFormulaProvider
    {
        List<Formula> GetFormulas();
    }
}