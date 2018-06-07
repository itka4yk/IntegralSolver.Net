using IntegralsSolver.Algorithms;
using IntegralsSolver.Helpers;
using IntegralsSolver.Models;
using OxyPlot;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Label = System.Windows.Controls.Label;
using Math = org.mariuszgromada.math.mxparser;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace IntegralsSolver
{
    public partial class MainWindow
    {
        private readonly MainWindowViewModel _ctx;
        private readonly TypeInfo[] _strategyTypes;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            _ctx = (MainWindowViewModel)DataContext;

            try
            {
                _ctx.Formulas = new FormulaProvider("Formulas.json").GetFormulas();
                FormulasCombobox.ItemsSource = _ctx.Formulas.Select(f => f.Representation);

                _strategyTypes = GetType()
                    .Assembly
                    .DefinedTypes
                    .Where(t => t.ImplementedInterfaces.Any(i => i == typeof(IIntegralStrategy)))
                    .ToArray();
                StrategyCombobox.ItemsSource = _strategyTypes.Select(s => s.GetCustomAttribute<AlgorithmName>().Name);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                    @"Please, copy Formulas.json to exe folder. File must contain array of formulas objects: { representation: '', variableNames: [] }");
                Application.Current.Shutdown();
            }
        }

        #region UserInteractions

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
            if (selectedStrategyType == null) return;
            _ctx.IntegralStrategy = (IIntegralStrategy)Activator.CreateInstance(selectedStrategyType);
        }

        private async void HandleIntegralSolvingClick(object sender, RoutedEventArgs e)
        {
            if (_ctx.IntegralStrategy == null) return;
            Spinner.Visibility = Visibility.Visible;
            var vars = FormulaVariablesStack.Children.OfType<TextBox>().Select(t => t.Text).ToList();
            var func = new Math.Expression(_ctx.Formula.Representation);

            for (var i = 0; i < vars.Count; i++)
                func.addConstants(new Math.Constant($"{_ctx.Formula.VariableNames[i]} = {Convert.ToDouble(vars[i]).ToString(CultureInfo.InvariantCulture)}"));

            var from = Convert.ToInt32(_ctx.From);
            var to = Convert.ToInt32(_ctx.To);

            if (to <= from) MessageBox.Show("From number must be lower then to number!");
            else
            {
                await CalculateIntegral(from, to, func);
            }
            Spinner.Visibility = Visibility.Hidden;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[-0-9]*(?:\.[0-9]*)?$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void HandleHelpButtonClick(object sender, RoutedEventArgs e)
            => MessageBox.Show("For more information about math formulas please visit MxParser documentation");

        #endregion

        #region Helpers

        private async Task CalculateIntegral(double from, double to, Math.Expression func)
        {
            var result = await _ctx.IntegralStrategy.Calculate(from, to, Convert.ToInt32(_ctx.SubdivisionsCount), func);
            await RefreshPlot(func, from, to);
            ResultLabel.Content = result.ToString(CultureInfo.InvariantCulture);
        }

        private async Task RefreshPlot(Math.Expression func, double from, double to)
        {
            _ctx.Points.Clear();
            await Task.Run(() =>
            {
                for (var i = from; i <= to; i++)
                {
                    func.addArguments(new Math.Argument($"x = {i}"));
                    _ctx.Points.Add(new DataPoint(i, func.calculate()));
                    func.removeAllArguments();
                }
            });
            FuncPlot.InvalidatePlot();
        }

        #endregion
    }
}
