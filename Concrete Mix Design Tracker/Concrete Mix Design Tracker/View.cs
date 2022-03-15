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


        const byte
            MATERIALS = 0,
            PROTOTYPE = 1,
            TRIAL_BATCH = 2,
            SUBMITTAL = 3,
            MIX_DESIGN = 4;


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

        /// <summary>
        /// Displays a list of strings in the listbox of a specific tab
        /// </summary>
        /// <param name="tab">tab in which to display the list</param>
        /// <param name="listToDisplay">String list to display</string></param>
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

        /// <summary>
        /// Freezes all the items on a specific tab
        /// </summary>
        /// <param name="tab">The tab to freeze</param>
        public void FreezeTab(byte tab)
        {
            btnEditSave[tab].Text = "Edit";

            switch (tab)
            {
                case MATERIALS:
                    txtName.Enabled =
                            txtSource.Enabled =
                            txtDensity.Enabled = false;
                    break;
                case PROTOTYPE:
                    udWcm.Enabled =
                        btnWcmReset.Enabled =
                        btnCopyPrototype.Enabled =
                        btnCopyPrototype.Enabled =
                        btnAddCement.Enabled =
                        btnAddAggregate.Enabled =
                        btnAddWater.Enabled =
                        btnAddAdmixtures.Enabled = false;
                    break;
                case TRIAL_BATCH:
                    txtNotes.Enabled =
                       rchTBPrototypeSummary.Enabled = false; 
                    break;
                case SUBMITTAL:
                    btnAddSubmittalFile.Enabled = false;
                    break;
                case MIX_DESIGN:
                    break;

            }
        }

        /// <summary>
        /// Thaws all the items on a specific tab
        /// </summary>
        /// <param name="tab">The tab to thaw</param>
        public void ThawTab(byte tab)
        {
            btnEditSave[tab].Text = "Save";

            switch (tab)
            {
                case MATERIALS:
                    txtName.Enabled =
                            txtSource.Enabled =
                            txtDensity.Enabled = true;
                    break;
                case PROTOTYPE:
                    udWcm.Enabled =
                        btnWcmReset.Enabled =
                        btnCopyPrototype.Enabled =
                        btnCopyPrototype.Enabled =
                        btnAddCement.Enabled =
                        btnAddAggregate.Enabled =
                        btnAddWater.Enabled =
                        btnAddAdmixtures.Enabled = true;
                    break;
                case TRIAL_BATCH:
                    txtNotes.Enabled =
                       rchTBPrototypeSummary.Enabled = true;
                    break;
                case SUBMITTAL:
                    btnAddSubmittalFile.Enabled = true;
                    break;
                case MIX_DESIGN:
                    break;
            }
        }

        // Does this need to change to "display cements" or something?
        // you could change it so that you send a parameter for what it gets and call this method for each type
        public void DisplayMaterials(Dictionary<string,string> dictionaryOfMaterials)
        {
            // This declaration is private and only for this scope
            System.Windows.Forms.Label[] lblMaterialNames;
            System.Windows.Forms.TextBox[] txtMaterialQtyOne;
            System.Windows.Forms.Label[] lblUnitOfMeasureOne;
            System.Windows.Forms.TextBox[] txtMaterialQtyTwo;
            System.Windows.Forms.Label[] lblUnitOfMeasureTwo;

            // Instantiate each array
            lblMaterialNames = new System.Windows.Forms.Label[dictionaryOfMaterials.Count];
            txtMaterialQtyOne = new System.Windows.Forms.TextBox[dictionaryOfMaterials.Count];
            lblUnitOfMeasureOne = new System.Windows.Forms.Label[dictionaryOfMaterials.Count];
            txtMaterialQtyTwo = new System.Windows.Forms.TextBox[dictionaryOfMaterials.Count];
            lblUnitOfMeasureTwo = new System.Windows.Forms.Label[dictionaryOfMaterials.Count];

            for (int i =0; i < dictionaryOfMaterials.Count; i++)
            {
                var material = dictionaryOfMaterials.ElementAt(i);

                // Instantiate each element of the array
                lblMaterialNames[i] = new System.Windows.Forms.Label();
                txtMaterialQtyOne[i] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureOne[i] = new System.Windows.Forms.Label();
                txtMaterialQtyTwo[i] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureTwo[i] = new System.Windows.Forms.Label();

                // Set properties for each element
                // lblMaterialNames
                lblMaterialNames[i].Text = material.Key;

                // continue going through each element and adding properties.

                // location
                // lay out each element line
                // put them in the panel (make sure it's protected so you can see it)

                // add everything




                
                
            }

        }
    }
}
