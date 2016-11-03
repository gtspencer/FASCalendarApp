using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;

namespace WFCalendarApp {

    /// <summary>
    /// Creates a timeline chart for every employee with the data from Google
    /// Calendar. Uses a Stacked Bar chart to display the information.
    /// </summary>
    class ChartWriter {

        private Chart chart;
        private List<KeyValuePair<Employee, List<TimePeriod>>> list;

        private const string FILE_NAME = "chart";
        private const string AREA_NAME = "Time chart";
        private const string START_SERIES_NAME = "start offset series";
        private const string Y_AXIS_FORMAT_DAY_HOUR = "MM/dd hh:mm";
        private const string Y_AXIS_FORMAT_DAY = "MM/dd";

        private const int X_AXIS_FONT_SIZE = 7;
        private const int WIDTH = 1920; // Play around with these values to get the best image.
        private const int HEIGHT = 1080; // It doesn't affect the window.

        private readonly Color BACK_COLOR = Color.FromArgb(230, 230, 230);

        /// <summary>
        /// Creates and formats the chart, and writes the data to it.
        /// </summary>
        /// <param name="data">The data from Google</param>
        /// <param name="start">The start date selected by the user</param>
        /// <param name="end">The end date selected by the user</param>
        /// <returns>The chart</returns>
        public Chart WriteChart(Dictionary<Employee, List<TimePeriod>> data, DateTime start, DateTime end) {
            chart = new Chart();
            chart.ChartAreas.Add(AREA_NAME);
            chart.ChartAreas[AREA_NAME].BackColor = BACK_COLOR;
            chart.BackColor = BACK_COLOR;
            chart.Width = WIDTH;
            chart.Height = HEIGHT;

            // Order must be maintained when making the chart.
            list = data.OrderBy(pair => pair.Key.Name).ToList();

            GenerateFirstSeries(start);
            MakeChart();
            FormatAxes(start, end);

            return chart;
        }

        /// <summary>
        /// Saves the chart as an image.
        /// </summary>
        /// <param name="path">The path to save the image to</param>
        public void CreateImageFile(string path) {
            if (chart != null) {
                chart.SaveImage($@"{path}\{FILE_NAME}.png", ChartImageFormat.Png);
            }
        }

        /// <summary>
        /// Adds the first series to the chart. This needs to be done because
        /// of how the chart works. We are using a Stacked Bar chart and
        /// setting the axis to be a time axis. However, the actual data points
        /// hold doubles, not DateTimes. We need to record the duration of each
        /// event, but we also need to offset everything so that it will begin
        /// at the start date selected by the user. This first series contains
        /// the value of the start date for every employee, to create the right
        /// offset. Then we can directly add the duration of each TimePeriod
        /// and it will appear in the correct place.
        /// </summary>
        /// <param name="start">The start date selected by the user</param>
        /// <param name="list">The list, used to create X-values</param>
        private void GenerateFirstSeries(DateTime start) {
            var startDouble = start.ToOADate();
            var startSeries = chart.Series.Add(START_SERIES_NAME);
            startSeries.ChartType = SeriesChartType.StackedBar;
            startSeries.YValueType = ChartValueType.DateTime;

            foreach (var pair in list) {
                startSeries.Points.AddXY(pair.Key.Name, startDouble);
            }
        }

        /// <summary>
        /// Creates all series that will be displayed on the chart. This is
        /// kind of exploiting how Stacked Bar charts work. There is no good
        /// way to create a timeline for every employee at once using MS
        /// charting tools. This method creates a new series for each index in
        /// every employee's list. Of course the time period at a certain index
        /// will be different for every employee (possibly non-existent), so the
        /// particular point in the series is colored approriately. Then it
        /// magically comes together into the timeline chart.
        /// </summary>
        /// <param name="list">"Maps" each employee to a list of that
        ///     employee's time periods. Used instead of a Dictionary to
        ///     maintain order. We went with a LinkedList because data is only
        ///     ever added to the end, and the whole thing can be iterated over
        ///     nicely.</param>
        private void MakeChart() {
            var flag = true;
            var i = 0;
            while (flag) {
                flag = false;

                var series = chart.Series.Add(i.ToString());
                series.ChartType = SeriesChartType.StackedBar;
                series.YValueType = ChartValueType.DateTime;

                foreach (var pair in list) {
                    var employee = pair.Key;
                    var timePeriods = pair.Value;

                    var duration = 0.0;
                    var c = Color.Transparent;
                    var toolTip = string.Empty;

                    if (timePeriods.Count > i) {
                        flag = true;
                        var t = timePeriods[i];

                        duration = t.Duration.TotalDays;
                        c = EventUtils.DecideColor(t.Type);
                        toolTip = MakeToolTip(employee, t);
                    }

                    var p = new DataPoint();
                    p.SetValueXY(employee.Name, duration);
                    p.Color = c;
                    p.ToolTip = toolTip;
                    series.Points.Add(p);
                }

                i++;
            }
        }

        /// <summary>
        /// Formats the axes of the chart.
        /// </summary>
        /// <param name="start">The start time selected by the user</param>
        /// <param name="end">The end time selected by the user</param>
        private void FormatAxes(DateTime start, DateTime end) {
            FormatXAxis();
            FormatYAxis(start, end);
        }

        /// <summary>
        /// Formats the X-axis of the chart.
        /// </summary>
        private void FormatXAxis() {
            var xAxis = chart.ChartAreas[0].AxisX;
            // Interval = 1.0 so that every employee's name is displayed
            xAxis.Interval = 1.0;
            // Fonst size made smaller so all names fit in one column.
            // Autofit option creates two columns of names.
            xAxis.LabelStyle.Font = new Font(xAxis.LabelStyle.Font.FontFamily, X_AXIS_FONT_SIZE);
        }

        /// <summary>
        /// Formats the Y-axis of the chart to start and end at the right time.
        /// </summary>
        /// <param name="start">The start time selected by the user</param>
        /// <param name="end">The end time selected by the user</param>
        private void FormatYAxis(DateTime start, DateTime end) {
            var yAxis = chart.ChartAreas[0].AxisY;
            yAxis.Minimum = start.ToOADate();
            yAxis.Maximum = end.ToOADate();
            yAxis.Interval = 1.0;
            yAxis.MajorTickMark.Enabled = false;
            yAxis.MinorTickMark.Enabled = true;
            yAxis.MinorTickMark.Interval = 0.5;
            yAxis.LabelStyle.Format = (end - start).Days < 5
                ? Y_AXIS_FORMAT_DAY_HOUR
                : Y_AXIS_FORMAT_DAY;
        }

        /// <summary>
        /// Constructs a tooltip for a data point.
        /// </summary>
        /// <param name="employee">The employee</param>
        /// <param name="t">The <code>TimePeriod</code> the data point represents</param>
        /// <returns>The tooltip</returns>
        private string MakeToolTip(Employee employee, TimePeriod t) {
            var result = $"{employee.Name} - {EventUtils.TypeString(t.Type)}";

            if (t.Type != EventType.OVERNIGHT
                    && t.Type != EventType.WEEKEND) {
                result += $" {EventUtils.GetDurationString(t.Duration)}";
            }

            return result;
        }
    }
}
