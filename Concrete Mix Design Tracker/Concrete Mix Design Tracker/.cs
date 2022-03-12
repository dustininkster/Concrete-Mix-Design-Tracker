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
            InitializeMaterialsControls();
            InitializePrototypesControls();
            InitializeTrialBatchesControls();
            InitializeSubmittalsControls();
            InitializeMixDesignsControls();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            for (byte i = 0; i < tbSelectionTabs.TabCount; i++)
            {
                foreach (string item in Controller.GetFilters(i))
                    cbFilterSelect[i].Items.Add(item);
                foreach (var item in Controller.GetList(i))
                    lstItemSelect[i].Items.Add(item);
            }
        }
    }
}