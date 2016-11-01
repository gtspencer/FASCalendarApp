namespace WFCalendarApp {
    partial class EmployeeDataForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeDataForm));
            this.searchBox = new System.Windows.Forms.TextBox();
            this.namesBox = new System.Windows.Forms.ComboBox();
            this.timeSpanInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.eventsGrid = new System.Windows.Forms.DataGridView();
            this.summaryCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkCol = new System.Windows.Forms.DataGridViewLinkColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eventsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(9, 23);
            this.searchBox.Margin = new System.Windows.Forms.Padding(2);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(162, 20);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            // 
            // namesBox
            // 
            this.namesBox.FormattingEnabled = true;
            this.namesBox.Location = new System.Drawing.Point(9, 23);
            this.namesBox.Margin = new System.Windows.Forms.Padding(2);
            this.namesBox.MaxDropDownItems = 10;
            this.namesBox.Name = "namesBox";
            this.namesBox.Size = new System.Drawing.Size(162, 21);
            this.namesBox.TabIndex = 1;
            this.namesBox.SelectionChangeCommitted += new System.EventHandler(this.namesBox_SelectionChangeCommitted);
            // 
            // timeSpanInfo
            // 
            this.timeSpanInfo.Location = new System.Drawing.Point(188, 6);
            this.timeSpanInfo.Margin = new System.Windows.Forms.Padding(2);
            this.timeSpanInfo.Name = "timeSpanInfo";
            this.timeSpanInfo.Size = new System.Drawing.Size(403, 42);
            this.timeSpanInfo.TabIndex = 3;
            this.timeSpanInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.timeSpanInfo_Paint);
            // 
            // eventsGrid
            // 
            this.eventsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.summaryCol,
            this.startCol,
            this.endCol,
            this.linkCol});
            this.eventsGrid.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.eventsGrid.Location = new System.Drawing.Point(9, 54);
            this.eventsGrid.Margin = new System.Windows.Forms.Padding(2);
            this.eventsGrid.Name = "eventsGrid";
            this.eventsGrid.RowTemplate.Height = 24;
            this.eventsGrid.Size = new System.Drawing.Size(952, 306);
            this.eventsGrid.TabIndex = 4;
            this.eventsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.eventsGrid_CellContentClick);
            // 
            // summaryCol
            // 
            this.summaryCol.HeaderText = "Summary";
            this.summaryCol.Name = "summaryCol";
            this.summaryCol.Width = 450;
            // 
            // startCol
            // 
            this.startCol.HeaderText = "Start";
            this.startCol.Name = "startCol";
            this.startCol.Width = 200;
            // 
            // endCol
            // 
            this.endCol.HeaderText = "End";
            this.endCol.Name = "endCol";
            this.endCol.Width = 200;
            // 
            // linkCol
            // 
            this.linkCol.HeaderText = "Link";
            this.linkCol.Name = "linkCol";
            this.linkCol.ReadOnly = true;
            this.linkCol.Text = "Link";
            this.linkCol.Width = 58;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search employees:";
            // 
            // EmployeeDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 370);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.timeSpanInfo);
            this.Controls.Add(this.namesBox);
            this.Controls.Add(this.eventsGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EmployeeDataForm";
            this.Text = "Calendar Data";
            this.Load += new System.EventHandler(this.EmployeeDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eventsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ComboBox namesBox;
        private System.Windows.Forms.FlowLayoutPanel timeSpanInfo;
        private System.Windows.Forms.DataGridView eventsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn summaryCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn startCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn endCol;
        private System.Windows.Forms.DataGridViewLinkColumn linkCol;
        private System.Windows.Forms.Label label1;
    }
}