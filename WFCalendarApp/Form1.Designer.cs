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
            this.directoryDisplay = new System.Windows.Forms.TextBox();
            this.chooseDirectoryButton = new System.Windows.Forms.Button();
            this.writeExcelButton = new System.Windows.Forms.Button();
            this.dialogBox = new System.Windows.Forms.TextBox();
            this.fileNameDisplay = new System.Windows.Forms.TextBox();
            this.directoryLabel = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.todayCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // directoryDisplay
            // 
            this.directoryDisplay.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.directoryDisplay.Location = new System.Drawing.Point(216, 40);
            this.directoryDisplay.Name = "directoryDisplay";
            this.directoryDisplay.ReadOnly = true;
            this.directoryDisplay.Size = new System.Drawing.Size(589, 22);
            this.directoryDisplay.TabIndex = 0;
            this.directoryDisplay.TextChanged += new System.EventHandler(this.directoryDisplay_TextChanged);
            // 
            // chooseDirectoryButton
            // 
            this.chooseDirectoryButton.BackColor = System.Drawing.SystemColors.Control;
            this.chooseDirectoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseDirectoryButton.Location = new System.Drawing.Point(8, 32);
            this.chooseDirectoryButton.Name = "chooseDirectoryButton";
            this.chooseDirectoryButton.Size = new System.Drawing.Size(192, 39);
            this.chooseDirectoryButton.TabIndex = 1;
            this.chooseDirectoryButton.Text = "Choose Directory";
            this.chooseDirectoryButton.UseVisualStyleBackColor = false;
            this.chooseDirectoryButton.Click += new System.EventHandler(this.chooseDirectoryButton_Click);
            // 
            // writeExcelButton
            // 
            this.writeExcelButton.BackColor = System.Drawing.SystemColors.Control;
            this.writeExcelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writeExcelButton.Location = new System.Drawing.Point(8, 88);
            this.writeExcelButton.Name = "writeExcelButton";
            this.writeExcelButton.Size = new System.Drawing.Size(192, 40);
            this.writeExcelButton.TabIndex = 2;
            this.writeExcelButton.Text = "Create Excel Workbook";
            this.writeExcelButton.UseVisualStyleBackColor = false;
            this.writeExcelButton.Click += new System.EventHandler(this.writeExcelButton_Click);
            // 
            // dialogBox
            // 
            this.dialogBox.AcceptsReturn = true;
            this.dialogBox.BackColor = System.Drawing.Color.Black;
            this.dialogBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dialogBox.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.dialogBox.Location = new System.Drawing.Point(0, 216);
            this.dialogBox.Multiline = true;
            this.dialogBox.Name = "dialogBox";
            this.dialogBox.ReadOnly = true;
            this.dialogBox.Size = new System.Drawing.Size(805, 322);
            this.dialogBox.TabIndex = 3;
            // 
            // fileNameDisplay
            // 
            this.fileNameDisplay.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fileNameDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileNameDisplay.Location = new System.Drawing.Point(216, 104);
            this.fileNameDisplay.Name = "fileNameDisplay";
            this.fileNameDisplay.Size = new System.Drawing.Size(589, 22);
            this.fileNameDisplay.TabIndex = 4;
            this.fileNameDisplay.TextChanged += new System.EventHandler(this.fileNameDisplay_TextChanged);
            // 
            // directoryLabel
            // 
            this.directoryLabel.AutoSize = true;
            this.directoryLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.directoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directoryLabel.Location = new System.Drawing.Point(216, 16);
            this.directoryLabel.Name = "directoryLabel";
            this.directoryLabel.Size = new System.Drawing.Size(72, 18);
            this.directoryLabel.TabIndex = 5;
            this.directoryLabel.Text = "Directory:";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileNameLabel.Location = new System.Drawing.Point(216, 80);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(76, 18);
            this.fileNameLabel.TabIndex = 6;
            this.fileNameLabel.Text = "File name:";
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.SystemColors.Control;
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(8, 144);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(192, 39);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear Dialog";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(384, 144);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(256, 22);
            this.startDatePicker.TabIndex = 8;
            this.startDatePicker.ValueChanged += new System.EventHandler(this.startDatePicker_ValueChanged);
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(384, 176);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(256, 22);
            this.endDatePicker.TabIndex = 9;
            this.endDatePicker.ValueChanged += new System.EventHandler(this.endDatePicker_ValueChanged);
            // 
            // todayCheckBox
            // 
            this.todayCheckBox.AutoSize = true;
            this.todayCheckBox.Location = new System.Drawing.Point(656, 160);
            this.todayCheckBox.Name = "todayCheckBox";
            this.todayCheckBox.Size = new System.Drawing.Size(103, 21);
            this.todayCheckBox.TabIndex = 10;
            this.todayCheckBox.Text = "Today Only";
            this.todayCheckBox.UseVisualStyleBackColor = true;
            this.todayCheckBox.CheckedChanged += new System.EventHandler(this.todayCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(296, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "End Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(288, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 18);
            this.label2.TabIndex = 12;
            this.label2.Text = "Start Date:";
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(805, 538);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.todayCheckBox);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.directoryLabel);
            this.Controls.Add(this.fileNameDisplay);
            this.Controls.Add(this.writeExcelButton);
            this.Controls.Add(this.chooseDirectoryButton);
            this.Controls.Add(this.directoryDisplay);
            this.Controls.Add(this.dialogBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox directoryDisplay;
        private System.Windows.Forms.Button chooseDirectoryButton;
        private System.Windows.Forms.Button writeExcelButton;
        private System.Windows.Forms.TextBox dialogBox;
        private System.Windows.Forms.TextBox fileNameDisplay;
        private System.Windows.Forms.Label directoryLabel;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.CheckBox todayCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
