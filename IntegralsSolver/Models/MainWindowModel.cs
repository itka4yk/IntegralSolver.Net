﻿using System.Collections.Generic;
using IntegralsSolver.Algorithms;
using OxyPlot;

namespace IntegralsSolver.Models
{
    public class MainWindowViewModel
    {
        public IList<DataPoint> Points { get; set; } = new List<DataPoint>();
        public List<Formula> Formulas { get; set; }
        public IIntegralStrategy IntegralStrategy = null;
        public string From { get; set; } = "0";
        public string To { get; set; } = "100";
        public string SubdivisionsCount { get; set; } = "10";
        public Formula Formula { get; set; } = null;
        public string Strategy { get; set; } = null;
        public int InvalidateFlag { get; set; } = 0;
    }
}