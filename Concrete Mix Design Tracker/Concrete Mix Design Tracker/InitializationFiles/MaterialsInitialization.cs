using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class MainForm
    {
        private System.Windows.Forms.Label lblName;
        protected System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSource;
        protected System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label lblDensity;
        protected System.Windows.Forms.TextBox txtDensity;
        private System.Windows.Forms.Label lblGrade;
        protected System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Label lblType;
        protected System.Windows.Forms.ComboBox cmbType;
        protected System.Windows.Forms.Label lblAddMaterialToPrototype;
        protected System.Windows.Forms.ListBox lstAddMaterialToPrototype;
        protected System.Windows.Forms.Button btnAddMaterial;
        private void InitializeMaterialsControls()
        {
            const int i = 0;
            this.lblName = new Label();
            this.txtName = new TextBox();
            this.lblSource = new Label();
            this.txtSource = new TextBox();
            this.lblGrade = new Label();
            this.txtGrade = new TextBox();
            this.lblDensity = new Label();
            this.txtDensity = new TextBox();
            this.lblType = new Label();
            this.cmbType = new ComboBox();
            this.lblAddMaterialToPrototype = new Label();
            this.lstAddMaterialToPrototype = new ListBox();
            this.btnAddMaterial = new Button();
            //System.Drawing.Bitmap TestImage = new System.Drawing.Bitmap(@"C:\Users\dusti\OneDrive\Documents\Capstone Project\Concrete Mix Design Tracker\Concrete Mix Design Tracker\bin\Debug\TestImage.png");
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING + lblIDDisplay[i].Height + ROW_SPACING);

            // Properties
            btnAdvance[i].Text = "Add To";

            lblName.Text = "Vendor Name";
            lblName.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;

            txtName.Text = "Name Test";
            txtName.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;
            txtName.Enabled = false;

            lblSource.Text = "Material Source";
            lblSource.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;

            txtSource.Text = "Source Test";
            txtSource.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;
            txtSource.Enabled = false;

            lblGrade.Text = "Material Grade";
            lblGrade.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;

            txtGrade.Text = "Grade Test";
            txtGrade.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;
            txtGrade.Enabled = false;

            lblType.Text = "Material Type";
            lblType.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;

            cmbType.Text = "Type Test";
            cmbType.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;
            cmbType.Items.Add("Cement");
            cmbType.Items.Add("SCM");
            cmbType.Items.Add("Coarse Aggregate");
            cmbType.Items.Add("Fine Aggregate");
            cmbType.Items.Add("Admixture");
            cmbType.Enabled = false;

            lblAddMaterialToPrototype.Text = "This text must be changed dynamically";
            lblAddMaterialToPrototype.Width = MAIN_PANEL2_SPLITTER_DISTANCE;
            lblAddMaterialToPrototype.Font = fntSectionHeader;
            lblAddMaterialToPrototype.Visible = false;

            lstAddMaterialToPrototype.Width = MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING *2);
            lstAddMaterialToPrototype.Visible = false;
            lstAddMaterialToPrototype.DoubleClick += lstAddToPrototypeDoubleClick;

            lblDensity.Text = "Relative Density";
            lblDensity.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;

            txtDensity.Text = "Density Test";
            txtDensity.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;
            txtDensity.Enabled = false;

            btnAddMaterial.Text = "Add Material";
            btnAddMaterial.Anchor = AnchorStyles.Bottom;
            btnAddMaterial.Click += buttonEventHandler;





            // Locate everything

            lblName.Location = pntCurrentLocation;
            pntCurrentLocation.X += lblName.Width + PANEL_PADDING;
            txtName.Location = pntCurrentLocation;
            pntCurrentLocation.X = PANEL_PADDING;
            pntCurrentLocation.Y += ROW_SPACING;

            lblSource.Location = pntCurrentLocation;
            pntCurrentLocation.X += lblSource.Width + PANEL_PADDING;
            txtSource.Location = pntCurrentLocation;
            pntCurrentLocation.X = PANEL_PADDING;
            pntCurrentLocation.Y += ROW_SPACING;

            lblGrade.Location = pntCurrentLocation;
            pntCurrentLocation.X += lblGrade.Width + PANEL_PADDING;
            txtGrade.Location = pntCurrentLocation;
            pntCurrentLocation.X = PANEL_PADDING;
            pntCurrentLocation.Y += ROW_SPACING;


            lblDensity.Location = pntCurrentLocation;
            pntCurrentLocation.X += lblDensity.Width + PANEL_PADDING;
            txtDensity.Location = pntCurrentLocation;
            pntCurrentLocation.X = PANEL_PADDING;
            pntCurrentLocation.Y += ROW_SPACING;

            lblType.Location = pntCurrentLocation;
            pntCurrentLocation.X += lblType.Width + PANEL_PADDING;
            cmbType.Location = pntCurrentLocation;
            pntCurrentLocation.X = PANEL_PADDING;
            pntCurrentLocation.Y += ROW_SPACING;

            lblAddMaterialToPrototype.Location = pntCurrentLocation;
            pntCurrentLocation.X = PANEL_PADDING;
            pntCurrentLocation.Y += ROW_SPACING;
            lstAddMaterialToPrototype.Location = pntCurrentLocation;
            



            pntCurrentLocation.X = btnAdvance[i].Location.X + btnAdvance[i].Width + PANEL_PADDING;
            pntCurrentLocation.Y = btnAdvance[i].Location.Y;
            btnAddMaterial.Location = pntCurrentLocation;

            // Put everything on the Materials form
            spMainPanel2Split[i].Panel1.Controls.Add(lblName);
            spMainPanel2Split[i].Panel1.Controls.Add(txtName);
            spMainPanel2Split[i].Panel1.Controls.Add(lblSource);
            spMainPanel2Split[i].Panel1.Controls.Add(txtSource);
            spMainPanel2Split[i].Panel1.Controls.Add(lblGrade);
            spMainPanel2Split[i].Panel1.Controls.Add(txtGrade);
            spMainPanel2Split[i].Panel1.Controls.Add(lblDensity);
            spMainPanel2Split[i].Panel1.Controls.Add(txtDensity);
            spMainPanel2Split[i].Panel1.Controls.Add(lblType);
            spMainPanel2Split[i].Panel1.Controls.Add(cmbType);
            spMainPanel2Split[i].Panel1.Controls.Add(btnAddMaterial);
            spMainPanel2Split[i].Panel1.Controls.Add(lblAddMaterialToPrototype);
            spMainPanel2Split[i].Panel1.Controls.Add(lstAddMaterialToPrototype);

        }


    }
}
