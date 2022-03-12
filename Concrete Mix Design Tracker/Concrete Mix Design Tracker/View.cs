using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete_Mix_Design_Tracker
{
    class View : MainForm 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public View() : base()
        {
         }

        /****************************************************************
         * This section has all of the output methods to each gui item  *
         ****************************************************************/

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

        public void DisplayInListBox(byte tab, List<string> listToDisplay)
        {
            lstItemSelect[tab].Items.Clear();
            foreach (string item in listToDisplay)
                lstItemSelect[tab].Items.Add(item);
        }

        public void DisplayProperties(byte tab, Dictionary<string, string> dicToDisplay)
        {
            //Each initialization method should include an assignment for the property grid to databind
            dgProperties[tab].Update();
        }

        public void FreezeTab(byte tab)
        {
            // for loop for all of the common controls
            //switch for all of the specific controls
        }
    

        /************************************************
         * This section has all of the event handlers   *
         ************************************************/

        private void View_Load(object sender, System.EventArgs e)
        {
        }

        private void cbFilterSelect_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        private void dgProperties_CurrentCellChanged(object sender, System.EventArgs e)
        {

        }
        private void lstItemSelect_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
        private void btnEditSave_Click(object sender, System.EventArgs e)
        {
        }

        private void btnAdvance_Click(object sender, System.EventArgs e)
        {

        }
    }
}
