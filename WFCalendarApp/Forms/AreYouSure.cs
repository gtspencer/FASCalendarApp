using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFCalendarApp.Forms
{

    public partial class AreYouSure : Form
    {
        public bool userResponse;
        public AreYouSure()
        {
            InitializeComponent();
            userResponse = true;
            String labelText = "Are you absolutely positively sure you want to include " + Environment.NewLine + "Nick Robish" + Environment.NewLine;
            labelText += "to your list?";
            label1.Text = labelText;
        }

        private void AreYouSure_Load(object sender, EventArgs e)
        {

        }

        private void noButton_Click(object sender, EventArgs e)
        {

        }

        private void yesButton_Click(object sender, EventArgs e)
        {

        }
    }
}
