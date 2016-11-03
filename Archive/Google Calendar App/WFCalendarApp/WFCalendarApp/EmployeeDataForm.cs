using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace WFCalendarApp {

    public partial class EmployeeDataForm : Form {

        private const string NO_EVENTS = "(No events)";
        private const int LINK_COL = 3;

        private bool autocomplete = true;
        private Dictionary<Employee, IList<GCEvent>> eventsDict;
        private Dictionary<Employee, List<TimePeriod>> timePeriodsDict;
        private List<Employee> employees = Employees.List;

        /// <summary>
        /// Initializes the form. Also takes in the data from Google for each
        /// employee.
        /// </summary>
        /// <param name="eventsDict">The mapping from employees to events</param>
        /// <param name="timePeriodsDict">The mapping from employees to time periods</param>
        public EmployeeDataForm(Dictionary<Employee, IList<GCEvent>> eventsDict,
                Dictionary<Employee, List<TimePeriod>> timePeriodsDict) {
            InitializeComponent();
            this.eventsDict = eventsDict;
            this.timePeriodsDict = timePeriodsDict;
        }

        /// <summary>
        /// Autocompletes employee names whenever the user changes the text in
        /// the search box.
        /// </summary>
        /// <param name="sender">The <code>searchBox</code></param>
        /// <param name="args">The event arguments</param>
        private void searchBox_TextChanged(object sender, EventArgs args) {
            if (!autocomplete) {
                return;
            }

            namesBox.Items.Clear();

            var search = searchBox.Text;
            if (search.Length == 0) {
                return;
            }

            var dummy = new Employee(search, string.Empty);
            var i = employees.BinarySearch(dummy, Comparer<Employee>.Create((a, b) => {
                return StringComparer.OrdinalIgnoreCase.Compare(a.Name, b.Name);
            }));

            // If the item is not found (which is what will happen unless the
            // user types in an exact name) BinarySearch returns the bitwise
            // complement of the index of the next greater item in the list,
            // i.e. the index the missing item would be at if it were in the
            // list.
            if (i < 0) {
                i = ~i;
            }

            while (i < employees.Count && MatchesSearch(employees[i], search)) {
                namesBox.Items.Add(employees[i++]);
            }

            namesBox.DroppedDown = true;
        }

        /// <summary>
        /// Selects the first item in the combobox when the user presses the
        /// down button from the search box. Simulates the ability to scroll
        /// down from the search box into the autocomplete results.
        /// </summary>
        /// <param name="sender">The <code>searchBox</code></param>
        /// <param name="e">Event arguments</param>
        private void searchBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Down && namesBox.Items.Count > 0) {
                namesBox.Focus();
                namesBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Sets when a user selects a name eiter by
        /// clicking on the name or by pressing enter
        /// </summary>
        /// <param name="sender">The <code>searchBox</code></param>
        /// <param name="e">Event arguments</param>
        private void namesBox_SelectionChangeCommitted(object sender, EventArgs e) {
            var employee = (Employee) namesBox.SelectedItem;
            SetSearchBoxText(employee.Name);
            namesBox.DroppedDown = false;
            namesBox.Items.Clear();
            DisplayEvents(eventsDict[employee]);
            DisplayTimeSpans(timePeriodsDict[employee]);
        }

        /// <summary>
        /// Opens a link to a Google Calendar event when the user clicks on it.
        /// </summary>
        /// <param name="sender">The <code>eventsGrid</code></param>
        /// <param name="e">Event arguments</param>
        private void eventsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == LINK_COL) {
                var url = eventsGrid[LINK_COL, e.RowIndex].Value.ToString();
                Process.Start(url);
            }
        }

        /// <summary>
        /// Determines whether or not an employee matches the search entered by
        /// the user.
        /// </summary>
        /// <param name="e">The employee</param>
        /// <param name="search">The search entered by the user</param>
        /// <returns></returns>
        private bool MatchesSearch(Employee e, string search) {
            return e.Name.StartsWith(search, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sets the text of the <code>searchBox</code> while bypassing the
        /// autocomplete functionality. This prevents bugs caused by
        /// autocompleting after a user has already selected someone from the
        /// list. The autocomplete has to be based on the TextChanged event,
        /// otherwise it does not work properly.
        /// </summary>
        /// <param name="text">The text to put in the <code>searchBox</code></param>
        private void SetSearchBoxText(string text) {
            autocomplete = false;
            searchBox.Text = text;
            autocomplete = true;
        }

        /// <summary>
        /// Displays the events in the list.
        /// </summary>
        /// <param name="events">The events to display</param>
        private void DisplayEvents(IList<GCEvent> events) {
            eventsGrid.Rows.Clear();

            if (events.Count > 0) {
                AddEventsToGrid(events);
            } else {
                eventsGrid[0, 0].Value = NO_EVENTS;
            }
        }

        /// <summary>
        /// Adds each event in the list to the grid.
        /// </summary>
        /// <param name="events">The events</param>
        private void AddEventsToGrid(IList<GCEvent> events) {
            foreach (var e in events) {
                var row = eventsGrid.Rows.Add();
                eventsGrid[0, row].Value = e.Summary;
                eventsGrid[1, row].Value = e.Start.ToString();
                eventsGrid[2, row].Value = e.End.ToString();
                eventsGrid[3, row].Value = e.HtmlLink;
            }
        }

        /// <summary>
        /// Shows how many hours out of the given list are OOO, searching, and
        /// no events.
        /// </summary>
        /// <param name="timePeriods">The list of time periods</param>
        private void DisplayTimeSpans(List<TimePeriod> timePeriods) {
            timeSpanInfo.Controls.Clear();

            var timeSearching = EventUtils.GetDurationOfType(timePeriods, EventType.SEARCHING);
            var timeOOO = EventUtils.GetDurationOfType(timePeriods, EventType.OOO);
            var timeNoEvents = EventUtils.GetDurationOfType(timePeriods, EventType.NO_EVENTS);

            if (timeSearching != TimeSpan.Zero) {
                timeSpanInfo.Controls.Add(
                    GenerateRichTextBox(timeSearching, EventType.SEARCHING)
                );
            }

            if (timeOOO != TimeSpan.Zero) {
                timeSpanInfo.Controls.Add(
                    GenerateRichTextBox(timeOOO, EventType.OOO)
                );
            }

            if (timeNoEvents != TimeSpan.Zero) {
                timeSpanInfo.Controls.Add(
                    GenerateRichTextBox(timeNoEvents, EventType.NO_EVENTS)
                );
            }
        }

        /// <summary>
        /// Creates a <code>RichTextBox</code> showing the total time of the
        /// given type of event. Used to show how long someone is OOO,
        /// searching, etc.
        /// </summary>
        /// <param name="span">The <code>TimeSpan</code></param>
        /// <param name="type">The type of event</param>
        /// <returns>The <code>RichTextBox</code></returns>
        private RichTextBox GenerateRichTextBox(TimeSpan span, EventType type) {
            var rtBox = new RichTextBox();

            rtBox.BackColor = MakePale(EventUtils.DecideColor(type));
            rtBox.ScrollBars = RichTextBoxScrollBars.None;
            rtBox.Height = timeSpanInfo.Height;
            rtBox.Width = timeSpanInfo.Width / 3;
            rtBox.Text = $"\n{EventUtils.GetDurationString(span)} {EventUtils.TypeString(type)}";
            rtBox.Font = new Font(rtBox.Font.FontFamily, rtBox.Font.Size + 2);
            rtBox.SelectionAlignment = HorizontalAlignment.Center;            
            rtBox.ReadOnly = true;

            return rtBox;
        }

        /// <summary>
        /// Makes a color paler/lighter
        /// </summary>
        /// <param name="c">The color</param>
        /// <returns>A paler version of the color</returns>
        private Color MakePale(Color c) {
            return Color.FromArgb(
                Math.Min(c.R + 50, 255),
                Math.Min(c.G + 50, 255),
                Math.Min(c.B + 50, 255)
            );
        }
    }
}
