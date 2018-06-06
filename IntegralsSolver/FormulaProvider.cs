using IntegralsSolver.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace IntegralsSolver
{
    public class FormulaProvider : IFormulaProvider
    {
        private readonly List<Formula> _formulas;

        public FormulaProvider(string formulasPath)
            => _formulas = JsonConvert.DeserializeObject<List<Formula>>(File.ReadAllText(formulasPath));

        public List<Formula> GetFormulas() => _formulas;
    }
}
