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
    public partial class GroupEdit : Form
    {
        private Dictionary<Employee, List<TimePeriod>> timePeriodsDict;

        private List<List<String>> saveGroupData;
        private String groupNameToSave;
        private List<String> allNames;
        private List<String> saveGroup;
        private List<String> notSaveGroup;
        private List<String> displayGroup;

        public GroupEdit(Dictionary<Employee, List<TimePeriod>> timePeriodsDict, String group)
        {
            InitializeComponent();
            allNames = new List<String>();
            saveGroup = new List<String>();
            notSaveGroup = new List<String>();
            displayGroup = new List<String>();
            label1.Text = "Group Editing: " + group;
            this.timePeriodsDict = timePeriodsDict;
            groupNameToSave = group;
            foreach (KeyValuePair<Employee, List<TimePeriod>> employee in timePeriodsDict)
            {
                allNames.Add(employee.Key.Name);
            }

            saveGroupData = Properties.Settings.Default.savedGroup;
            foreach (List<String> tempGroup in saveGroupData)
            {
                if (tempGroup[0].Equals(groupNameToSave))
                {
                    saveGroup = tempGroup;
                }
            }
            displayGroup = saveGroup;
            displayGroup.Remove(saveGroup[0]);
            listBox1.DataSource = displayGroup;
            foreach (String allName in allNames)
            {
                if (!saveGroup.Contains(allName))
                {
                    notSaveGroup.Add(allName);
                }
            }
            listBox2.DataSource = notSaveGroup;
        }

        private void UpdateHistory()
        {
            Properties.Settings.Default.savedGroup = saveGroupData;
            Properties.Settings.Default.Save();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

        }

        private void leftToRight_Click(object sender, EventArgs e)
        {

            notSaveGroup.Add(listBox1.SelectedItem.ToString());
            displayGroup.Remove(listBox1.SelectedItem.ToString());
            listBox1.DataSource = displayGroup;
            listBox2.DataSource = notSaveGroup;
        }

        private void rightToLeft_Click(object sender, EventArgs e)
        {
            displayGroup.Add(listBox1.SelectedItem.ToString());
            notSaveGroup.Remove(listBox1.SelectedItem.ToString());
            listBox1.DataSource = displayGroup;
            listBox2.DataSource = notSaveGroup;
        }

        //--------------------------------Pretty much useless, freaks out if you delete it though--------------------------------
        private void GroupEdit_Load(object sender, EventArgs e)
        {
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.SelectedItem = null;
            leftToRight.Enabled = false;
            rightToLeft.Enabled = true;
            listBox2.SelectedItem = listBox2.SelectedItem;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedItem = null;
            rightToLeft.Enabled = false;
            leftToRight.Enabled = true;
            listBox1.SelectedItem = listBox1.SelectedItem;
        }
    }
}
