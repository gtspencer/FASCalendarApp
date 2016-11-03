using System;
using System.IO;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;

namespace WFCalendarApp {

    /// <summary>
    /// The main form of the application. From here the user selects which
    /// directory the backup will go in, and the start and end dates, and
    /// presses the Go button to get the data.
    /// </summary>
    public partial class MainForm : Form {

        //private string directory;
        
        private DateTime start;
        private DateTime end;
        private string jobNumber;

        private const string DIALOG_OPENING = "FAS Google Calendar Tool";
        private const string TIMESTAMP_FORMAT = "yyyy-MM-dd@HHmm";
        private const string FOLDER_EXISTS = "A folder with that timestamp already exists. Would you like to overwrite it?";
        private const string FOLDER_EXISTS_CAPTION = "Folder already exists.";
        private const string CONNECT_FAILED = "Could not connect to Google. Please check that you are connected to the internet.";

        /// <summary>
        /// Initializes the form.
        /// </summary>
        public MainForm() {
            InitializeComponent();

            //directoryDisplay.Text = Properties.Settings.Default.lastSelectedDirectory;
            startDatePicker.Value = Properties.Settings.Default.lastDateSelectedStart;
            endDatePicker.Value = Properties.Settings.Default.lastDateSelectedEnd;

            Alert(DIALOG_OPENING);
            Alert("Last retrieval: " + Properties.Settings.Default.lastRetrieval.ToString());
        }

        /// <summary>
        /// Main functionality of the app. Retrieves data from Google Calendar
        /// and first writes it to an Excel spreadsheet, then creates a
        /// timeline chart.
        /// </summary>
        /// <param name="sender">The <code>goButton</code></param>
        /// <param name="e">Event arguments</param>
        private void goButton_Click(object sender, EventArgs e) {

            progressBar1.Increment(10);
            var errorMessage = CheckInput();
            if (errorMessage != string.Empty) {
                // errorMessage will already have a newline.
                dialogBox.AppendText(errorMessage);
                Alert("Aborted");
                return;
            }
            progressBar1.Increment(20);

            // Get Google Calendar data
            Dictionary<Employee, IList<GCEvent>> data;
            Alert("Retrieving data from Google Calendar...");
            progressBar1.Increment(30);
            try
            {
                data = GoogleComm.RetrieveData(start, end);
            } catch (HttpRequestException) {
                MessageBox.Show(CONNECT_FAILED);
                return;
            }
            var now = DateTime.Now;
            progressBar1.Increment(50);

            //// Create the new folder with current timestamp
            //var now = DateTime.Now;
            //var folderName = $"Calendar Backup {now.ToString(TIMESTAMP_FORMAT)}";
            //var tentativePath = Path.Combine(directory, folderName);
            //var path = SafePath(tentativePath, Directory.Exists);
            //Directory.CreateDirectory(path);

            //// Write the data to Excel
            //var xlWriter = new ExcelWriter();
            //if (xlWriter.CreateWorkbook(path)) {
            //    Alert("Writing data to workbook...");
            //    xlWriter.WriteWorkbook(data, start, end, jobNumber);
            //} else {
            //    Alert("Workbook not created");
            //}

            // Create the chart: save as image and open in new window
            Alert("Creating chart...");
            var timePeriods = EventUtils.TimePeriodDict(data, start, end);
            var cWriter = new ChartWriter();
            var chart = cWriter.WriteChart(timePeriods, start, end);
            //cWriter.CreateImageFile(path);
            (new ChartForm(chart)).Show();
            progressBar1.Increment(70);

            if (jobNumber != "" && jobNumber != null)
            {
                // Create the chart: save as image and open in new window
                Alert("Creating chart...");
                timePeriods = EventUtils.JobFilteredDict(data, start, end, jobNumber);
                cWriter = new ChartWriter();
                chart = cWriter.WriteChart(timePeriods, start, end);
                //cWriter.CreateImageFile(path);
                (new ChartForm(chart)).Show();
            }
            progressBar1.Increment(90);

            // Also bring up a quick search window.
            (new EmployeeDataForm(data, timePeriods)).Show();
            
            // Update settings file with most recent info
            Alert("Last retrival: " + now);
            UpdateHistory(now);
            progressBar1.Increment(100);

        }

        /// <summary>
        /// Displays a folder browser dialog that allows the user to select a directory.
        /// </summary>
        /// <param name="sender">The <code>chooseDirectoryButton</code></param>
        /// <param name="e">Event arguments</param>
        // private void chooseDirectoryButton_Click(object sender, EventArgs e) {
        //   var folderDialog = new FolderBrowserDialog();
        // if (folderDialog.ShowDialog() == DialogResult.OK) {
        //   directory = folderDialog.SelectedPath;
        // directoryDisplay.Text = directory;
        //     }
        //}

