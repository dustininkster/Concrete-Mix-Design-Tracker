using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class MainForm
    {
        /// <summary>
        /// Declarations for Prototypes Controls
        /// </summary>
        private System.Windows.Forms.Label lblPrototypeWcm;
        protected System.Windows.Forms.NumericUpDown udWcm;
        protected System.Windows.Forms.Button btnWcmReset;
        protected System.Windows.Forms.Button btnCopyPrototype;
        protected System.Windows.Forms.Panel pnProportionPanel;
        protected System.Windows.Forms.ListBox lstConcreteClassSelect;


       private void InitializePrototypesControls()
        {
            const int i = 1;
            this.lblPrototypeWcm = new Label();
            this.udWcm = new NumericUpDown();
            this.btnCopyPrototype = new Button();
            this.pnProportionPanel = new Panel();
            lstConcreteClassSelect = new ListBox();
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING + lblIDDisplay[i].Height + ROW_SPACING);

            // Properties

            btnAdvance[i].Text = "Trial Batch";

            pnProportionPanel.Width = spMainPanel2Split[i].Panel1.Width - 30;
            pnProportionPanel.Anchor = (AnchorStyles.Right | AnchorStyles.Left|AnchorStyles.Top|AnchorStyles.Bottom);


            btnCopyPrototype.Text = "Copy Prototype";
            btnCopyPrototype.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            btnCopyPrototype.Click += buttonEventHandler;

            lblPrototypeWcm.Text = "Water/ Cementitious Materials";
            lblPrototypeWcm.Width = System.Convert.ToInt32(Convert.ToDouble(MAIN_PANEL2_SPLITTER_DISTANCE) / 2.5);

            udWcm.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 5;
            udWcm.DecimalPlaces = 2;
            udWcm.Increment = (decimal)0.01;
            udWcm.Enabled = false;

            lstConcreteClassSelect.Width = MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING * 2);
            lstConcreteClassSelect.Dock = DockStyle.Fill;
            lstConcreteClassSelect.Visible = false;
            lstConcreteClassSelect.Items.Add("Class A");
            lstConcreteClassSelect.Items.Add("Class B");
            lstConcreteClassSelect.Items.Add("Class C");
            lstConcreteClassSelect.Items.Add("Class E");
            lstConcreteClassSelect.Items.Add("Class F");
            lstConcreteClassSelect.Items.Add("Class FH");
            lstConcreteClassSelect.Items.Add("Class FHM");
            lstConcreteClassSelect.Items.Add("Class FM");
            lstConcreteClassSelect.Items.Add("Class G");
            lstConcreteClassSelect.Items.Add("Class H");
            lstConcreteClassSelect.Items.Add("Class HES");
            lstConcreteClassSelect.Items.Add("Class M");




            // Top row w/cm label, picker, reset
            lblPrototypeWcm.Location = pntCurrentLocation;
            pntCurrentLocation.X += lblPrototypeWcm.Width;
            udWcm.Location = pntCurrentLocation;
            pntCurrentLocation.X += udWcm.Width;
            pntCurrentLocation.Y += ROW_SPACING;
            pntCurrentLocation.X = 13;


            // Proportion Panel
            pnProportionPanel.Location = pntCurrentLocation;
            pnProportionPanel.Height = spMainPanel2Split[i].Panel1.Height - ROW_SPACING - btnEditSave[i].Height - pntCurrentLocation.Y;

            // Locate everything
            // Test, Copy buttons
            pntCurrentLocation.X = btnAdvance[i].Location.X + btnAdvance[i].Width + PANEL_PADDING;
            pntCurrentLocation.Y = btnAdvance[i].Location.Y;
            btnCopyPrototype.Location = pntCurrentLocation;
            pntCurrentLocation.Y += ROW_SPACING;
            pntCurrentLocation.X = 13;

            /*pntCurrentLocation.X = 0;
            pntCurrentLocation.Y = 0;
            lstConcreteClassSelect.Location = pntCurrentLocation;
            */

            // Add items
            spMainPanel2Split[i].Panel1.Controls.Add(btnCopyPrototype);
            spMainPanel2Split[i].Panel1.Controls.Add(lblPrototypeWcm);
            spMainPanel2Split[i].Panel1.Controls.Add(udWcm);
            spMainPanel2Split[i].Panel1.Controls.Add(pnProportionPanel);
            pnProportionPanel.Controls.Add(lstConcreteClassSelect);

        }


    }
}
 