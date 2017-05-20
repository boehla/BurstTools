using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib;

namespace BurstTools {
    public partial class Form1 : Form {
        Options opts = new Options("BurstTools_settings.json");
        public Form1() {
            InitializeComponent();
            Lib.Logging.log("Start Aplication...");
        }

        private void Form1_Load(object sender, EventArgs e) {
            Logging.showForm(); 
        }

        private void bwOptimizer_DoWork(object sender, DoWorkEventArgs e) {
            StartArgs args = (StartArgs)e.Argument;
            if (!args.OutputFile.EndsWith("\\")) args.OutputFile += "\\";
            switch (args.Type) {
                case OptimiseType.Dual:
                    BurstOptimizerDual bodual = new BurstOptimizerDual();
                    bodual.optimize(args.InputFile, args.OutputFile, args.Ram);
                    break;
                case OptimiseType.Org:
                    BurstOptimizer bo = new BurstOptimizer();
                    bo.optimize(args.InputFile, args.OutputFile, args.Ram);
                    break;
            }
        }

        private void bwOptimizer_ProgressChanged(object sender, ProgressChangedEventArgs e) {

        }

        private void bwOptimizer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            MessageBox.Show("Finished");
        }

        private void saveUI() {
            opts.load();
            opts.set("inputfile", tbInputFile.Text);
            opts.set("outputfolder", tbOutputFolder.Text);
            opts.set("typeorg", rbTypeOrg.Checked);
        }
        private void loadUI() {
            tbInputFile.Text = opts.get("inputfile", "").Value;
            tbOutputFolder.Text = opts.get("outputfolder", "").Value;
            rbTypeOrg.Checked = opts.get("typeorg", true).BoolValue;
            opts.save();
        }

        private void bStart_Click(object sender, EventArgs e) {
            saveUI();
            StartArgs args = new StartArgs();
            args.InputFile = tbInputFile.Text;
            args.OutputFile = tbOutputFolder.Text;
            args.Ram = 1024 * 1024 * 1024;
            args.Type = rbTypeOrg.Checked ? OptimiseType.Org : OptimiseType.Dual;
            bwOptimizer.RunWorkerAsync(args);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            saveUI();
        }

        private void bInputFileSelect_Click(object sender, EventArgs e) {
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                tbInputFile.Text = openFileDialog.FileName;
            }
        }

        private void bOutputFolderSelect_Click(object sender, EventArgs e) {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                tbOutputFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
    enum OptimiseType { Org, Dual};
    class StartArgs {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public OptimiseType Type { get; set; }
        public long Ram { get; set; }
    }
}
