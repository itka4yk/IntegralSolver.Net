using IntegralsSolver.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegralsSolver
{
    public class FormulaProvider : IFormulaProvider
    {
        private readonly string _formulasPath;
        private List<Formula> _formulas;

        public FormulaProvider(string formulasPath)
        {
            _formulasPath = formulasPath;
            _formulas = JsonConvert.DeserializeObject<List<Formula>>(File.ReadAllText(_formulasPath));
        }

        public List<Formula> GetFormulas() => _formulas;
    }
}
