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
            this.coOpOnly = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkedListBox
            // 
            this.checkedListBox.CheckOnClick = true;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(12, 36);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(356, 304);
            this.checkedListBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 369);
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
            this.dateText.Name = "dateText";
            this.dateText.ReadOnly = true;
            this.dateText.Size = new System.Drawing.Size(356, 20);
            this.dateText.TabIndex = 3;
            this.dateText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dateText.TextChanged += new System.EventHandler(this.dateText_TextChanged);
            // 
            // coOpOnly
            // 
            this.coOpOnly.AutoSize = true;
            this.coOpOnly.Location = new System.Drawing.Point(12, 346);
            this.coOpOnly.Name = "coOpOnly";
            this.coOpOnly.Size = new System.Drawing.Size(91, 17);
            this.coOpOnly.TabIndex = 4;
            this.coOpOnly.Text = "Co op Search";
            this.coOpOnly.UseVisualStyleBackColor = true;
            this.coOpOnly.CheckedChanged += new System.EventHandler(this.coOpOnly_CheckedChanged);
            // 
            // GroupSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 434);
            this.Controls.Add(this.coOpOnly);
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
        private System.Windows.Forms.CheckBox coOpOnly;
    }
}