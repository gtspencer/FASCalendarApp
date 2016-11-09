using System;
using System.IO;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace WFCalendarApp {

    /// <summary>
    /// The main form of the application. From here the user selects which
    /// directory the backup will go in, and the start and end dates, and
    /// presses the Go button to get the data.
    /// </summary>
    public partial class MainForm : Form {

        private string directory;
        private bool looking;
        private bool inOfficeSearch;
        private bool groupSearch;

        private DateTime start;
        private DateTime end;
        private string jobNumber;

        private const string DIALOG_OPENING = "FAS Google Calendar Tool";
        private const string DIALOG_INSTRUCTION = "Select start and end times, and any of the optional filters, then press Go";
        private const string TIMESTAMP_FORMAT = "yyyy-MM-dd@HHmm";
        private const string FOLDER_EXISTS = "A folder with that timestamp already exists. Would you like to overwrite it?";
        private const string FOLDER_EXISTS_CAPTION = "Folder already exists.";
        private const string CONNECT_FAILED = "Could not connect to Google. Please check that you are connected to the internet.";

        /// <summary>
        /// Initializes the form.
        /// </summary>
        public MainForm() {

            InitializeComponent();
            InputTimeButton.Hide();

            startDatePicker.Value = DateTime.Now;
            endDatePicker.Value = DateTime.Now;
            Alert(DIALOG_OPENING);
            Alert(DIALOG_INSTRUCTION);
            Alert("Last retrieval: " + Properties.Settings.Default.lastRetrieval.ToString());
        }

        /// <summary>
        /// Main functionality of the app. Retrieves data from Google Calendar
        /// and first writes it to an Excel spreadsheet, then creates a
        /// timeline chart.
        /// Ok it used to write to an Excel sheet but that functionality is kind
        /// of useless so it's no longer implemented.  Some of that code is still
        /// living in dark corners of this app so tread carefully.
        /// </summary>
        /// <param name="sender">The <code>goButton</code></param>
        /// <param name="e">Event arguments</param>
        private void goButton_Click(object sender, EventArgs e) {
            var errorMessage = CheckInput();
            if (errorMessage != string.Empty) {
                // errorMessage will already have a newline.
                dialogBox.AppendText(errorMessage);
                MessageBox.Show(errorMessage);
                Alert("Aborted");
                return;
            }
            // Get Google Calendar data
            Dictionary<Employee, IList<GCEvent>> data;
            if (groupSearch)
            {
                Alert("Retrieving list of current employees...");
            } else
            {
                Alert("Retrieving data from Google Calendar...");
            }
            try {
                data = GoogleComm.RetrieveData(start, end);
            } catch (HttpRequestException) {
                MessageBox.Show(CONNECT_FAILED);
                return;
            }
            var now = DateTime.Now;
            var timePeriods = EventUtils.TimePeriodDict(data, start, end);
            var cWriter = new ChartWriter();
            String formTitle = start + " to " + end;

            //opens Group Search form if selected, else assume normal process
            if (groupSearch)
            {
                (new GroupSearch(timePeriods, start, end)).Show();
            } else
            {
                // Create the chart: save as image and open in new window
                Alert("Creating Chart...");
                var chart = cWriter.WriteChart(timePeriods, start, end);

                if (jobNumber != "" && jobNumber != null)
                {
                    // Create the chart: save as image and open in new window
                    Alert("Creating chart from job number " + jobNumber + " ...");
                    timePeriods = EventUtils.JobFilteredDict(data, start, end, jobNumber);
                    cWriter = new ChartWriter();
                    chart = cWriter.WriteChart(timePeriods, start, end);
                    //cWriter.CreateImageFile(path);
                    (new JobChartForm(chart, jobNumber)).Show();
                    return;
                }
                if (looking || inOfficeSearch)
                {
                    //Needed seperate methods for differentiating looking and in office
                    if (looking)
                    {
                        Alert("Creating Searching for Work Chart...");
                        timePeriods = EventUtils.LookingDict(data, start, end);
                        formTitle = "Looking For Work -- from " + start + " to " + end;
                    }
                    else if (inOfficeSearch)
                    {
                        Alert("Creating In Office Chart...");
                        timePeriods = EventUtils.InOfficeDict(data, start, end);
                        formTitle = "In Office Calendar -- from " + start + " to " + end;
                    }
                    cWriter = new ChartWriter();
                    chart = cWriter.WriteChart(timePeriods, start, end);
                    (new ChartForm(chart, formTitle)).Show();
                } else
                {
                    (new ChartForm(chart, formTitle)).Show();
                }
                // Also bring up a quick search window.
                (new EmployeeDataForm(data, timePeriods)).Show();
            }
            // Update settings file with most recent info
            Alert("Last retrival: " + now);
            UpdateHistory(now);
        }
        
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
            Properties.Settings.Default.lastSelectedDirectory = directory;
            Properties.Settings.Default.lastDateSelectedStart = start;
            Properties.Settings.Default.lastDateSelectedEnd = end;
            Properties.Settings.Default.Save();
        }
        

        //----------------------------------------------------------Mostly Useless------------------------------------------------------------------------
        private void MainForm_Load(object sender, EventArgs e)
        {
            libLink.Links.Remove(libLink.Links[0]);
            libLink.Links.Add(0, libLink.Text.Length, "www.google.com/calendar");
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (inOfficeCheck.Checked)
            {
                inOfficeSearch = true;
                LookingForWork.Checked = false;
            }
            else
            {
                inOfficeSearch = false;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(e.Link.LinkData.ToString());
            Process.Start(sInfo);
        }
        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
        }
        private void label2_Click(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Sets the directory when the user selects one from the file browser
        /// dialog (which will change the text in this field).
        /// </summary>
        /// <param name="sender">The <code>directoryDisplay</code></param>
        /// <param name="e">Event arguments</param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            jobNumber = jobNumberText.Text;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (LookingForWork.Checked) {
                inOfficeCheck.Checked = false;
                looking = true;
            } else
            {
                looking = false;
            }
        }
        private void checkBox1_CheckedChanged_2(object sender, EventArgs e)
        {
            if (groupBox.Checked)
            {
                groupSearch = true;
                jobNumberText.Enabled = false;
                todayCheckBox.Enabled = false;
                inOfficeCheck.Enabled = false;
                LookingForWork.Enabled = false;
            }
            else if (!groupBox.Checked)
            {
                groupSearch = false;
                jobNumberText.Enabled = true;
                todayCheckBox.Enabled = true;
                inOfficeCheck.Enabled = true;
                LookingForWork.Enabled = true;
            }
            else
            {
                groupSearch = false;
                jobNumberText.Enabled = true;
                todayCheckBox.Enabled = true;
                inOfficeCheck.Enabled = true;
                LookingForWork.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GoogleComm comm = new GoogleComm();
            GoogleComm.RetrieveData(start, end);
            GoogleComm.CreateNewEvent(GoogleComm.service, "Test", "Here", "Test Description", start, end);
        }

        //----------------------------------------------------------Completely Useless--------------------------------------------------------------------
        /**
        /// <summary>
        /// Sets the directory when the user selects one from the file browser
        /// dialog (which will change the text in this field).
        /// </summary>
        /// <param name="sender">The <code>directoryDisplay</code></param>
        /// <param name="e">Event arguments</param>
        private void directoryDisplay_TextChanged(object sender, EventArgs e)
        {
            //directory = directoryDisplay.Text;
        }

        private void directoryCheck_CheckedChanged(object sender, EventArgs e)
        {

            if (directoryCheck.Checked)
            {
                directoryDisplay.Show();
                chooseDirectoryButton.Show();
                directoryDisplay.Enabled = true;
                chooseDirectoryButton.Enabled = true;
            } else
            {
                directoryDisplay.Hide();
                chooseDirectoryButton.Hide();
            }
    
        }

        /// <summary>
        /// Displays a folder browser dialog that allows the user to select a directory.
        /// </summary>
        /// <param name="sender">The <code>chooseDirectoryButton</code></param>
        /// <param name="e">Event arguments</param>
        private void chooseDirectoryButton_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                directory = folderDialog.SelectedPath;
                //directoryDisplay.Text = directory;
            }
        }*/
        private void endDateLabel_Click(object sender, EventArgs e)
        {
        }
        private void startDateLabel_Click(object sender, EventArgs e)
        {
        }
    }
}
