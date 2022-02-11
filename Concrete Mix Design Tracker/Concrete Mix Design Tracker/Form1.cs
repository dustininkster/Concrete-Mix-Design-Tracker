using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concrete_Mix_Design_Tracker
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            InitializeTabs();
        }

        /// <summary>
        /// Declarations for controls to add
        /// </summary>
        private System.Windows.Forms.TabControl tbSelectionTabs;
        private System.Windows.Forms.TabPage tbMaterials;
        private System.Windows.Forms.TabPage tbPrototypes;
        private System.Windows.Forms.TabPage tbTrialBatches;
        private System.Windows.Forms.TabPage tbSubmittals;
        private System.Windows.Forms.TabPage tbMixDesigns;

        private void InitializeTabs()
        {
            this.tbSelectionTabs = new System.Windows.Forms.TabControl();
            this.tbMaterials = new System.Windows.Forms.TabPage();
            this.tbPrototypes = new System.Windows.Forms.TabPage();
            this.tbTrialBatches = new System.Windows.Forms.TabPage();
            this.tbSubmittals = new System.Windows.Forms.TabPage();
            this.tbMixDesigns = new System.Windows.Forms.TabPage();

            tbMaterials.Text = "Materials";
            tbMaterials.Size = new System.Drawing.Size(256, 214);
            tbMaterials.TabIndex = 0;
            tbPrototypes.Text = "Prototypes";
            tbTrialBatches.Text = "Trial Batches";
            tbSubmittals.Text = "Submittals";
            tbMixDesigns.Text = "Mix Designs";

            tbSelectionTabs.Location = new System.Drawing.Point(40, 72);
            tbSelectionTabs.Size = new System.Drawing.Size(176, 32);
            tbSelectionTabs.SelectedIndex = 0;
            tbSelectionTabs.TabIndex = 0;
            tbSelectionTabs.Dock = DockStyle.Fill;

            this.Controls.Add(this.tbSelectionTabs);
            tbSelectionTabs.Controls.Add(this.tbMaterials);
            tbSelectionTabs.Controls.Add(this.tbPrototypes);
            tbSelectionTabs.Controls.Add(this.tbTrialBatches);
            tbSelectionTabs.Controls.Add(this.tbSubmittals);
            tbSelectionTabs.Controls.Add(this.tbMixDesigns);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
