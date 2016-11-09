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
        private Dictionary<Employee, List<TimePeriod>> timePeriodsDict;
        private DateTime start;
        private DateTime end;
        private bool prefabSearch;

        private List<String> currentGroup;
        private String currentGroupName;
        private List<List<String>> archivedGroup;

        private Dictionary<Employee, List<TimePeriod>> selectedGroup;
        private List<String> data;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public GroupSearch(Dictionary<Employee, List<TimePeriod>> timePeriodsDict, DateTime start,
                DateTime end)
        {
            InitializeComponent();

            this.timePeriodsDict = timePeriodsDict;
            this.start = start;
            this.end = end;
            currentGroup = new List<String>();
            data = new List<String>();
            archivedGroup = Properties.Settings.Default.savedGroup;
            selectedGroup = new Dictionary<Employee, List<TimePeriod>>();

            mainView();
            
            dateText.Text = "Select individual employees or select a saved group" + Environment.NewLine +  start + "  to  " + end;
            textBox3.Text = "Enter Group Name";

            foreach (KeyValuePair<Employee, List<TimePeriod>> temp in timePeriodsDict)
            {
                data.Add(temp.Key.Name);
            }

            foreach (List<String> list in archivedGroup)
            {
                String name = list[0];
                comboBox1.Items.Add(name);
            }

            checkedListBox.DataSource = data;
            comboBox1.Items.Add("(none)");
        }


        //--------------------------------------------------------------------Buttons go here------------------------------------------------------------------------------

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
                selectedGroup.Clear();
                title = currentGroupName + " Group Calendar -- from " + start + " to " + end;
                foreach (List<String> group in archivedGroup)
                {
                    if (group[0].Equals(currentGroupName))
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
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Oops! Something went wrong with the group." + Environment.NewLine + "Please try recreate the group and try again.");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            //returns everyone else 
            {
                selectedGroup.Clear();
                title = "Group Calendar -- from " + start + " to " + end;
                foreach (String emp in checkedListBox.CheckedItems)
                {

                    foreach (KeyValuePair<Employee, List<TimePeriod>> tempEmp in timePeriodsDict)
                    {
                        if (tempEmp.Key.Name.Equals(emp))
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

        private void saveGroupButton_Click(object sender, EventArgs e)
        {
            if (checkedListBox.CheckedItems.Count <= 0)
            {
                MessageBox.Show("Please select employees to include in your new group!");
                return;
            }
            else if (currentGroupName == null || groupName.Text == "")
            {
                MessageBox.Show("Please name the group you would like to save!");
                return;
            }
            else if (!checkSaveName())
            {
                MessageBox.Show("Please choose a unique name to save!");
                return;
            }
            else
            {
                currentGroup.Clear();
                currentGroup.Add(currentGroupName); //first index is name of group -- makes it easier to find
                foreach (String emp in checkedListBox.CheckedItems)
                {
                    if (!currentGroup.Contains(emp))
                    {
                        currentGroup.Add(emp);
                    }
                }
                archivedGroup = Properties.Settings.Default.savedGroup;
                comboBox1.Items.Add(currentGroupName);
                archivedGroup.Add(currentGroup);
                UpdateHistory();

                selectedGroup.Clear();
                String title = currentGroupName + " GROUP SAVED -- from " + start + " to " + end;
                foreach (String name in checkedListBox.CheckedItems)
                {
                    foreach (KeyValuePair<Employee, List<TimePeriod>> tempEmp in timePeriodsDict)
                    {
                        if (name.Equals(tempEmp.Key.Name))
                        {
                            try
                            {
                                selectedGroup.Add(tempEmp.Key, tempEmp.Value);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Oops! Something went wrong with the group." + Environment.NewLine + "Please try recreate the group and try again.");
                                return;
                            }
                        }
                    }
                }
                //screw this i'm pretty sure it was never meant to work i'm just typing this in here to
                //do something because today i've coded in circles and it's annoying as fuck.  So 
                //here's three lines proving i did something today.
                var cWriter = new ChartWriter();
                var chart = cWriter.WriteChart(selectedGroup, start, end);
                (new ChartForm(chart, title)).Show();
                this.Close();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            currentGroup.Clear();
            editView();
            checkedListBox.ClearSelected();
            currentGroupName = groupName.Text;
            for (int i = 0; i < data.Count; i++)
            {
                checkedListBox.SetItemChecked(i, false);
                foreach (List<String> group in archivedGroup)
                {
                    if (group[0].Equals(currentGroupName))
                    {
                        currentGroup = group;
                    }
                }
                foreach (String tempName in currentGroup)
                {
                    if (data[i].Equals(tempName))
                    {
                        checkedListBox.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            List<List<String>> groupss = Properties.Settings.Default.savedGroup;
            foreach (List<String> group in groupss)
            {
                if (group[0].Equals(currentGroupName))
                {
                    archivedGroup.Remove(group);
                    Properties.Settings.Default.Save();
                    comboBox1.Items.Remove(comboBox1.SelectedItem);
                    comboBox1.SelectedItem = null;
                    MessageBox.Show("Group " + group[0] + " successfully deleted.");
                    mainView();
                    return;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && (String)comboBox1.SelectedItem != "(none)")
            {
                checkedListBox.ClearSelected();
                saveView();
                currentGroupName = comboBox1.SelectedItem.ToString();
            }
            else
            {
                mainView();
            }
        }

        private void groupName_TextChanged(object sender, EventArgs e)
        {
            currentGroupName = groupName.Text;
        }
        private void cancelButton_Click_1(object sender, EventArgs e)
        {
            checkedListBox.ClearSelected();
            saveView();
        }

        private void updateGroupButton_Click_1(object sender, EventArgs e)
        {
            archivedGroup.Remove(currentGroup);
            currentGroup.Clear();
            currentGroup.Add(currentGroupName);
            foreach (String name in checkedListBox.CheckedItems)
            {
                if (!currentGroup.Contains(name))
                {
                    currentGroup.Add(name);
                }
            }
            archivedGroup.Add(currentGroup);
            UpdateHistory();

            selectedGroup.Clear();
            String title = currentGroupName + " GROUP SAVED -- from " + start + " to " + end;
            foreach (String name in checkedListBox.CheckedItems)
            {
                foreach (KeyValuePair<Employee, List<TimePeriod>> tempEmp in timePeriodsDict)
                {
                    if (name.Equals(tempEmp.Key.Name))
                    {
                        try
                        {
                            selectedGroup.Add(tempEmp.Key, tempEmp.Value);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Oops! Something went wrong with the group." + Environment.NewLine + "Please try recreate the group and try again.");
                            return;
                        }
                    }
                }
            }
            var cWriter = new ChartWriter();
            var chart = cWriter.WriteChart(selectedGroup, start, end);
            (new ChartForm(chart, title)).Show();
            this.Close(); 
        }


        //--------------------------------------------------------------------View changes go here--------------------------------------------------------------------------
        //Controls which elements are shown and enabled by grouping certain elements
        private void mainView()
        {
            checkedListBox.Enabled = true;
            groupName.Enabled = true;
            saveGroupButton.Enabled = true;
            prefabSearch = false;
            deleteButton.Hide();
            editButton.Hide();
            saveGroupButton.Show();
            groupName.Show();
            textBox3.Show();
            cancelButton.Hide();
            updateGroupButton.Hide();
            button1.Enabled = true;
            comboBox1.Enabled = true;
            comboBox1.SelectedValue = "(none)";
        }

        private void saveView()
        {
            checkedListBox.Enabled = false;
            groupName.Enabled = false;
            saveGroupButton.Enabled = false;
            prefabSearch = true;
            editButton.Show();
            deleteButton.Show();
            cancelButton.Hide();
            updateGroupButton.Hide();
            comboBox1.Enabled = true;
            button1.Enabled = true;
            saveGroupButton.Show();
            groupName.Show();
            textBox3.Show();
        }

        private void editView()
        {
            checkedListBox.Enabled = true;
            deleteButton.Hide();
            editButton.Hide();
            textBox3.Hide();
            groupName.Hide();
            saveGroupButton.Hide();
            updateGroupButton.Show();
            cancelButton.Show();
            button1.Enabled = false;
            comboBox1.Enabled = false;
        }


        //------------------------------------------------------------------------Misc Logic---------------------------------------------------------------------------

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
            if (groupName == null || groupName.Equals(""))
            {
                save = false;
            }
            return save;
        }

        private void UpdateHistory()
        {
            Properties.Settings.Default.savedGroup = archivedGroup;
            Properties.Settings.Default.Save();
        }


        //----------------------------------------------------------Pretty much useless, freaks out if you delete it though---------------------------------------------------
        private void GroupSearch_Load(object sender, EventArgs e)
        {
        }
        private void dateText_TextChanged(object sender, EventArgs e)
        {
        }
        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
