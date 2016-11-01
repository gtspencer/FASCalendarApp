using System.IO;
using System.Collections.Generic;

using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Deployment.Application;
using System.Windows.Forms;
using System;

namespace WFCalendarApp {

    /// <summary>
    /// This class creates and writes the Excel spreadsheet with the data from
    /// Google Calendar.
    /// </summary>
    class ExcelWriter {

        private Excel.Application excelApp;
        private Excel.Workbook workbook;

        private static readonly string[] sheetNames = {
            "All Data",
            "OOO",
            "Searching",
            "No events"
        };

        private const string WRITE_FAIL = "Something went wrong while creating the workbook!";
        private const string TEMPLATE = "CalendarTemplate.xltx";
        private const string FILE_NAME = "spreadsheet";
        private const string OOO_FLAG = "OOO";
        private const string SEARCHING_FLAG = "searching";
        private const string ALL_DATA = "All Data";
        private const string OOO = "OOO";
        private const string JOB = "Jobs";
        private const string SEARCHING = "Searching";
        private const string NO_EVENTS = "No Events";
        private const string CHART_DATA = "ChartData";

        private const int STARTING_OFFSET = 3;
        private const int ALL_DATA_SHEET_INDEX = 0;
        private const int OOO_SHEET_INDEX = 1;
        private const int NO_WORK_SHEET_INDEX = 2;
        private const int NO_EVENTS_SHEET_INDEX = 3;
        private const int DAILY_HOURS = 8;
   
        /// <summary>
        /// Attempts to create the workbook to which the data from Google Calendar will be written.
        /// </summary>
        /// <param name="path">The directory in which to create the workbook</param>
        /// <returns>True if the workbook was created succesfully</returns>
        public bool CreateWorkbook(string path) {
            Excel.Workbooks workbooks = null;
            Excel.Sheets sheets = null;
 
            excelApp = new Excel.Application();
            workbooks = excelApp.Workbooks;
            var file = $@"{path}\{FILE_NAME}";

            // If you are testing with the VS debugger/runner/whatever put the
            // template on your computer somewhere and refer directly to that path.
            // Else if publishing application use below:
            // ApplicationDeployment.CurrentDeployment.DataDirectory + @"\" + TEMPLATE
            workbook = workbooks.Add(@"C:\Users\image\Desktop\CalendarTemplate.xltx");
            workbook.SaveAs(file, Excel.XlFileFormat.xlOpenXMLWorkbook);

            ReleaseObjects(
                workbooks,
                sheets
            );

            return workbook != null;
        }

        /// <summary>
        /// Writes all data to the workbook.
        /// </summary>
        /// <param name="data">The data from Google</param>
        /// <param name="start">The start date selected by the user</param>
        /// <param name="end">The end date selected by the user</param>
        public void WriteWorkbook(Dictionary<Employee, IList<GCEvent>> data, DateTime start, DateTime end, String jobSearch) {
            try {
                WriteData(data, start, end, jobSearch);
                workbook.Save();
                excelApp.WindowState = Excel.XlWindowState.xlMinimized;
                excelApp.Visible = true;
            } catch (Exception e) {
                MessageBox.Show($"{WRITE_FAIL}\n{e.Message}");
            } finally {
                ReleaseObjects(excelApp);
            }
        }

