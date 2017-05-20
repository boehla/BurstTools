namespace Lib
{
    partial class DebugForm
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
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lvLogFiles = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtbDebug = new System.Windows.Forms.RichTextBox();
            this.cbRun = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lvLogFiles
            // 
            this.lvLogFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvLogFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName});
            this.lvLogFiles.FullRowSelect = true;
            this.lvLogFiles.Location = new System.Drawing.Point(1, 2);
            this.lvLogFiles.Name = "lvLogFiles";
            this.lvLogFiles.Size = new System.Drawing.Size(189, 435);
            this.lvLogFiles.TabIndex = 1;
            this.lvLogFiles.UseCompatibleStateImageBehavior = false;
            this.lvLogFiles.View = System.Windows.Forms.View.Details;
            this.lvLogFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvLogFiles_ItemSelectionChanged);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 178;
            // 
            // rtbDebug
            // 
            this.rtbDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDebug.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDebug.Location = new System.Drawing.Point(196, 25);
            this.rtbDebug.Name = "rtbDebug";
            this.rtbDebug.ReadOnly = true;
            this.rtbDebug.Size = new System.Drawing.Size(640, 412);
            this.rtbDebug.TabIndex = 2;
            this.rtbDebug.Text = "";
            // 
            // cbRun
            // 
            this.cbRun.AutoSize = true;
            this.cbRun.Checked = true;
            this.cbRun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRun.Location = new System.Drawing.Point(197, 2);
            this.cbRun.Name = "cbRun";
            this.cbRun.Size = new System.Drawing.Size(46, 17);
            this.cbRun.TabIndex = 3;
            this.cbRun.Text = "Run";
            this.cbRun.UseVisualStyleBackColor = true;
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 440);
            this.Controls.Add(this.cbRun);
            this.Controls.Add(this.rtbDebug);
            this.Controls.Add(this.lvLogFiles);
            this.Name = "DebugForm";
            this.Text = "DebugForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugForm_FormClosing);
            this.Load += new System.EventHandler(this.DebugForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ListView lvLogFiles;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.RichTextBox rtbDebug;
        private System.Windows.Forms.CheckBox cbRun;
    }
}