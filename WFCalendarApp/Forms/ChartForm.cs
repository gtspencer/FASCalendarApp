using System.Windows.Forms;
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
        public ChartForm(Chart chart, string title) {
            InitializeComponent();
            Controls.Add(chart);
            chart.Dock = DockStyle.Fill;
            formTitle.Text = title;
        }

        private void textBox4_TextChanged(object sender, System.EventArgs e)
        {

        }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, System.EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, System.EventArgs e)
        {

        }
        private void ChartForm_Load(object sender, System.EventArgs e)
        {

        }
        private void formTitle_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
