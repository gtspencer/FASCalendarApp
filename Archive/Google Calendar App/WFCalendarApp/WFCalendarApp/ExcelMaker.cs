using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Deployment.Application;
using System.Windows.Forms;
using System;

namespace WFCalendarApp {
    static class ExcelWriter {

        public const string TEMPLATE_NAME = "CalendarTemplate.xltx";
        const string TEMPLATE_PATH = "O:\\Richard Bethune\\Calendar\\CalendarTemplate.xltx";
        const string FILE_ALREADY_EXISTS_WARNING = "An excel file with this name already exists.\nWould you like to overwrite the file?";
        const int EXCEL_OFFSET = 3;

        public static void WriteData(Excel.Workbook workbook) {
            Excel.Worksheet allDataSheet = null;
            Excel.Worksheet OOOSheet = null;
            Excel.Worksheet noWorkSheet = null;

            allDataSheet = workbook.Sheets[1];
            OOOSheet = workbook.Sheets[2];
            noWorkSheet = workbook.Sheets[3];

            List<CalendarEvent> data = Comm.RetrieveData();

            WriteSheet(allDataSheet, data);
            WriteSheet(OOOSheet, data.Where(e => e.Summary.Contains("OOO")).ToList());
            WriteSheet(noWorkSheet, data.Where(e => e.Summary.Contains("No Events:")).ToList());

            allDataSheet.Activate();
            workbook.Save();

            ReleaseObjects(
                allDataSheet,
                OOOSheet,
                noWorkSheet
            );
        }

        /// <summary>
        /// Writes the data from the given list to the given excel sheet, one event per row.
        /// </summary>
        /// <param name="sheet">The sheet to write to</param>
        /// <param name="data">The data to write to the sheet (list of events)</param>
        private static void WriteSheet(Excel.Worksheet sheet, List<CalendarEvent> data) {
            for (int i = 0; i < data.Count; i++) {
                sheet.Cells[i + EXCEL_OFFSET, 1].Value = data[i].PrimaryEmail;
                sheet.Cells[i + EXCEL_OFFSET, 2].Value = data[i].FullName;
                sheet.Cells[i + EXCEL_OFFSET, 3].Value = data[i].Summary;
                sheet.Cells[i + EXCEL_OFFSET, 4].Value = data[i].Date.ToString();
            }

            sheet.Columns.AutoFit();
        }

        public static void ReleaseObjects(params object[] objects) {
            foreach (var o in objects) {
                if (o != null) {
                    Marshal.ReleaseComObject(o);
                }
            }
        }

        public static bool CreateWorkbook(string path, string fileName) {
            Excel.Application excelApp = null;
            Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Sheets sheets = null;

            try {
                excelApp = new Excel.Application();
                workbooks = excelApp.Workbooks;
                var fullFilePath = path + "\\" + fileName + ".xls";

                if (File.Exists(fullFilePath) == true) {
                    if (MessageBox.Show(FILE_ALREADY_EXISTS_WARNING, "", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        workbook = workbooks.Open(fullFilePath);
                    } else {
                        ReleaseObjects(
                            excelApp,
                            workbooks
                        );

                        return false;
                    }
                } else {
                    workbook = workbooks.Add(ApplicationDeployment.CurrentDeployment.DataDirectory + @"\" + TEMPLATE_NAME);
                    workbook.SaveAs(path + "\\" + fileName, Excel.XlFileFormat.xlWorkbookNormal);
                }

                ExcelWriter.WriteData(workbook);
                excelApp.Visible = true;
            } catch (Exception e) {
                MessageBox.Show("Something went wrong while creating the workook! " + e.StackTrace + "  :  " + e.Message);
                return false;
            } finally {
                ExcelWriter.ReleaseObjects(
                    excelApp,
                    workbooks,
                    workbook,
                    sheets
                );
            }

            return true;
        }
    }
}
