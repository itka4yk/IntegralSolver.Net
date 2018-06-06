using IntegralsSolver.Algorithms;
using IntegralsSolver.Helpers;
using IntegralsSolver.Models;
using OxyPlot;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

using Math = org.mariuszgromada.math.mxparser;

namespace IntegralsSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _ctx;
        private TypeInfo[] _strategyTypes;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            _ctx = (MainWindowViewModel)DataContext;
            _ctx.Formulas = new FormulaProvider("Formulas.json").GetFormulas();
            FormulasCombobox.ItemsSource = _ctx.Formulas.Select(f => f.Representation);

            _strategyTypes = GetType()
                .Assembly
                .DefinedTypes
                .Where(t => t.ImplementedInterfaces.Any(i => i == typeof(IIntegralStrategy)))
                .ToArray();
            StrategyCombobox.ItemsSource = _strategyTypes.Select(s => s.GetCustomAttribute<AlgorithmName>().Name);
        }

        private void HandleFormulaSelection(object sender, SelectionChangedEventArgs e)
        {
            var cbx = (ComboBox)sender;
            _ctx.Formula = _ctx.Formulas[cbx.SelectedIndex];
            FormulaVariablesStack.Children.Clear();
            _ctx.Formula.VariableNames.ToList().ForEach(v => {
                FormulaVariablesStack.Children.Add(new Label { Content = v });
                FormulaVariablesStack.Children.Add(new TextBox { Text = "1" });
            });
        }

        private void HandleStrategySelection(object sender, SelectionChangedEventArgs e)
        {
            var cbx = (ComboBox)sender;
            var selectedStrategyType = _strategyTypes.FirstOrDefault(s => s.GetCustomAttribute<AlgorithmName>().Name == cbx.SelectedValue.ToString());
            _ctx.IntegralStrategy = (IIntegralStrategy)Activator.CreateInstance(selectedStrategyType);
        }

        private void HandleIntegralSolvingClick(object sender, RoutedEventArgs e)
        {
            if (_ctx.IntegralStrategy == null) return;
            var vars = FormulaVariablesStack.Children.OfType<TextBox>().Select(t => t.Text).ToList();
            var func = new Math.Expression(_ctx.Formula.Representation);

            for (int i = 0; i < vars.Count(); i++)
                func.addConstants(new Math.Constant($"{_ctx.Formula.VariableNames[i]} = {Convert.ToDouble(vars[i])}"));

            var from = Convert.ToInt32(_ctx.From);
            var to = Convert.ToInt32(_ctx.To);
            _ctx.Points.Clear();
            for (var i = from; i <= to; i++)
            {
                func.addArguments(new Math.Argument($"x = {i}"));
                _ctx.Points.Add(new DataPoint(i, func.calculate()));
                func.removeAllArguments();
            }
            FuncPlot.InvalidatePlot(true);

            var result = _ctx.IntegralStrategy.Calculate(from, to, Convert.ToInt32(_ctx.SubdivisionsCount), func);
            ResultLabel.Content = result.ToString();
        }
    }
}
