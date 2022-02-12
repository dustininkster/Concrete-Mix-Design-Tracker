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
            InitializeDatabase();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}