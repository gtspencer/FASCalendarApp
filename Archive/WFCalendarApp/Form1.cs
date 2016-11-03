using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace WFCalendarApp {

    /// <summary>
    /// The main form of the application.
    /// </summary>
    public partial class MainForm : Form {

        private string directory;
        private string fileName;
        private DateTime start;
        private DateTime end;

        const string DIALOG_OPENING = "FAS Google Calendar Tool";

        /// <summary>
        /// Initializes the form.
        /// </summary>
        public MainForm() {
            InitializeComponent();

            directoryDisplay.Text = Properties.Settings.Default.lastSelectedDirectory;
            fileNameDisplay.AppendText(Properties.Settings.Default.lastFileName);
            startDatePicker.Value = Properties.Settings.Default.lastDateSelectedStart;
            endDatePicker.Value = Properties.Settings.Default.lastDateSelectedEnd;

            Alert(DIALOG_OPENING);
            Alert("Last retrieval: " + Properties.Settings.Default.lastRetrieval.ToString());
        }

        /// <summary>
        /// Displays a folder browser dialog that allows the user to select a directory.
        /// </summary>
        /// <param name="sender">The <code>chooseDirectoryButton</code></param>
        /// <param name="e">Event arguments</param>
        private void chooseDirectoryButton_Click(object sender, EventArgs e) {
            var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK) {
                directory = folderDialog.SelectedPath;
                directoryDisplay.Text = directory;
            }
        }

        /// <summary>
        /// Retrives data from Google and writes it to an Excel file. If the file does not already exist a new one is created.
        /// </summary>
        /// <param name="sender">The <code>writeExcelButton_Click</code></param>
        /// <param name="e">Event arguments</param>
        private void writeExcelButton_Click(object sender, EventArgs e) {
            if (!CheckInput()) {
                Alert("Aborted");
                return;
            }

            var xlWriter = new ExcelWriter();
            if (xlWriter.CreateWorkbook(directory, fileName)) {
                Alert("Retrieving data from Google Calendar...");
                var data = GoogleComm.RetrieveData(start, end.AddDays(1.0));
                Alert("Writing data to workbook...");
                xlWriter.WriteWorkbook(data, start, end.AddDays(1.0));
                //ExcelWriter.CreateChart(workbook, data, start, end);
                DateTime currentTime = DateTime.Now;
                Alert("Last retrival: " + currentTime);
                UpdateHistory(currentTime);
            } else {
                Alert("Workbook not created");
            }
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
        /// If the user checks the box, the date pickers are disabled and the range is set to today.
        /// If it is unchecked the date pickers are enabled again and the range is set according to their dates.
        /// </summary>
        /// <param name="sender">The <code>todayCheckBox</code></param>
        /// <param name="e">Event arguments</param>
        private void todayCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (todayCheckBox.Checked) {
                startDatePicker.Enabled = false;
                endDatePicker.Enabled = false;
                start = DateTime.Today;
                end = DateTime.Today.AddDays(1.0);
            } else {
                startDatePicker.Enabled = true;
                endDatePicker.Enabled = true;
                start = startDatePicker.Value;
                end = endDatePicker.Value;
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
            end = endDatePicker.Value;
        }

        /// <summary>
        /// Sets the directory when the user selects one from the file browser dialog (which will change the text in this field).
        /// </summary>
        /// <param name="sender">The <code>directoryDisplay</code></param>
        /// <param name="e">Event arguments</param>
        private void directoryDisplay_TextChanged(object sender, EventArgs e) {
            directory = directoryDisplay.Text;
        }

        /// <summary>
        /// Sets the desired file name when the user changes it.
        /// </summary>
        /// <param name="sender">The <code>fileNameDisplay</code></param>
        /// <param name="e">Event arguments</param>
        private void fileNameDisplay_TextChanged(object sender, EventArgs e) {
            fileName = fileNameDisplay.Text;
        }

        /// <summary>
        /// Writes a message to the dialog, follwed by a new line.
        /// </summary>
        /// <param name="message">The message to write</param>
        private void Alert(string message) {
            dialogBox.AppendText(message + Environment.NewLine);
        }

        /// <summary>
        /// Checks the user's input to make sure it is all there.
        /// </summary>
        /// <returns>True if the input is correct, false otherwise</returns>
        private bool CheckInput() {
            string errorMessage = "";

            if (String.IsNullOrEmpty(directory)) {
                errorMessage += "Please choose a directory!" + Environment.NewLine;
            }

            if (String.IsNullOrEmpty(fileName)) {
                errorMessage += "Please specify a file name!" + Environment.NewLine;
            }

            if (start > end) {
                errorMessage += "The start date must come before the end date!" + Environment.NewLine;
            }

            if (errorMessage.Length > 0) {
                dialogBox.AppendText(errorMessage);
                return false;
            } else {
                return true;
            }
        }

        /// <summary>
        /// Updates the Settings file to keep track of the last time data was retrieved and the last filename and directory used.
        /// </summary>
        /// <param name="currentTime">The time of the last retrieval</param>
        private void UpdateHistory(DateTime currentTime) {
            Properties.Settings.Default.lastRetrieval = currentTime;
            Properties.Settings.Default.lastSelectedDirectory = directory;
            Properties.Settings.Default.lastFileName = fileName;
            Properties.Settings.Default.lastDateSelectedStart = start;
            Properties.Settings.Default.lastDateSelectedEnd = end;
            Properties.Settings.Default.Save();
        }
    }
}
