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
            dgProperties[tab].Rows.Clear();
            foreach (var item in dicToDisplay)
            {
                dgProperties[tab].Rows.Add( item.Key, item.Value);
            }
        }

        /// <summary>
        /// Freezes all the items on a specific tab
        /// </summary>
        /// <param name="tab">The tab to freeze</param>
        public void FreezeTab(byte tab)
        {
            btnEditSave[tab].Text = "Edit";
            lstItemSelect[tab].Enabled = true;
            cbFilterSelect[tab].Enabled = true;
            dgProperties[tab].Enabled = false;
            btnEditSave[tab].Enabled = true;
            btnAddMaterial.Enabled = true;

            switch (tab)
            {
                case MATERIALS:
                    txtName.Enabled =
                            txtSource.Enabled =
                            txtGrade.Enabled =
                            txtDensity.Enabled = false;
                    break;
                case PROTOTYPE:
                    udWcm.Enabled = false;
                    for (int i = pnProportionPanel.Controls.Count-1; i >= 0; i--)
                    {
                        if (pnProportionPanel.Controls[i] is System.Windows.Forms.TextBox)
                            pnProportionPanel.Controls[i].Enabled = false;
                    }
                    break;
                case TRIAL_BATCH:
                    txtNotes.Enabled = false;
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
            lstItemSelect[tab].Enabled = false;
            cbFilterSelect[tab].Enabled = false;
            dgProperties[tab].Enabled = true;
            btnEditSave[tab].Enabled = true;
            btnAdvance[tab].Enabled = false;
            btnAddMaterial.Enabled = false;
            lstAddMaterialToPrototype.Visible = false;
            lstItemSelect[MATERIALS].SelectedIndex = lstItemSelect[MATERIALS].SelectedIndex;

            switch (tab)
            {
                case MATERIALS:
                    txtName.Enabled =
                            txtSource.Enabled =
                            txtGrade.Enabled =
                            txtDensity.Enabled = true;
                    cmbType.Enabled = false;
                    btnAddMaterial.Text = "Add Material";
                    break;
                case PROTOTYPE:
                    udWcm.Enabled = false;
                    for (int i = pnProportionPanel.Controls.Count-1; i >= 0; i--)
                    {
                        if (pnProportionPanel.Controls[i] is System.Windows.Forms.TextBox)
                            pnProportionPanel.Controls[i].Enabled = true;
                    }
                    break;
                case TRIAL_BATCH:
                    txtNotes.Enabled = false;
                    break;
                case SUBMITTAL:
                    btnAddSubmittalFile.Enabled = true;
                    break;
                case MIX_DESIGN:
                    break;
            }
        }

        System.Windows.Forms.Label[] lblMaterialNames;
        System.Windows.Forms.TextBox[] txtMaterialQtyOne;
        System.Windows.Forms.Label[] lblUnitOfMeasureOne;
        System.Windows.Forms.TextBox[] txtMaterialQtyTwo;
        System.Windows.Forms.Label[] lblUnitOfMeasureTwo;
        // Does this need to change to "display cements" or something?
        // you could change it so that you send a parameter for what it gets and call this method for each type
        public void DisplayMaterials(
            List<MaterialField> cementList,
            List<MaterialField> scmList,
            List<MaterialField> caList,
            List<MaterialField> fnList,
            List<MaterialField> admixList)
        {
            byte totalMaterialCount = (byte)(cementList.Count + scmList.Count + caList.Count + fnList.Count + admixList.Count);
            const int PADDING = 5;
            const int NAME_LABEL_WIDTH = 100;
            const int TXT1_LABEL_WIDTH = 80;
            const int QTY1_LABEL_WIDTH = 60;
            const int TXT2_LABEL_WIDTH = 80;
            const int QTY2_LABEL_WIDTH = 80;
            const int HEADER_WIDTH = 300;

            // Instantiate each array
            lblMaterialNames = new System.Windows.Forms.Label[totalMaterialCount];
            txtMaterialQtyOne = new System.Windows.Forms.TextBox[totalMaterialCount];
            lblUnitOfMeasureOne = new System.Windows.Forms.Label[totalMaterialCount];
            txtMaterialQtyTwo = new System.Windows.Forms.TextBox[totalMaterialCount];
            lblUnitOfMeasureTwo = new System.Windows.Forms.Label[totalMaterialCount];
            System.Windows.Forms.Label lblCementHeader = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblSCMHeader = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblCAHeader = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblFNHeader = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblAdmixHeader = new System.Windows.Forms.Label();
            System.Drawing.Font fntSectionHeader = new System.Drawing.Font("Courier New", 15, System.Drawing.FontStyle.Regular);
            System.Drawing.Point location = new System.Drawing.Point();

            // Header Properties
            lblCementHeader.Text = "Cement";
            lblSCMHeader.Text = "Supplementary Cementitious Materials";
            lblCAHeader.Text = "Coarse Aggregates";
            lblFNHeader.Text = "Fine Aggregates";
            lblAdmixHeader.Text = "Chemical Admixtures";
            lblCementHeader.Font =
                lblSCMHeader.Font =
                lblCAHeader.Font =
                lblFNHeader.Font =
                lblAdmixHeader.Font = fntSectionHeader;
            lblCementHeader.Width =
                lblSCMHeader.Width =
                lblCAHeader.Width =
                lblFNHeader.Width =
                lblAdmixHeader.Width = HEADER_WIDTH;



            location.X = PADDING;
            lblCementHeader.Location = location;
            for (int i = 0; i < cementList.Count; i++)
            {

                // Instantiate each element of the array
                lblMaterialNames[i] = new System.Windows.Forms.Label();
                txtMaterialQtyOne[i] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureOne[i] = new System.Windows.Forms.Label();
                txtMaterialQtyTwo[i] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureTwo[i] = new System.Windows.Forms.Label();

                lblMaterialNames[i].Text = cementList[i].Name;
                txtMaterialQtyOne[i].Text = cementList[i].QuantityOne;
                lblUnitOfMeasureOne[i].Text = "lbs";
                txtMaterialQtyTwo[i].Text = string.Format("{0:F2}", Convert.ToDecimal(cementList[i].QuantityTwo));
                lblUnitOfMeasureTwo[i].Text = "cubic feet";

                // properties
                lblMaterialNames[i].Width = NAME_LABEL_WIDTH;
                lblMaterialNames[i].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                txtMaterialQtyOne[i].Width = TXT1_LABEL_WIDTH;
                txtMaterialQtyOne[i].Enabled = false;
                lblUnitOfMeasureOne[i].Width = QTY1_LABEL_WIDTH;
                lblUnitOfMeasureOne[i].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                txtMaterialQtyTwo[i].Width = TXT2_LABEL_WIDTH;
                txtMaterialQtyTwo[i].Enabled = false;
                lblUnitOfMeasureTwo[i].Width = QTY2_LABEL_WIDTH;
                lblUnitOfMeasureTwo[i].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                location.X = PADDING;
                location.Y += ROW_SPACING;
                lblMaterialNames[i].Location = location;
                location.X += lblMaterialNames[i].Width;
                txtMaterialQtyOne[i].Location = location;
                location.X += txtMaterialQtyOne[i].Width;
                lblUnitOfMeasureOne[i].Location = location;
                location.X += lblUnitOfMeasureOne[i].Width;
                txtMaterialQtyTwo[i].Location = location;
                location.X += txtMaterialQtyTwo[i].Width;
                lblUnitOfMeasureTwo[i].Location = location;

                pnProportionPanel.Controls.Add(lblMaterialNames[i]);
                pnProportionPanel.Controls.Add(txtMaterialQtyOne[i]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureOne[i]);
                pnProportionPanel.Controls.Add(txtMaterialQtyTwo[i]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureTwo[i]);
            }

            byte index = (byte)cementList.Count;
            location.Y += ROW_SPACING;
            location.X = PADDING;
            lblSCMHeader.Location = location;
            for (byte i =0; i < scmList.Count; i++)
            {
                index += i;

                // Instantiate each element of the array
                lblMaterialNames[index] = new System.Windows.Forms.Label();
                txtMaterialQtyOne[index] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureOne[index] = new System.Windows.Forms.Label();
                txtMaterialQtyTwo[index] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureTwo[index] = new System.Windows.Forms.Label();

                lblMaterialNames[index].Text = scmList[i].Name;
                txtMaterialQtyOne[index].Text = scmList[i].QuantityOne;
                lblUnitOfMeasureOne[index].Text = "lbs";
                txtMaterialQtyTwo[index].Text = string.Format("{0:F2}", Convert.ToDecimal(scmList[i].QuantityTwo));
                lblUnitOfMeasureTwo[index].Text = "cubic feet";

                // properties
                lblMaterialNames[index].Width = NAME_LABEL_WIDTH;
                lblMaterialNames[index].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                txtMaterialQtyOne[index].Width = TXT1_LABEL_WIDTH;
                txtMaterialQtyOne[index].Enabled = false;
                lblUnitOfMeasureOne[index].Width = QTY1_LABEL_WIDTH;
                lblUnitOfMeasureOne[index].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                txtMaterialQtyTwo[index].Width = TXT2_LABEL_WIDTH;
                txtMaterialQtyTwo[index].Enabled = false;
                lblUnitOfMeasureTwo[index].Width = QTY2_LABEL_WIDTH;
                lblUnitOfMeasureTwo[index].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                location.X = PADDING;
                location.Y += ROW_SPACING;
                lblMaterialNames[index].Location = location;
                location.X += lblMaterialNames[index].Width;
                txtMaterialQtyOne[index].Location = location;
                location.X += txtMaterialQtyOne[index].Width;
                lblUnitOfMeasureOne[index].Location = location;
                location.X += lblUnitOfMeasureOne[index].Width;
                txtMaterialQtyTwo[index].Location = location;
                location.X += txtMaterialQtyTwo[index].Width;
                lblUnitOfMeasureTwo[index].Location = location;

                pnProportionPanel.Controls.Add(lblMaterialNames[index]);
                pnProportionPanel.Controls.Add(txtMaterialQtyOne[index]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureOne[index]);
                pnProportionPanel.Controls.Add(txtMaterialQtyTwo[index]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureTwo[index]);

            }

            location.Y += ROW_SPACING;
            location.X = PADDING;
            lblCAHeader.Location = location;
            for (byte i =0; i < caList.Count; i++)
            {
                index += i;

                // Instantiate each element of the array
                lblMaterialNames[index] = new System.Windows.Forms.Label();
                txtMaterialQtyOne[index] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureOne[index] = new System.Windows.Forms.Label();
                txtMaterialQtyTwo[index] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureTwo[index] = new System.Windows.Forms.Label();

                lblMaterialNames[index].Text = caList[i].Name;
                txtMaterialQtyOne[index].Text = caList[i].QuantityOne;
                lblUnitOfMeasureOne[index].Text = "lbs";
                txtMaterialQtyTwo[index].Text = string.Format("{0:F2}", Convert.ToDecimal(caList[i].QuantityTwo));
                lblUnitOfMeasureTwo[index].Text = "cubic feet";

                // properties
                lblMaterialNames[index].Width = NAME_LABEL_WIDTH;
                lblMaterialNames[index].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                txtMaterialQtyOne[index].Width = TXT1_LABEL_WIDTH;
                txtMaterialQtyOne[index].Enabled = false;
                lblUnitOfMeasureOne[index].Width = QTY1_LABEL_WIDTH;
                lblUnitOfMeasureOne[index].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                txtMaterialQtyTwo[index].Width = QTY2_LABEL_WIDTH;
                txtMaterialQtyTwo[index].Enabled = false;
                lblUnitOfMeasureTwo[index].Width = TXT2_LABEL_WIDTH;
                lblUnitOfMeasureTwo[index].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                location.X = PADDING;
                location.Y += ROW_SPACING;
                lblMaterialNames[index].Location = location;
                location.X += lblMaterialNames[index].Width;
                txtMaterialQtyOne[index].Location = location;
                location.X += txtMaterialQtyOne[index].Width;
                lblUnitOfMeasureOne[index].Location = location;
                location.X += lblUnitOfMeasureOne[index].Width;
                txtMaterialQtyTwo[index].Location = location;
                location.X += txtMaterialQtyTwo[index].Width;
                lblUnitOfMeasureTwo[index].Location = location;

                pnProportionPanel.Controls.Add(lblMaterialNames[index]);
                pnProportionPanel.Controls.Add(txtMaterialQtyOne[index]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureOne[index]);
                pnProportionPanel.Controls.Add(txtMaterialQtyTwo[index]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureTwo[index]);

            }

            location.Y += ROW_SPACING;
            location.X = PADDING;
            lblFNHeader.Location = location;
            for (byte i =0; i < fnList.Count; i++)
            {
                index += i;

                // Instantiate each element of the array
                lblMaterialNames[index] = new System.Windows.Forms.Label();
                txtMaterialQtyOne[index] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureOne[index] = new System.Windows.Forms.Label();
                txtMaterialQtyTwo[index] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureTwo[index] = new System.Windows.Forms.Label();

                lblMaterialNames[index].Text = fnList[i].Name;
                txtMaterialQtyOne[index].Text = fnList[i].QuantityOne;
                lblUnitOfMeasureOne[index].Text = "lbs";
                txtMaterialQtyTwo[index].Text = string.Format("{0:F2}", Convert.ToDecimal(fnList[i].QuantityTwo));
                lblUnitOfMeasureTwo[index].Text = "cubic feet";

                // properties
                lblMaterialNames[index].Width = NAME_LABEL_WIDTH;
                lblMaterialNames[index].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                txtMaterialQtyOne[index].Width = TXT1_LABEL_WIDTH;
                txtMaterialQtyOne[index].Enabled = false;
                lblUnitOfMeasureOne[index].Width = QTY1_LABEL_WIDTH;
                lblUnitOfMeasureOne[index].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                txtMaterialQtyTwo[index].Width = TXT2_LABEL_WIDTH;
                txtMaterialQtyTwo[index].Enabled = false;
                lblUnitOfMeasureTwo[index].Width = QTY2_LABEL_WIDTH;
                lblUnitOfMeasureTwo[index].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                location.X = PADDING;
                location.Y += ROW_SPACING;
                lblMaterialNames[index].Location = location;
                location.X += lblMaterialNames[index].Width;
                txtMaterialQtyOne[index].Location = location;
                location.X += txtMaterialQtyOne[index].Width;
                lblUnitOfMeasureOne[index].Location = location;
                location.X += lblUnitOfMeasureOne[index].Width;
                txtMaterialQtyTwo[index].Location = location;
                location.X += txtMaterialQtyTwo[index].Width;
                lblUnitOfMeasureTwo[index].Location = location;

                pnProportionPanel.Controls.Add(lblMaterialNames[index]);
                pnProportionPanel.Controls.Add(txtMaterialQtyOne[index]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureOne[index]);
                pnProportionPanel.Controls.Add(txtMaterialQtyTwo[index]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureTwo[index]);

            }

            location.Y += ROW_SPACING;
            location.X = PADDING;
            lblAdmixHeader.Location = location;
            for (byte i =0; i < admixList.Count; i++)
            {
                index += i;

                // Instantiate each element of the array
                lblMaterialNames[index] = new System.Windows.Forms.Label();
                txtMaterialQtyOne[index] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureOne[index] = new System.Windows.Forms.Label();
                txtMaterialQtyTwo[index] = new System.Windows.Forms.TextBox();
                lblUnitOfMeasureTwo[index] = new System.Windows.Forms.Label();

                lblMaterialNames[index].Text = admixList[i].Name;
                txtMaterialQtyOne[index].Text = admixList[i].QuantityOne;
                lblUnitOfMeasureOne[index].Text = "oz/ cwt";
                txtMaterialQtyTwo[index].Text = string.Format("{0:F2}", Convert.ToDecimal(admixList[i].QuantityTwo));
                lblUnitOfMeasureTwo[index].Text = "oz/ yd";

                // properties
                lblMaterialNames[index].Width = NAME_LABEL_WIDTH;
                lblMaterialNames[index].TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                txtMaterialQtyOne[index].Width = TXT1_LABEL_WIDTH;
                txtMaterialQtyOne[index].Enabled = false;
                lblUnitOfMeasureOne[index].Width = QTY1_LABEL_WIDTH;
                lblUnitOfMeasureOne[index].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                txtMaterialQtyTwo[index].Width = TXT2_LABEL_WIDTH;
                txtMaterialQtyTwo[index].Enabled = false;
                lblUnitOfMeasureTwo[index].Width = QTY2_LABEL_WIDTH;
                lblUnitOfMeasureTwo[index].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                location.X = PADDING;
                location.Y += ROW_SPACING;
                lblMaterialNames[index].Location = location;
                location.X += lblMaterialNames[index].Width;
                txtMaterialQtyOne[index].Location = location;
                location.X += txtMaterialQtyOne[index].Width;
                lblUnitOfMeasureOne[index].Location = location;
                location.X += lblUnitOfMeasureOne[index].Width;
                txtMaterialQtyTwo[index].Location = location;
                location.X += txtMaterialQtyTwo[index].Width;
                lblUnitOfMeasureTwo[index].Location = location;

                pnProportionPanel.Controls.Add(lblMaterialNames[index]);
                pnProportionPanel.Controls.Add(txtMaterialQtyOne[index]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureOne[index]);
                pnProportionPanel.Controls.Add(txtMaterialQtyTwo[index]);
                pnProportionPanel.Controls.Add(lblUnitOfMeasureTwo[index]);

            }

            pnProportionPanel.Controls.Add(lblCementHeader);
            pnProportionPanel.Controls.Add(lblSCMHeader);
            pnProportionPanel.Controls.Add(lblCAHeader);
            pnProportionPanel.Controls.Add(lblFNHeader);
            pnProportionPanel.Controls.Add(lblAdmixHeader);
        }

        public void DisplaySelectionName(byte tab, string selection)
        {
            lblIDDisplay[tab].Text = selection;
        }

        public void DisplayWaterCMRatio(decimal ratio)
        {
            udWcm.Value = ratio;
        }
        public void ClearPrototypePanel()
        {
            for(int i = pnProportionPanel.Controls.Count-1; i >= 0; i--)
            {
                System.Windows.Forms.Control control = pnProportionPanel.Controls[i];
                if (control is System.Windows.Forms.TextBox)
                    control.TextChanged -= new System.EventHandler(base.buttonEventHandler);

                if (!(control is System.Windows.Forms.ListBox))
                {
                    pnProportionPanel.Controls.Remove(control);
                    control.Dispose();
                }
            }
        }

        public void DisplayMaterialInfo(string name, string source, string grade, decimal density, byte type)
        {
            txtName.Text = name;
            txtSource.Text = source;
            txtGrade.Text = grade;
            txtDensity.Text = string.Format("{0:F2}", density);
            cmbType.SelectedIndex = type;
        }

        public void DisplayTrialBatchSummary(string summary)
        {
            rchTBPrototypeSummary.Text = summary;
        }
        public string GetEnteredMaterialVendor()
        {
            string returnValue;
            returnValue = txtName.Text;
            return returnValue;
        }
        public string GetEnteredMaterialSource()
        {
            string returnValue;
            returnValue = txtSource.Text;
            return returnValue;
        }
        public string GetEnteredGrade()
        {
            string returnValue;
            returnValue = txtGrade.Text;
            return returnValue;
        }
        public decimal GetEnteredRelativeDensity()
        {
            decimal returnValue;
            returnValue = decimal.Parse(txtDensity.Text);
            return returnValue;
        }
        public byte GetEnteredMaterialType()
        {
            return (byte)cmbType.SelectedIndex;
        }
        public byte GetSelectedMaterialID()
        {
            return byte.Parse(lstItemSelect[MATERIALS].Text.Substring(0,3));
        }

        public Dictionary<string, string> GetEnteredProperties()
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();

            return dicReturn;
        }

        public void MaterialSelectMode()
        {
            byte i = 0;
            lblIDDisplay[i].Text = "Select Material Type";
            lstItemSelect[i].Enabled =
                cbFilterSelect[i].Enabled =
                txtName.Enabled =
                txtSource.Enabled =
                txtGrade.Enabled =
                dgProperties[i].Enabled =
                btnAdvance[i].Enabled =
                btnEditSave[i].Enabled =
                txtDensity.Enabled = false;
            txtName.Text =
                txtSource.Text =
                txtDensity.Text =
                txtGrade.Text = "";
                dgProperties[i].Rows.Clear();

            btnAddMaterial.Text = "Accept Type";
            cmbType.Enabled = true;
        }

        public void SelectLastIndex(byte tab)
        {
            lstItemSelect[tab].SelectedIndex = lstItemSelect[tab].Items.Count - 1;
        }
        public string GetFilterSelection(byte tab)
        {
            return cbFilterSelect[tab].Text;
        }

        public void SelectPrototypeToAddMaterialMode()
        {
            cbFilterSelect[MATERIALS].Enabled =
               lstItemSelect[MATERIALS].Enabled =
               btnEditSave[MATERIALS].Enabled =
               btnAddMaterial.Enabled =
               txtName.Enabled =
               txtSource.Enabled =
               txtGrade.Enabled =
               cmbType.Enabled =
               btnAdvance[MATERIALS].Enabled =
               txtDensity.Enabled = false;
            lstAddMaterialToPrototype.Visible =
                lblAddMaterialToPrototype.Visible = true;
            lblAddMaterialToPrototype.Text = string.Format("Add {0} {1} to which Prototype?", txtSource.Text, txtGrade.Text);

            lstAddMaterialToPrototype.Items.Add("Create New Prototype");

        }

        public void DisplayPrototypesInSelectionListBox(List<string> listToDisplay)
        {
            foreach (var item in listToDisplay)
            {
                lstAddMaterialToPrototype.Items.Add(item);
            }
        }
        public void OpenTab(byte tab)
        {
            tbSelectionTabs.SelectedIndex = tab;
        }
        public void ClassSelectMode()
        {
            lstItemSelect[PROTOTYPE].Enabled = false;
            cbFilterSelect[PROTOTYPE].Enabled = false;
            lblIDDisplay[PROTOTYPE].Text = "Select Concrete Class for New Prototype";
            udWcm.Value = 0;
            ClearPrototypePanel();
            lstConcreteClassSelect.Visible = true;
        }
    }
}
