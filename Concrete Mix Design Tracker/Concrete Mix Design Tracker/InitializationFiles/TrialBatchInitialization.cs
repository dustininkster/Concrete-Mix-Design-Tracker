using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class MainForm
    {
  
        private System.Windows.Forms.Label lblNotes;
        protected System.Windows.Forms.TextBox txtNotes;
        protected System.Windows.Forms.RichTextBox rchTBPrototypeSummary;
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
            this.pcTrialBatchImg = new PictureBox();
            this.btnAddTrialBatchImg = new Button();
            this.btnTrialBatchImgLeft = new Button();
            this.btnTrialBatchImgRight = new Button();
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING + lblIDDisplay[i].Height + ROW_SPACING);

            //Properties

            btnAdvance[i].Text = "Submit Results";

            lblNotes.Text = "Notes";
            lblNotes.Font = fntSectionHeader;

            System.Drawing.Size size = new System.Drawing.Size(MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING * 2), spMainPanel2Split[i].Height/3);
            txtNotes.Multiline = true;
            txtNotes.Anchor = (AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right| AnchorStyles.Top);


            btnAddTrialBatchImg.Text = "Add Image";
            btnAddTrialBatchImg.Anchor = AnchorStyles.Bottom;
            btnAddTrialBatchImg.Click += buttonEventHandler;

            pcTrialBatchImg.Size = new System.Drawing.Size(spImagePropSplit[i].Panel1.Height - btnAddMaterialsImg.Height - (IMG_PANEL_PADDING * 2), spImagePropSplit[i].Panel1.Width);
            pcTrialBatchImg.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            pcTrialBatchImg.SizeMode = PictureBoxSizeMode.CenterImage;

            btnTrialBatchImgLeft.Text = "<";
            btnTrialBatchImgLeft.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            btnTrialBatchImgLeft.Width = IMG_SELECT_BUTTON_SIZE;
            btnTrialBatchImgLeft.Click += buttonEventHandler;

            btnTrialBatchImgRight.Text = ">";
            btnTrialBatchImgRight.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            btnTrialBatchImgRight.Width = IMG_SELECT_BUTTON_SIZE;
            btnTrialBatchImgRight.Click += buttonEventHandler;

            // Locate everything

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

            spImagePropSplit[i].Panel1.Controls.Add(pcTrialBatchImg);
            spImagePropSplit[i].Panel1.Controls.Add(btnAddTrialBatchImg);
            spImagePropSplit[i].Panel1.Controls.Add(btnTrialBatchImgLeft);
            spImagePropSplit[i].Panel1.Controls.Add(btnTrialBatchImgRight);
            spMainPanel2Split[i].Panel1.Controls.Add(lblNotes);
            spMainPanel2Split[i].Panel1.Controls.Add(txtNotes);
        }

    }
}
 