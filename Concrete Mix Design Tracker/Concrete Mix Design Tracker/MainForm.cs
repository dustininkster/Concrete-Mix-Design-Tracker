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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeTabs();
            InitializeMaterialsControls();
            InitializePrototypesControls();
            InitializeTrialBatchesControls();
            InitializeSubmittalsControls();
            InitializeMixDesignsControls();
        }

         /// <summary>
        /// Displays a list of items into the filter combobox on a specific tab
        /// </summary>
        /// <param name="tab">Tab in which to display filter data</param>
        /// <param name="listToDisplay">List to display</param>
        public void DisplayInFilterBox(byte tab, List<string> listToDisplay)
        {
            cbFilterSelect[tab].Items.Clear();
            foreach (string item in listToDisplay)
                cbFilterSelect[tab].Items.Add(item);

        }

   }
}