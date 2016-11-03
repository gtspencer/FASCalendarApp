namespace WFCalendarApp
{
    partial class GroupSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupSearch));
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dateText = new System.Windows.Forms.TextBox();
            this.saveGroupButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupName = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkedListBox
            // 
            this.checkedListBox.CheckOnClick = true;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(12, 57);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(356, 319);
            this.checkedListBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 488);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(356, 53);
            this.button1.TabIndex = 2;
            this.button1.Text = "Search Selected Employees";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateText
            // 
            this.dateText.Location = new System.Drawing.Point(12, 10);
            this.dateText.Multiline = true;
            this.dateText.Name = "dateText";
            this.dateText.ReadOnly = true;
            this.dateText.Size = new System.Drawing.Size(356, 39);
            this.dateText.TabIndex = 3;
            this.dateText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dateText.TextChanged += new System.EventHandler(this.dateText_TextChanged);
            // 
            // saveGroupButton
            // 
            this.saveGroupButton.Location = new System.Drawing.Point(242, 408);
            this.saveGroupButton.Name = "saveGroupButton";
            this.saveGroupButton.Size = new System.Drawing.Size(126, 20);
            this.saveGroupButton.TabIndex = 5;
            this.saveGroupButton.Text = "Save Current Selection";
            this.saveGroupButton.UseVisualStyleBackColor = true;
            this.saveGroupButton.Click += new System.EventHandler(this.saveGroupButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 461);
            this.comboBox1.MaxDropDownItems = 17;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(356, 21);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupName
            // 
            this.groupName.Location = new System.Drawing.Point(126, 382);
            this.groupName.Multiline = true;
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(242, 20);
            this.groupName.TabIndex = 9;
            this.groupName.TextChanged += new System.EventHandler(this.groupName_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 382);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(108, 20);
            this.textBox3.TabIndex = 10;
            this.textBox3.Text = "New Group Name";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GroupSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 553);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.groupName);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.saveGroupButton);
            this.Controls.Add(this.dateText);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GroupSearch";
            this.Text = "Group Search";
            this.Load += new System.EventHandler(this.GroupSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox dateText;
        private System.Windows.Forms.Button saveGroupButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox groupName;
        private System.Windows.Forms.TextBox textBox3;
    }
}