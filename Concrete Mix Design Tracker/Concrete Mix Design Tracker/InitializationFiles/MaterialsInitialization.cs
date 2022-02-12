using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class Main
    {
       private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label lblDensity;
        private System.Windows.Forms.TextBox txtDensity;
        private System.Windows.Forms.PictureBox pcMaterialImg;
        private System.Windows.Forms.Button btnAddMaterialsImg;
        private System.Windows.Forms.Button btnMaterialsImgLeft;
        private System.Windows.Forms.Button btnMaterialsImgRight;
        private void InitializeMaterialsControls()
        {
            const int ROW_SPACING = 35;
            const int i = 0;
            this.lblName = new Label();
            this.txtName = new TextBox();
            this.lblSource = new Label();
            this.txtSource = new TextBox();
            this.lblDensity = new Label();
            this.txtDensity = new TextBox();
            this.pcMaterialImg = new PictureBox();
            this.btnAddMaterialsImg = new Button();
            this.btnMaterialsImgLeft = new Button();
            this.btnMaterialsImgRight = new Button();
            //System.Drawing.Bitmap TestImage = new System.Drawing.Bitmap(@"C:\Users\dusti\OneDrive\Documents\Capstone Project\Concrete Mix Design Tracker\Concrete Mix Design Tracker\bin\Debug\TestImage.png");
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING + lblIDDisplay[i].Height + ROW_SPACING);

            // Properties
            btnAdvance[i].Text = "Add To";

            lblName.Text = "Material Name";
            lblName.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;

            txtName.Text = "Name Test";
            txtName.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;
            txtName.Enabled = false;

            lblSource.Text = "Material Source";
            lblSource.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;

            txtSource.Text = "Source Test";
            txtSource.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;
            txtSource.Enabled = false;

            lblDensity.Text = "Relative Density";
            lblDensity.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;

            txtDensity.Text = "Source Test";
            txtDensity.Width = MAIN_PANEL2_SPLITTER_DISTANCE / 3;
            txtDensity.Enabled = false;

            btnAddMaterialsImg.Text = "Add Image";
            btnAddMaterialsImg.Anchor = AnchorStyles.Bottom;

            pcMaterialImg.Size = new System.Drawing.Size(spImagePropSplit[i].Panel1.Width, spImagePropSplit[i].Panel1.Height - btnAddMaterialsImg.Height - (IMG_PANEL_PADDING*2));
            pcMaterialImg.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            pcMaterialImg.SizeMode = PictureBoxSizeMode.CenterImage;
            //pcMaterialImg.Image = TestImage;


            btnMaterialsImgLeft.Text = "<";
            btnMaterialsImgLeft.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            btnMaterialsImgLeft.Width = IMG_SELECT_BUTTON_SIZE;

            btnMaterialsImgRight.Text = ">";
            btnMaterialsImgRight.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            btnMaterialsImgRight.Width = IMG_SELECT_BUTTON_SIZE;


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

            lblDensity.Location = pntCurrentLocation;
            pntCurrentLocation.X += lblSource.Width + PANEL_PADDING;
            txtDensity.Location = pntCurrentLocation;
            pntCurrentLocation.X = PANEL_PADDING;
            pntCurrentLocation.Y += ROW_SPACING;

            pntCurrentLocation.X = (spImagePropSplit[i].Width - btnAddMaterialsImg.Width)/2;
            pntCurrentLocation.Y = spImagePropSplit[i].Panel1.Height - btnAddMaterialsImg.Height - IMG_PANEL_PADDING;
            btnAddMaterialsImg.Location = pntCurrentLocation;
            pntCurrentLocation.X = IMG_PANEL_PADDING;
            btnMaterialsImgLeft.Location = pntCurrentLocation;
            pntCurrentLocation.X = spImagePropSplit[i].Panel1.Width - btnMaterialsImgRight.Width - IMG_PANEL_PADDING;
            btnMaterialsImgRight.Location = pntCurrentLocation;

            // Put everything on the Materials form
            spImagePropSplit[i].Panel1.Controls.Add(pcMaterialImg);
            spImagePropSplit[i].Panel1.Controls.Add(btnAddMaterialsImg);
            spImagePropSplit[i].Panel1.Controls.Add(btnMaterialsImgLeft);
            spImagePropSplit[i].Panel1.Controls.Add(btnMaterialsImgRight);
            spMainPanel2Split[i].Panel1.Controls.Add(lblName);
            spMainPanel2Split[i].Panel1.Controls.Add(txtName);
            spMainPanel2Split[i].Panel1.Controls.Add(lblSource);
            spMainPanel2Split[i].Panel1.Controls.Add(txtSource);
            spMainPanel2Split[i].Panel1.Controls.Add(lblDensity);
            spMainPanel2Split[i].Panel1.Controls.Add(txtDensity);
        }


    }
}