        /// <summary>
        /// Writes the event data to the workbook.
        /// </summary>
        /// <param name="workbook">The workbook</param>
        /// <param name="data">The data from Google Calendar</param>
        private void WriteData(Dictionary<Employee, IList<GCEvent>> data, DateTime start, DateTime end, String jobSearch) {
            Excel.Sheets worksheets = workbook.Worksheets;
            Excel.Worksheet allDataSheet = worksheets[ALL_DATA];
            Excel.Worksheet OOOSheet = worksheets[OOO];
            Excel.Worksheet jobSheet = worksheets[JOB];
            Excel.Worksheet searchingSheet = worksheets[SEARCHING];
            Excel.Worksheet noEventsSheet = worksheets[NO_EVENTS];

            var col = 1;
            foreach (var pair in data) {
                var employee = pair.Key;
                var events = pair.Value;

                for (int i = 1; i <= 3; i++) {
                    WriteEventHeader(worksheets[i], employee, col);
                }

                WriteEventHeader(worksheets[JOB], employee, col); // add headers for job search sheet

                int noEventsCol = (int) Math.Ceiling(col / 2.0);
                WriteNoEventsHeader(noEventsSheet, employee, noEventsCol);

                var allDataRow = STARTING_OFFSET;
                var oooRow = STARTING_OFFSET;
                var jobRow = STARTING_OFFSET;
                var searchingRow = STARTING_OFFSET;
                var noEventsRow = STARTING_OFFSET;

                foreach (var e in events) {
                    WriteEvent(allDataSheet, e, allDataRow++, col);

                    if (e.Summary.Contains(OOO_FLAG)) {
                        WriteEvent(OOOSheet, e, oooRow++, col);
                    } else if (e.Summary.Contains(SEARCHING_FLAG)) {
                        WriteEvent(searchingSheet, e, searchingRow++, col);
                    } else if (e.Summary.Contains(jobSearch) && jobSearch != "")
                    {
                        WriteEvent(jobSheet, e, jobRow++, col);
                    }
                }

                var hoursDict = EventUtils.GetHoursPerDay(events, start, end);
                for (DateTime day = start; day <= end; day = day.AddDays(1)) {
                    double hours;
                    hoursDict.TryGetValue(day, out hours);

                    if (!EventUtils.IsWeekend(day) && hours < DAILY_HOURS) {
                        WriteNoEvents(noEventsSheet, day, DAILY_HOURS - hours, noEventsRow++, noEventsCol);
                    }
                }

                for (int i = 1; i <= 4; i++) {
                    // The reason there are so many ternary operators is because
                    // each employee's section on the no events sheet is 2
                    // columns wide, but on the other 3 sheets it is 4 columns
                    // wide.
                    FormatEmployeeCells(
                        worksheets[i],
                        i < 4 ? col : noEventsCol,
                        i < 4 ? 4 : 2,
                        col % 8 == 1 ? 37 : 0
                    );
                }

                col += 4;
            }

            foreach (Excel.Worksheet sheet in workbook.Worksheets) {
                sheet.Columns.AutoFit();
            }

            ReleaseObjects(
                worksheets,
                allDataSheet,
                OOOSheet,
                searchingSheet,
                noEventsSheet
            );
        }

        /// <summary>
        /// Writes the event header for the given employee. See below:
        /// 
        /// +------------------------------+
        /// |          John Smith          |
        /// +---------+-------+-----+------+
        /// | Summary | Start | End | Link |
        /// +---------+-------+-----+------+
        /// 
        /// This appears on all three events sheets for each employee.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="e"></param>
        /// <param name="col"></param>
        private void WriteEventHeader(Excel.Worksheet sheet, Employee e, int col) {
            sheet.Cells[1, col].Value = e.Name;
            sheet.Cells[2, col].Value = "Summary";
            sheet.Cells[2, col + 1].Value = "Start";
            sheet.Cells[2, col + 2].Value = "End";
            sheet.Cells[2, col + 3].Value = "Link";
        }

        /// <summary>
        /// Writes the no events header for the given employee. See below:
        /// 
        /// +--------------+
        /// |  John Smith  |
        /// +------+-------+
        /// | Date | Hours |
        /// +------+-------+
        /// 
        /// This appears on the No Events sheet for each employee.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="e"></param>
        /// <param name="col"></param>
        private void WriteNoEventsHeader(Excel.Worksheet sheet, Employee e, int col) {
            sheet.Cells[1, col].Value = e.Name;
            sheet.Cells[2, col].Value = "Date";
            sheet.Cells[2, col + 1].Value = "Hours Free";
        }

        /// <summary>
        /// Writes the data of an event to the worksheet.
        /// </summary>
        /// <param name="sheet">The worksheet</param>
        /// <param name="e">The event</param>
        /// <param name="row">The row to write to</param>
        /// <param name="col">The column of the summary (first column of data)</param>
        private void WriteEvent(Excel.Worksheet sheet, GCEvent e, int row, int col) {
            sheet.Cells[row, col].Value = e.Summary;
            sheet.Cells[row, col + 1].Value = e.Start;
            sheet.Cells[row, col + 2].Value = e.End;

            if (e.IsPrivate) {
                sheet.Cells[row, col + 3].Value = "(Private)";
            } else {
                Excel.Range linkCell = sheet.Cells[row, col + 3];
                sheet.Hyperlinks.Add(linkCell, e.HtmlLink, Type.Missing, e.Summary, "Link");
            }
        }

