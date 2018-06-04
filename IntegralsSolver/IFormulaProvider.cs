using System.Collections.Generic;
using System.Threading.Tasks;
using IntegralsSolver.models;

namespace IntegralsSolver
{
    public interface IFormulaProvider
    {
        List<Formula> GetFormulas();
    }
}