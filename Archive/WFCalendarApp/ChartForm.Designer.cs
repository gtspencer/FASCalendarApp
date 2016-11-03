namespace WFCalendarApp {
    partial class ChartForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartForm));
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.chooseDirectoryButton = new System.Windows.Forms.Button();
            this.directoryDisplay = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chooseDirectoryButton
            // 
            this.chooseDirectoryButton.BackColor = System.Drawing.SystemColors.Control;
            this.chooseDirectoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseDirectoryButton.Location = new System.Drawing.Point(12, 530);
            this.chooseDirectoryButton.Name = "chooseDirectoryButton";
            this.chooseDirectoryButton.Size = new System.Drawing.Size(110, 36);
            this.chooseDirectoryButton.TabIndex = 2;
            this.chooseDirectoryButton.Text = "Choose Directory";
            this.chooseDirectoryButton.UseVisualStyleBackColor = false;
            this.chooseDirectoryButton.Click += new System.EventHandler(this.chooseDirectoryButton_Click);
            // 
            // directoryDisplay
            // 
            this.directoryDisplay.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.directoryDisplay.Location = new System.Drawing.Point(244, 540);
            this.directoryDisplay.Name = "directoryDisplay";
            this.directoryDisplay.ReadOnly = true;
            this.directoryDisplay.Size = new System.Drawing.Size(891, 20);
            this.directoryDisplay.TabIndex = 3;
            this.directoryDisplay.TextChanged += new System.EventHandler(this.directoryDisplay_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(128, 530);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 36);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 578);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.directoryDisplay);
            this.Controls.Add(this.chooseDirectoryButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ChartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Timeline";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.Button chooseDirectoryButton;
        private System.Windows.Forms.TextBox directoryDisplay;
        private System.Windows.Forms.Button button1;
    }
}