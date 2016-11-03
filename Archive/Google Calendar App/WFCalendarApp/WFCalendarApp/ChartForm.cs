﻿using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WFCalendarApp {

    /// <summary>
    /// Displays the chart created by the application.
    /// </summary>
    public partial class ChartForm : Form {

        /// <summary>
        /// Initializes the window and displays the chart so that it fills the
        /// window.
        /// </summary>
        /// <param name="chart">The chart</param>
        public ChartForm(Chart chart) {
            InitializeComponent();
            Controls.Add(chart);
            chart.Dock = DockStyle.Fill;
        }
    }
}
