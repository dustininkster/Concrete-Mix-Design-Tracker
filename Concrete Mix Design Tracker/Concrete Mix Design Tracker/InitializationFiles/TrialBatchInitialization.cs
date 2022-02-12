using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class Main
    {
  
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.RichTextBox rchPrototypeSummary;
        private System.Windows.Forms.PictureBox pcTrialBatchImg;
        private System.Windows.Forms.Button btnAddTrialBatchImg;
        private System.Windows.Forms.Button btnTrialBatchImgRight;
        private System.Windows.Forms.Button btnTrialBatchImgLeft;
        private void InitializeTrialBatchesControls()
        {
            const int ROW_SPACING = 35;
            const int i = 2;
            this.lblNotes = new Label();
            this.txtNotes = new TextBox();
            this.rchPrototypeSummary = new RichTextBox();
            this.pcTrialBatchImg = new PictureBox();
            this.btnAddTrialBatchImg = new Button();
            this.btnTrialBatchImgLeft = new Button();
            this.btnTrialBatchImgRight = new Button();
            System.Drawing.Bitmap TestImage = new System.Drawing.Bitmap(@"C:\Users\dusti\OneDrive\Documents\Capstone Project\Concrete Mix Design Tracker\Concrete Mix Design Tracker\bin\Debug\TrialBatchTest.jpg");
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING + lblIDDisplay[i].Height + ROW_SPACING);

            //Properties

            lblNotes.Text = "Notes";
            lblNotes.Font = fntSectionHeader;

            System.Drawing.Size size = new System.Drawing.Size(MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING * 2), spMainPanel2Split[i].Height/3);
            txtNotes.Multiline = true;
            txtNotes.Anchor = (AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right| AnchorStyles.Top);

            rchPrototypeSummary.Size = size;
            rchPrototypeSummary.Anchor = (AnchorStyles.Left | AnchorStyles.Right|AnchorStyles.Top);

            btnAddTrialBatchImg.Text = "Add Image";
            btnAddTrialBatchImg.Anchor = AnchorStyles.Bottom;

            pcTrialBatchImg.Size = new System.Drawing.Size(spImagePropSplit[i].Panel1.Height - btnAddMaterialsImg.Height - (IMG_PANEL_PADDING * 2), spImagePropSplit[i].Panel1.Width);
            pcTrialBatchImg.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            pcTrialBatchImg.SizeMode = PictureBoxSizeMode.CenterImage;
            pcTrialBatchImg.Image = TestImage;

            btnTrialBatchImgLeft.Text = "<";
            btnTrialBatchImgLeft.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            btnTrialBatchImgLeft.Width = IMG_SELECT_BUTTON_SIZE;

            btnTrialBatchImgRight.Text = ">";
            btnTrialBatchImgRight.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            btnTrialBatchImgRight.Width = IMG_SELECT_BUTTON_SIZE;

            // Locate everything

            rchPrototypeSummary.Location = pntCurrentLocation;
            pntCurrentLocation.Y += ROW_SPACING + rchPrototypeSummary.Height;
            lblNotes.Location = pntCurrentLocation;
            pntCurrentLocation.Y += ROW_SPACING;
            txtNotes.Location = pntCurrentLocation;
            txtNotes.Size = new System.Drawing.Size(
                MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING * 2),
                spMainPanel2Split[i].Height - pntCurrentLocation.Y - btnEditSave[i].Height - (PANEL_PADDING*2));

            pntCurrentLocation.X = (spImagePropSplit[i].Width - btnAddTrialBatchImg.Width)/2;
            pntCurrentLocation.Y = spImagePropSplit[i].Panel1.Height - btnAddTrialBatchImg.Height - IMG_PANEL_PADDING;
            btnAddTrialBatchImg.Location = pntCurrentLocation;
            pntCurrentLocation.X = IMG_PANEL_PADDING;
            btnTrialBatchImgLeft.Location = pntCurrentLocation;
            pntCurrentLocation.X = spImagePropSplit[i].Panel1.Width - btnTrialBatchImgRight.Width - IMG_PANEL_PADDING;
            btnTrialBatchImgRight.Location = pntCurrentLocation;

            // Put everything on the Materials Form
            spImagePropSplit[i].Panel1.Controls.Add(pcTrialBatchImg);
            spImagePropSplit[i].Panel1.Controls.Add(btnAddTrialBatchImg);
            spImagePropSplit[i].Panel1.Controls.Add(btnTrialBatchImgLeft);
            spImagePropSplit[i].Panel1.Controls.Add(btnTrialBatchImgRight);
            spMainPanel2Split[i].Panel1.Controls.Add(rchPrototypeSummary);
            spMainPanel2Split[i].Panel1.Controls.Add(lblNotes);
            spMainPanel2Split[i].Panel1.Controls.Add(txtNotes);
        }

    }
}
 