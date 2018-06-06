using IntegralsSolver.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace IntegralsSolver
{
    public class FormulaProvider : IFormulaProvider
    {
        private readonly string _formulasPath;
        private readonly List<Formula> _formulas;

        public FormulaProvider(string formulasPath)
        {
            _formulasPath = formulasPath;
            _formulas = JsonConvert.DeserializeObject<List<Formula>>(File.ReadAllText(_formulasPath));
        }

        public List<Formula> GetFormulas() => _formulas;
    }
}