        /// <summary>
        /// Creates a path based on the desired name with the appropriate
        /// offset appended to the end. For example, if "Desktop\Docs" is
        /// passsed in, this will return "Desktop\Docs_1". In general, If the
        /// folders "Docs_2", "Docs_3", ... "Docs_n" exist, the result will be
        /// "Docs_{n + 1}".
        /// </summary>
        /// <param name="path">The path to use</param>
        /// <param name="exists">Function to determine if the path exists</param>
        /// <returns>A safe version of the path with the correct number/returns>
        private string SafePath(string path, Func<string, bool> exists) {
            if (!exists(path)) {
                return path;
            }  

            path = $"{path}_2";

            var i = 2;
            while (exists(path)) {
                path = $"{path.Substring(0, path.Length - 2)}_{i++}";
            }

            return path;
        }

        /// <summary>
        /// Clears the dialog, leaving only the opening text at the top.
        /// </summary>
        /// <param name="sender">The <code>clearButton</code></param>
        /// <param name="e">Event arguments</param>
        private void clearButton_Click(object sender, EventArgs e) {
            dialogBox.Clear();
            Alert(DIALOG_OPENING);
        }

        /// <summary>
        /// If the user checks the box, the date pickers are disabled and the
        /// range is set to today. If it is unchecked the date pickers are
        /// enabled again and the range is set according to their dates.
        /// </summary>
        /// <param name="sender">The <code>todayCheckBox</code></param>
        /// <param name="e">Event arguments</param>
        private void todayCheckBox_CheckedChanged(object sender, EventArgs e) {
            startDatePicker.Enabled = !todayCheckBox.Checked;
            endDatePicker.Enabled = !todayCheckBox.Checked;

            if (todayCheckBox.Checked) {
                start = DateTime.Today;
                end = DateTime.Today.AddDays(1.0);
            } else {
                start = startDatePicker.Value;
                end = endDatePicker.Value.AddDays(1);
            }
        }

        /// <summary>
        /// Sets the start date when one is selected from the <code>startDatePicker</code>.
        /// </summary>
        /// <param name="sender">The <code>startDatePicker</code></param>
        /// <param name="e">Event arguments</param>
        private void startDatePicker_ValueChanged(object sender, EventArgs e) {
            start = startDatePicker.Value;
        }

        /// <summary>
        /// Sets the end date when one is selected from the <code>endDatePicker</code>.
        /// </summary>
        /// <param name="sender">The <code>endDatePicker</code></param>
        /// <param name="e">Event arguments</param>
        private void endDatePicker_ValueChanged(object sender, EventArgs e) {
            end = endDatePicker.Value.AddDays(1);
        }

        /// <summary>
        /// Sets the directory when the user selects one from the file browser
        /// dialog (which will change the text in this field).
        /// </summary>
        /// <param name="sender">The <code>directoryDisplay</code></param>
        /// <param name="e">Event arguments</param>
        /// only needed for save location of spreadsheets, comment out otherwise.
        private void directoryDisplay_TextChanged(object sender, EventArgs e) {
        //    directory = directoryDisplay.Text;
        }

        /// <summary>
        /// Writes a message to the dialog, follwed by a new line.
        /// </summary>
        /// <param name="message">The message to write</param>
        private void Alert(string message) {
            dialogBox.AppendText(message + Environment.NewLine);
        }

        /// <summary>
        /// Checks the user's input to make sure it is all there. Will return
        /// the empty string if everything is OK.
        /// </summary>
        /// <returns>An error message based on user input.</returns>
        private string CheckInput() {
            string errorMessage = string.Empty;

            // If you add more input options, put the checks here. Follow the
            // same format of checking and then appending the appropriate
            // error message.

            //if (string.IsNullOrEmpty(directory)) {
            //    errorMessage += "Please choose a directory!" + Environment.NewLine;
            //}

            if (start > end) {
                errorMessage += "The start date must come before the end date!" + Environment.NewLine;
            }

            return errorMessage;
        }

        /// <summary>
        /// Updates the Settings file to keep track of all data that needs to be
        /// persisted between uses. Note that the current time is passed in
        /// rather than calculated here so that the last retrieval time can be
        /// exactly the same as the time stamp in the folder name, if we ever
        /// include seconds in the folder name.
        /// </summary>
        /// <param name="currentTime">The time of the last retrieval</param>
        private void UpdateHistory(DateTime currentTime) {
            Properties.Settings.Default.lastRetrieval = currentTime;
            //Properties.Settings.Default.lastSelectedDirectory = directory;
            Properties.Settings.Default.lastDateSelectedStart = start;
            Properties.Settings.Default.lastDateSelectedEnd = end;
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// Sets the directory when the user selects one from the file browser
        /// dialog (which will change the text in this field).
        /// </summary>
        /// <param name="sender">The <code>directoryDisplay</code></param>
        /// <param name="e">Event arguments</param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            jobNumber = textBox1.Text;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

    }
}
