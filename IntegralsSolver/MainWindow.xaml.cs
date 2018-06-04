using IntegralsSolver.models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IntegralsSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Formulas = new FormulaProvider("Formulas.json").GetFormulas();
            FormulasCombobox.ItemsSource = Formulas.Select(f => f.Representation);
        }

        public List<Formula> Formulas { get; }

        private void HandleFormulaSelection(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var cbx = (ComboBox)sender;
            var selectedFormula = Formulas[cbx.SelectedIndex];
            FormulaVariablesStack.Children.Clear();
            selectedFormula.VariableNames.ToList().ForEach(v => {
                FormulaVariablesStack.Children.Add(new Label { Content = v });
                FormulaVariablesStack.Children.Add(new TextBox { Text = "1" });
            });
        }
    }
}
