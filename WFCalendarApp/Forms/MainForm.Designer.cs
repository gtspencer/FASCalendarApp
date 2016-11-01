namespace WFCalendarApp {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.goButton = new System.Windows.Forms.Button();
            this.dialogBox = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.todayCheckBox = new System.Windows.Forms.CheckBox();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.jobNumberText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LookingForWork = new System.Windows.Forms.CheckBox();
            this.inOfficeCheck = new System.Windows.Forms.CheckBox();
            this.libLink = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.InputTimeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // goButton
            // 
            this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.goButton.AutoSize = true;
            this.goButton.BackColor = System.Drawing.SystemColors.Control;
            this.goButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goButton.Location = new System.Drawing.Point(542, 9);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(243, 47);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "Go";
            this.toolTip1.SetToolTip(this.goButton, "I hope you find what you\'re looking for...");
            this.goButton.UseVisualStyleBackColor = false;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // dialogBox
            // 
            this.dialogBox.AcceptsReturn = true;
            this.dialogBox.BackColor = System.Drawing.Color.Black;
            this.dialogBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dialogBox.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.dialogBox.Location = new System.Drawing.Point(0, 206);
            this.dialogBox.Multiline = true;
            this.dialogBox.Name = "dialogBox";
            this.dialogBox.ReadOnly = true;
            this.dialogBox.Size = new System.Drawing.Size(797, 220);
            this.dialogBox.TabIndex = 3;
            this.toolTip1.SetToolTip(this.dialogBox, "the mysterious and elusive black box . . .");
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.AutoSize = true;
            this.clearButton.BackColor = System.Drawing.SystemColors.Control;
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(661, 375);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(124, 39);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear Dialog";
            this.toolTip1.SetToolTip(this.clearButton, "Don\'t press this button");
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(145, 11);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(391, 20);
            this.startDatePicker.TabIndex = 8;
            this.toolTip1.SetToolTip(this.startDatePicker, "In the beginning . . .  There was Ross (and Mark was there too)");
            this.startDatePicker.Value = new System.DateTime(2016, 10, 20, 12, 27, 14, 0);
            this.startDatePicker.ValueChanged += new System.EventHandler(this.startDatePicker_ValueChanged);
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(145, 36);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(391, 20);
            this.endDatePicker.TabIndex = 9;
            this.toolTip1.SetToolTip(this.endDatePicker, "The End");
            this.endDatePicker.Value = new System.DateTime(2016, 10, 20, 0, 0, 0, 0);
            this.endDatePicker.ValueChanged += new System.EventHandler(this.endDatePicker_ValueChanged);
            // 
            // todayCheckBox
            // 
            this.todayCheckBox.AutoSize = true;
            this.todayCheckBox.Location = new System.Drawing.Point(145, 86);
            this.todayCheckBox.Name = "todayCheckBox";
            this.todayCheckBox.Size = new System.Drawing.Size(80, 17);
            this.todayCheckBox.TabIndex = 10;
            this.todayCheckBox.Text = "Today Only";
            this.toolTip1.SetToolTip(this.todayCheckBox, "¡Ahora solomente!");
            this.todayCheckBox.UseVisualStyleBackColor = true;
            this.todayCheckBox.CheckedChanged += new System.EventHandler(this.todayCheckBox_CheckedChanged);
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endDateLabel.Location = new System.Drawing.Point(81, 38);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(61, 15);
            this.endDateLabel.TabIndex = 11;
            this.endDateLabel.Text = "End Date:";
            this.endDateLabel.Click += new System.EventHandler(this.endDateLabel_Click);
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startDateLabel.Location = new System.Drawing.Point(78, 14);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(64, 15);
            this.startDateLabel.TabIndex = 12;
            this.startDateLabel.Text = "Start Date:";
            this.startDateLabel.Click += new System.EventHandler(this.startDateLabel_Click);
            // 
            // jobNumberText
            // 
            this.jobNumberText.Location = new System.Drawing.Point(145, 60);
            this.jobNumberText.Name = "jobNumberText";
            this.jobNumberText.Size = new System.Drawing.Size(159, 20);
            this.jobNumberText.TabIndex = 13;
            this.toolTip1.SetToolTip(this.jobNumberText, "Choose a number, but select wisely, as not all numbers are created equal");
            this.jobNumberText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Job Number (Optional):";
            // 
            // LookingForWork
            // 
            this.LookingForWork.AutoSize = true;
            this.LookingForWork.Location = new System.Drawing.Point(145, 130);
            this.LookingForWork.Name = "LookingForWork";
            this.LookingForWork.Size = new System.Drawing.Size(159, 17);
            this.LookingForWork.TabIndex = 15;
            this.LookingForWork.Text = "Looking For Work/No Work";
            this.toolTip1.SetToolTip(this.LookingForWork, resources.GetString("LookingForWork.ToolTip"));
            this.LookingForWork.UseVisualStyleBackColor = true;
            this.LookingForWork.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // inOfficeCheck
            // 
            this.inOfficeCheck.AutoSize = true;
            this.inOfficeCheck.Location = new System.Drawing.Point(145, 108);
            this.inOfficeCheck.Name = "inOfficeCheck";
            this.inOfficeCheck.Size = new System.Drawing.Size(66, 17);
            this.inOfficeCheck.TabIndex = 17;
            this.inOfficeCheck.Text = "In Office";
            this.toolTip1.SetToolTip(this.inOfficeCheck, "For those too lazy to walk around a bit and actually look");
            this.inOfficeCheck.UseVisualStyleBackColor = true;
            this.inOfficeCheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // libLink
            // 
            this.libLink.AutoSize = true;
            this.libLink.Location = new System.Drawing.Point(658, 190);
            this.libLink.Margin = new System.Windows.Forms.Padding(0);
            this.libLink.Name = "libLink";
            this.libLink.Size = new System.Drawing.Size(136, 13);
            this.libLink.TabIndex = 18;
            this.libLink.TabStop = true;
            this.libLink.Text = "Go To My Google Calendar";
            this.toolTip1.SetToolTip(this.libLink, "Opens your calendar in the default browser, unless it\'s internet explorer, in whi" +
        "ch case this is a self destruct button.  Get a real browser");
            this.libLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // groupBox
            // 
            this.groupBox.AutoSize = true;
            this.groupBox.Location = new System.Drawing.Point(145, 152);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(114, 17);
            this.groupBox.TabIndex = 20;
            this.groupBox.Text = "Specific Group . . .";
            this.toolTip1.SetToolTip(this.groupBox, "Will open a window to select employees from");
            this.groupBox.UseVisualStyleBackColor = true;
            this.groupBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_2);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(661, 58);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(124, 25);
            this.cancelButton.TabIndex = 21;
            this.cancelButton.Text = "Cancel";
            this.toolTip1.SetToolTip(this.cancelButton, "The way out");
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // InputTimeButton
            // 
            this.InputTimeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InputTimeButton.AutoSize = true;
            this.InputTimeButton.BackColor = System.Drawing.SystemColors.Control;
            this.InputTimeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputTimeButton.Location = new System.Drawing.Point(145, 173);
            this.InputTimeButton.Name = "InputTimeButton";
            this.InputTimeButton.Size = new System.Drawing.Size(124, 25);
            this.InputTimeButton.TabIndex = 22;
            this.InputTimeButton.Text = "Input Time";
            this.toolTip1.SetToolTip(this.InputTimeButton, "Doesn\'t actually do anything yet... if you\'re seeing this there was a mistsake");
            this.InputTimeButton.UseVisualStyleBackColor = false;
            this.InputTimeButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "Narrow Results to...";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.goButton;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(797, 426);
            this.Controls.Add(this.InputTimeButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.libLink);
            this.Controls.Add(this.inOfficeCheck);
            this.Controls.Add(this.LookingForWork);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.jobNumberText);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.todayCheckBox);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.dialogBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "FAS Calendar App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.TextBox dialogBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.CheckBox todayCheckBox;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.TextBox jobNumberText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox LookingForWork;
        private System.Windows.Forms.CheckBox inOfficeCheck;
        private System.Windows.Forms.LinkLabel libLink;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox groupBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button InputTimeButton;
    }
}
