using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFCalendarApp
{
    public partial class GroupSearch : Form
    {

        private List<String> coOps = new List<String>();

        private Dictionary<Employee, List<TimePeriod>> selectedGroup;

        private Dictionary<Employee, IList<GCEvent>> eventsDict;
        private Dictionary<Employee, List<TimePeriod>> timePeriodsDict;
        private DateTime start;
        private DateTime end;
        private bool coOpSearch;

        public GroupSearch(Dictionary<Employee, IList<GCEvent>> eventsDict,
        Dictionary<Employee, List<TimePeriod>> timePeriodsDict, DateTime start,
                DateTime end)
        {
            InitializeComponent();
            this.eventsDict = eventsDict;
            this.timePeriodsDict = timePeriodsDict;
            this.start = start;
            this.end = end;
            dateText.Text = start + "  to  " + end;


            //Hard code co ops here
            coOps.Add("Spencer Obsitnik");
            coOps.Add("Lovic Ryals");
            coOps.Add("Oreofe Aderibigbe");
            coOps.Add("Cody Huggins");
            coOps.Add("Nick Robish");
            coOps.Add("Peter Giavotto");

            List<Employee> data = new List<Employee>();
            foreach (KeyValuePair<Employee, List<TimePeriod>> temp in timePeriodsDict)
            {
                data.Add(temp.Key);
            }
            checkedListBox.DataSource = data;
            selectedGroup = new Dictionary<Employee, List<TimePeriod>>();
        }

        //Enter button (sorry for the bad comments, getting a bit lazy but you get the idea)
        private void button1_Click(object sender, EventArgs e)
        {
            String title = "";

            //checks input, cancels if any inputs are errored
            String errorMessage = checkInput();
            if (errorMessage != string.Empty)
            {
                MessageBox.Show(errorMessage);
                return;
            }

            //returns only co ops in coOp list
            if (coOpSearch)
            {
                title = "Co Op Calendar -- from " + start + " to " + end;
                foreach (KeyValuePair<Employee, List<TimePeriod>> tempEmp in timePeriodsDict)
                {
                    foreach (String str in coOps)
                    {
                        if (str.Equals(tempEmp.Key.Name))
                        {
                            //compares iterated employee list from Google to hard coded co op list
                            selectedGroup.Add(tempEmp.Key, tempEmp.Value);
                        }
                    }
                }
            } else
            //returns everyone else
            {
                title = "Group Calendar -- from " + start + " to " + end;
                foreach (Employee emp in checkedListBox.CheckedItems)
                {

                    foreach (KeyValuePair<Employee, List<TimePeriod>> tempEmp in timePeriodsDict)
                    {
                        if (tempEmp.Key == emp)
                        {
                            //compares iterated employee list from Google to selected employees from checked list box
                            selectedGroup.Add(tempEmp.Key, tempEmp.Value);
                        }
                    }
                }
            }
            //write the chart
            var cWriter = new ChartWriter();
            var chart = cWriter.WriteChart(selectedGroup, start, end);
            (new ChartForm(chart, title)).Show();
            this.Close();
        }

        //Add inputs here to check validity of form to avoid errors
        private String checkInput()
        {
            String errorMessage = String.Empty;
            if (start > end)
            {
                errorMessage += "The start date must come before the end date!" + Environment.NewLine;
            }

            if (checkedListBox.CheckedItems.Count == 0 && !coOpSearch)
            {
                errorMessage += "There are no employees selected!" + Environment.NewLine;
            }
            return errorMessage;
        }

        //Called when co op checkbox changed
        //Disables checkbox
        private void coOpOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (coOpOnly.Checked) {
                coOpSearch = true;
                checkedListBox.Enabled = false;
            } else
            {
                coOpSearch = false;
                checkedListBox.Enabled = true;
            }
        }

        //--------------------------------Pretty much useless, freaks out if you delete it though--------------------------------
        private void GroupSearch_Load(object sender, EventArgs e)
        {
        }
        private void dateText_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
