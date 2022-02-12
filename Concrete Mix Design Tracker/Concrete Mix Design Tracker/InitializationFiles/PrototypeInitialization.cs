using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class Main
    {
        /// <summary>
        /// Declarations for Prototypes Controls
        /// </summary>
        private System.Windows.Forms.Label lblPrototypeWcm;
        private System.Windows.Forms.NumericUpDown udWcm;
        private System.Windows.Forms.Button btnWcmReset;
        private System.Windows.Forms.Button btnPromoteToTrialBatch;
        private System.Windows.Forms.Button btnCopyPrototype;
        private System.Windows.Forms.Label lblCementMaterials;
        private System.Windows.Forms.Button btnAddCement;
        private System.Windows.Forms.Label lblAggregateMaterials;
        private System.Windows.Forms.Button btnAddAggregate;
        private System.Windows.Forms.Label lblWaterMaterials;
        private System.Windows.Forms.Button btnAddWater;
        private System.Windows.Forms.Label lblAdmixtures;
        private System.Windows.Forms.Button btnAddAdmixtures;
        private System.Windows.Forms.Panel pnProportionPanel;


       private void InitializePrototypesControls()
        {
            const int ROW_SPACING = 33;
            const int i = 1;
            this.lblPrototypeWcm = new Label();
            this.udWcm = new NumericUpDown();
            this.btnWcmReset = new Button();
            this.btnPromoteToTrialBatch = new Button();
            this.btnCopyPrototype = new Button();
            this.lblCementMaterials = new Label();
            this.btnAddCement = new Button();
            this.lblAggregateMaterials = new Label();
            this.btnAddAggregate = new Button();
            this.lblWaterMaterials = new Label();
            this.btnAddWater = new Button();
            this.lblAdmixtures = new Label();
            this.btnAddAdmixtures = new Button();
            this.pnProportionPanel = new Panel();
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING + lblIDDisplay[i].Height + ROW_SPACING);

            // Properties

            btnAdvance[i].Text = "Trial Batch";

            pnProportionPanel.Width = spMainPanel2Split[i].Panel1.Width - 30;
            pnProportionPanel.Anchor = (AnchorStyles.Right | AnchorStyles.Left|AnchorStyles.Top|AnchorStyles.Bottom);

            btnPromoteToTrialBatch.Text = "Trial Batch";

            btnCopyPrototype.Text = "Copy Prototype";

            lblPrototypeWcm.Text = "Water/ Cementitious Materials";
            lblPrototypeWcm.Width = System.Convert.ToInt32(Convert.ToDouble(MAIN_PANEL2_SPLITTER_DISTANCE) / 2.5);

            udWcm.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 5;

            btnWcmReset.Text = "Reset";
            btnWcmReset.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 6;

            lblCementMaterials.Font = fntSectionHeader;
            lblCementMaterials.Text = "Cementitious Materials";
            lblCementMaterials.Width = pnProportionPanel.Width;

            lblAggregateMaterials.Font = fntSectionHeader;
            lblAggregateMaterials.Text = "Aggregates";
            lblAggregateMaterials.Width = pnProportionPanel.Width;

            lblWaterMaterials.Font = fntSectionHeader;
            lblWaterMaterials.Text = "Water";
            lblWaterMaterials.Width = pnProportionPanel.Width;

            lblAdmixtures.Font = fntSectionHeader;
            lblAdmixtures.Text = "Admixtures";
            lblAdmixtures.Width = pnProportionPanel.Width;


            // Locate everything
            // Test, Copy buttons
            btnPromoteToTrialBatch.Location = pntCurrentLocation;
            pntCurrentLocation.X += btnPromoteToTrialBatch.Width + PANEL_PADDING;
            btnCopyPrototype.Location = pntCurrentLocation;
            pntCurrentLocation.Y += ROW_SPACING;
            pntCurrentLocation.X = 13;

            // Top row w/cm label, picker, reset
            lblPrototypeWcm.Location = pntCurrentLocation;
            pntCurrentLocation.X += lblPrototypeWcm.Width;
            udWcm.Location = pntCurrentLocation;
            pntCurrentLocation.X += udWcm.Width;
            btnWcmReset.Location = pntCurrentLocation;
            pntCurrentLocation.Y += ROW_SPACING;
            pntCurrentLocation.X = 13;

            // Proportion Panel
            pnProportionPanel.Location = pntCurrentLocation;
            pnProportionPanel.Height = spMainPanel2Split[i].Panel1.Height - ROW_SPACING - btnEditSave[i].Height - pntCurrentLocation.Y;


            // Add items
            spMainPanel2Split[i].Panel1.Controls.Add(btnPromoteToTrialBatch);
            spMainPanel2Split[i].Panel1.Controls.Add(btnCopyPrototype);
            spMainPanel2Split[i].Panel1.Controls.Add(lblPrototypeWcm);
            spMainPanel2Split[i].Panel1.Controls.Add(udWcm);
            spMainPanel2Split[i].Panel1.Controls.Add(btnWcmReset);
            spMainPanel2Split[i].Panel1.Controls.Add(pnProportionPanel);

        }


    }
}
 