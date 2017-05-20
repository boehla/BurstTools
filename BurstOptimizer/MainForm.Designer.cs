namespace BurstTools {
    partial class Form1 {
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
            this.bwOptimizer = new System.ComponentModel.BackgroundWorker();
            this.tbInputFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOutputFolder = new System.Windows.Forms.TextBox();
            this.bInputFileSelect = new System.Windows.Forms.Button();
            this.bOutputFolderSelect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDual = new System.Windows.Forms.RadioButton();
            this.rbTypeOrg = new System.Windows.Forms.RadioButton();
            this.pbTotal = new System.Windows.Forms.ProgressBar();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.bStart = new System.Windows.Forms.Button();
            this.bShowDebug = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bwOptimizer
            // 
            this.bwOptimizer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwOptimizer_DoWork);
            this.bwOptimizer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwOptimizer_ProgressChanged);
            this.bwOptimizer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwOptimizer_RunWorkerCompleted);
            // 
            // tbInputFile
            // 
            this.tbInputFile.Location = new System.Drawing.Point(99, 13);
            this.tbInputFile.Name = "tbInputFile";
            this.tbInputFile.Size = new System.Drawing.Size(336, 20);
            this.tbInputFile.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Unoptimized File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output Folder:";
            // 
            // tbOutputFolder
            // 
            this.tbOutputFolder.Location = new System.Drawing.Point(99, 39);
            this.tbOutputFolder.Name = "tbOutputFolder";
            this.tbOutputFolder.Size = new System.Drawing.Size(336, 20);
            this.tbOutputFolder.TabIndex = 0;
            // 
            // bInputFileSelect
            // 
            this.bInputFileSelect.Location = new System.Drawing.Point(441, 13);
            this.bInputFileSelect.Name = "bInputFileSelect";
            this.bInputFileSelect.Size = new System.Drawing.Size(28, 20);
            this.bInputFileSelect.TabIndex = 2;
            this.bInputFileSelect.Text = "...";
            this.bInputFileSelect.UseVisualStyleBackColor = true;
            this.bInputFileSelect.Click += new System.EventHandler(this.bInputFileSelect_Click);
            // 
            // bOutputFolderSelect
            // 
            this.bOutputFolderSelect.Location = new System.Drawing.Point(441, 39);
            this.bOutputFolderSelect.Name = "bOutputFolderSelect";
            this.bOutputFolderSelect.Size = new System.Drawing.Size(28, 20);
            this.bOutputFolderSelect.TabIndex = 2;
            this.bOutputFolderSelect.Text = "...";
            this.bOutputFolderSelect.UseVisualStyleBackColor = true;
            this.bOutputFolderSelect.Click += new System.EventHandler(this.bOutputFolderSelect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bOutputFolderSelect);
            this.groupBox1.Controls.Add(this.tbInputFile);
            this.groupBox1.Controls.Add(this.bInputFileSelect);
            this.groupBox1.Controls.Add(this.tbOutputFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 73);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Files";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbDual);
            this.groupBox2.Controls.Add(this.rbTypeOrg);
            this.groupBox2.Location = new System.Drawing.Point(12, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 72);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type";
            // 
            // rbDual
            // 
            this.rbDual.AutoSize = true;
            this.rbDual.Location = new System.Drawing.Point(9, 44);
            this.rbDual.Name = "rbDual";
            this.rbDual.Size = new System.Drawing.Size(322, 17);
            this.rbDual.TabIndex = 1;
            this.rbDual.Text = "Dual (Read and write at same time, Usefull when using 2 Hdds)";
            this.rbDual.UseVisualStyleBackColor = true;
            // 
            // rbTypeOrg
            // 
            this.rbTypeOrg.AutoSize = true;
            this.rbTypeOrg.Checked = true;
            this.rbTypeOrg.Location = new System.Drawing.Point(9, 20);
            this.rbTypeOrg.Name = "rbTypeOrg";
            this.rbTypeOrg.Size = new System.Drawing.Size(144, 17);
            this.rbTypeOrg.TabIndex = 0;
            this.rbTypeOrg.TabStop = true;
            this.rbTypeOrg.Text = "Original (Read then write)";
            this.rbTypeOrg.UseVisualStyleBackColor = true;
            // 
            // pbTotal
            // 
            this.pbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTotal.Location = new System.Drawing.Point(0, 288);
            this.pbTotal.Name = "pbTotal";
            this.pbTotal.Size = new System.Drawing.Size(679, 23);
            this.pbTotal.TabIndex = 4;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(511, 12);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(105, 59);
            this.bStart.TabIndex = 5;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bShowDebug
            // 
            this.bShowDebug.Location = new System.Drawing.Point(511, 77);
            this.bShowDebug.Name = "bShowDebug";
            this.bShowDebug.Size = new System.Drawing.Size(105, 23);
            this.bShowDebug.TabIndex = 6;
            this.bShowDebug.Text = "Show debug";
            this.bShowDebug.UseVisualStyleBackColor = true;
            this.bShowDebug.Click += new System.EventHandler(this.bShowDebug_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 310);
            this.Controls.Add(this.bShowDebug);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.pbTotal);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Plot Optimiser RW";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bwOptimizer;
        private System.Windows.Forms.TextBox tbInputFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOutputFolder;
        private System.Windows.Forms.Button bInputFileSelect;
        private System.Windows.Forms.Button bOutputFolderSelect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbDual;
        private System.Windows.Forms.RadioButton rbTypeOrg;
        private System.Windows.Forms.ProgressBar pbTotal;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bShowDebug;
    }
}

