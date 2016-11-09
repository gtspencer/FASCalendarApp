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
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.updateGroupButton = new System.Windows.Forms.Button();
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
            this.checkedListBox.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 489);
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
            this.saveGroupButton.Location = new System.Drawing.Point(192, 408);
            this.saveGroupButton.Name = "saveGroupButton";
            this.saveGroupButton.Size = new System.Drawing.Size(176, 20);
            this.saveGroupButton.TabIndex = 5;
            this.saveGroupButton.Text = "Save Current Selection";
            this.saveGroupButton.UseVisualStyleBackColor = true;
            this.saveGroupButton.Click += new System.EventHandler(this.saveGroupButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 462);
            this.comboBox1.MaxDropDownItems = 17;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(356, 21);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupName
            // 
            this.groupName.Location = new System.Drawing.Point(192, 382);
            this.groupName.Multiline = true;
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(176, 20);
            this.groupName.TabIndex = 9;
            this.groupName.TextChanged += new System.EventHandler(this.groupName_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 382);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(174, 20);
            this.textBox3.TabIndex = 10;
            this.textBox3.Text = "New Group Name";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(192, 436);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(175, 20);
            this.editButton.TabIndex = 12;
            this.editButton.Text = "Edit Group";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(12, 436);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(175, 20);
            this.deleteButton.TabIndex = 13;
            this.deleteButton.Text = "Delete Group";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 381);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(175, 46);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click_1);
            // 
            // updateGroupButton
            // 
            this.updateGroupButton.Location = new System.Drawing.Point(192, 381);
            this.updateGroupButton.Name = "updateGroupButton";
            this.updateGroupButton.Size = new System.Drawing.Size(175, 46);
            this.updateGroupButton.TabIndex = 16;
            this.updateGroupButton.Text = "Update Group";
            this.updateGroupButton.UseVisualStyleBackColor = true;
            this.updateGroupButton.Click += new System.EventHandler(this.updateGroupButton_Click_1);
            // 
            // GroupSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 554);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.updateGroupButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
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
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button updateGroupButton;
    }
}