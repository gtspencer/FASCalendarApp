using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
        private bool prefabSearch;
        private List<String> saveGroup;
        private List<List<String>> saveGroupData;
        private String groupNameToSave;
        private String editGroup;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public GroupSearch(Dictionary<Employee, IList<GCEvent>> eventsDict,
        Dictionary<Employee, List<TimePeriod>> timePeriodsDict, DateTime start,
                DateTime end)
        {
            InitializeComponent();
            this.eventsDict = eventsDict;
            this.timePeriodsDict = timePeriodsDict;
            this.start = start;
            this.end = end;
            editButton.Hide();
            deleteButton.Hide();
            dateText.Text = "Select individual employees or select a saved group" + Environment.NewLine +  start + "  to  " + end;
            textBox3.Text = "Enter Group Name";
            saveGroup = new List<String>();

            saveGroupData = Properties.Settings.Default.savedGroup;
            List<Employee> data = new List<Employee>();
            foreach (KeyValuePair<Employee, List<TimePeriod>> temp in timePeriodsDict)
            {
                data.Add(temp.Key);
            }

            foreach (List<String> list in saveGroupData)
            {
                String name = list[0];
                comboBox1.Items.Add(name);
            }
            checkedListBox.DataSource = data;
            comboBox1.Items.Add("(none)");
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

            //returns only group specified from list
            if (prefabSearch)
            {
                title = comboBox1.SelectedItem + " Group Calendar -- from " + start + " to " + end;
                foreach (List<String> group in saveGroupData)
                {
                    if (group[0].Equals(comboBox1.SelectedItem))
                    {
                        foreach (String name in group)
                        {
                            foreach (KeyValuePair<Employee, List<TimePeriod>> tempEmp in timePeriodsDict)
                            {
                                if (name.Equals(tempEmp.Key.Name))
                                {
                                    try
                                    {
                                        selectedGroup.Add(tempEmp.Key, tempEmp.Value);
                                    } catch (Exception ex)
                                    {
                                        MessageBox.Show("Oops! Something went wrong with the group." + Environment.NewLine + "Please try recreate the group and try again.");
                                    }
                                }
                            }
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

            if (checkedListBox.CheckedItems.Count == 0 && !prefabSearch)
            {
                errorMessage += "There are no employees selected!" + Environment.NewLine;
            }
            return errorMessage;
        }

        private bool checkSaveName()
        {
            bool save = true;
            foreach (String str in comboBox1.Items)
            {
                if (str.Equals(groupName.Text))
                {
                    save = false;
                }
            }
            return save;
        }

        private void saveGroupButton_Click(object sender, EventArgs e)
        {
            if (checkedListBox.CheckedItems.Count <= 0)
            {
                MessageBox.Show("Please select employees to include in your new group!");
                return;
            } else if (groupNameToSave == null)
            {
               MessageBox.Show("Please name the group you would like to save!");
                return;
            } else if (!checkSaveName())
            {
                MessageBox.Show("Please choose a unique name to save!");
                return;
            }
            else
            {
                saveGroup.Clear();
                saveGroup.Add(groupNameToSave); //first index is name of group -- makes it easier to find
                foreach (Employee emp in checkedListBox.CheckedItems)
                {
                    if (!saveGroup.Contains(emp.Name))
                    {
                        saveGroup.Add(emp.Name);
                    }
                }
                comboBox1.Items.Add(groupNameToSave);
                saveGroupData.Add(saveGroup);
                UpdateHistory();
                textBox3.Text = "Group Saved!";
                groupName.Text = "";
                timer.Interval = 3000;
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && (String)comboBox1.SelectedItem != "(none)")
            {
                checkedListBox.ClearSelected();
                checkedListBox.Enabled = false;
                groupName.Enabled = false;
                saveGroupButton.Enabled = false;
                prefabSearch = true;
                editButton.Show();
                deleteButton.Show();
            } else
            {
                checkedListBox.Enabled = true;
                groupName.Enabled = true;
                saveGroupButton.Enabled = true;
                prefabSearch = false;
                editButton.Hide();
                deleteButton.Hide();
            }
        }

        private void groupName_TextChanged(object sender, EventArgs e)
        {
            groupNameToSave = groupName.Text;
        }

        private void UpdateHistory()
        {
            Properties.Settings.Default.savedGroup = saveGroupData;
            Properties.Settings.Default.Save();
        }

        void timer_Tick(Object sender, EventArgs e)
        {
            textBox3.Text = "Enter Group Name";
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            String name = comboBox1.SelectedItem.ToString();
            editGroup = name;
            (new GroupEdit(timePeriodsDict, name)).Show();
            comboBox1.SelectedItem = null;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            List<List<String>> groupss = Properties.Settings.Default.savedGroup;
            String name = comboBox1.SelectedItem.ToString();
            foreach (List<String> group in groupss)
            {
                if (group[0].Equals(name))
                {
                    Properties.Settings.Default.savedGroup.Remove(group);
                    Properties.Settings.Default.Save();
                    comboBox1.Items.Remove(comboBox1.SelectedItem);
                    comboBox1.SelectedItem = null;
                    MessageBox.Show("Group " + group[0] + " successfully deleted.");
                    checkedListBox.Enabled = true;
                    groupName.Enabled = true;
                    saveGroupButton.Enabled = true;
                    prefabSearch = false;
                    editButton.Hide();
                    deleteButton.Hide();
                    return;
                }
            }
        }

        //--------------------------------Pretty much useless, freaks out if you delete it though--------------------------------
        private void GroupSearch_Load(object sender, EventArgs e)
        {
        }
        private void dateText_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