        /// <summary>
        /// Writes the date and time number of hours for which an employee has
        /// no events on their calendar.
        /// </summary>
        /// <param name="sheet">The worksheet</param>
        /// <param name="date">The date</param>
        /// <param name="hours">The number of hours</param>
        /// <param name="row">The row to write to</param>
        /// <param name="col">The column of the date (+1 for hours)</param>
        private void WriteNoEvents(Excel.Worksheet sheet, DateTime date, double hours, int row, int col) {
            sheet.Cells[row, col].Value = date;
            sheet.Cells[row, col + 1].Value = hours;
        }

        /// <summary>
        /// Formats the cells of a particular employee so that they are the
        /// right color and have all borders. Called for each employee on each
        /// sheet.
        /// </summary>
        /// <param name="sheet">The sheet</param>
        /// <param name="col">The column of the employee's name</param>
        /// <param name="width">The width of the data under the name</param>
        /// <param name="colorIndex">The index into the Excel color palette</param>
        private void FormatEmployeeCells(Excel.Worksheet sheet, int col, int width, int colorIndex) {
            Excel.Range cells = sheet.Cells;
            Excel.Range topLeft = cells[1, col];
            Excel.Range bottomLeft = topLeft.End[Excel.XlDirection.xlDown];
            Excel.Range bottomRight = bottomLeft.Offset[0, width - 1];
            Excel.Range range = sheet.Range[topLeft, bottomRight];

            range.Interior.ColorIndex = colorIndex;
            range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            var name = topLeft.Value;
            range.Name = "'" + sheet.Name + "'!" + name.Replace(" ", "_");
        }

        /// <summary>
        /// Returns the range from the given sheet containing all cells with calendar data in them.
        /// </summary>
        /// <param name="sheet">The sheet</param>
        /// <returns>The range of cells with calendar data in them</returns>
        private static Excel.Range DataCells(Excel.Worksheet sheet) {
            Excel.Range topLeft = sheet.Cells[STARTING_OFFSET, 1];
            Excel.Range topRight = topLeft.End[Excel.XlDirection.xlToRight];

            // If there is only one entry in the sheet, Excel will go all the
            // way to the bottom of the worksheet.
            Excel.Range bottomRight = topRight.Offset[1, 0].Value2 == null
                    ? topRight
                    : topRight.End[Excel.XlDirection.xlDown];

            return sheet.Range[topLeft, bottomRight];
        }

        /// <summary>
        /// Clears all cells in the given workbook which contain employee data.
        /// Does not clear formatting, since that would unmerge the top cells.
        /// </summary>
        /// <param name="workbook">The workbook to clear</param>
        private static void ClearWorkbook(Excel.Workbook workbook) {
            foreach (Excel.Worksheet sheet in workbook.Worksheets) {
                Excel.Range cells = sheet.Cells;
                Excel.Range topLeft = cells[2, 1];
                Excel.Range topRight = topLeft.End[Excel.XlDirection.xlToRight];
                Excel.Range bottomRight = cells[1000, topRight.Column];
                Excel.Range data = sheet.Range[topLeft, bottomRight];

                cells.ClearContents();
                data.ClearFormats();
            }
        }

        /// <summary>
        /// Releases the given COM objects to avoid weird side effects after
        /// closing the application.
        /// </summary>
        /// <param name="objects">The objects to release</param>
        private static void ReleaseObjects(params object[] objects) {
            foreach (var o in objects) {
                if (o != null) {
                    Marshal.ReleaseComObject(o);
                }
            }
        }

        /// <summary>
        /// Determines whether the given file is in use.
        /// </summary>
        /// <param name="path">The path ot the file</param>
        /// <returns>Whether or not the file is currently open or otherwise
        ///     being used by the OS</returns>
        private static bool FileInUse(string path) {
            FileStream stream = null;

            try {
                stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);
            } catch (IOException) {
                return true;
            } finally {
                stream?.Close();
            }

            return false;
        }
    }
}
