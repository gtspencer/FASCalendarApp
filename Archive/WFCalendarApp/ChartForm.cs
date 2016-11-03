using System.Windows.Forms;
using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace WFCalendarApp {

    /// <summary>
    /// Displays the chart created by the application.
    /// </summary>
    public partial class ChartForm : Form {

        private string directory;
        private Chart chart;
        /// <summary>
        /// Initializes the window and displays the chart so that it fills the
        /// window.
        /// </summary>
        /// <param name="chart">The chart</param>
        public ChartForm(Chart chart) {
            InitializeComponent();
            Controls.Add(chart);
            chart.Dock = DockStyle.Fill;
            this.chart = chart;
        }

        private void directoryDisplay_TextChanged(object sender, System.EventArgs e)
        {
            directory = directoryDisplay.Text;
        }

        private void chooseDirectoryButton_Click(object sender, System.EventArgs e)
        {

            var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                directory = folderDialog.SelectedPath;
                directoryDisplay.Text = directory;
            }
        }

        private void button1_Click_1(object sender, EventArgs e) //save button on Chart page
        {
            DateTime local = DateTime.Now;
            if (chart != null && directory != null)
            {
                chart.SaveImage($@"{directory}\{local.ToString()}_WFC_schedule.png", ChartImageFormat.Png);
            }
        }
    }
}
