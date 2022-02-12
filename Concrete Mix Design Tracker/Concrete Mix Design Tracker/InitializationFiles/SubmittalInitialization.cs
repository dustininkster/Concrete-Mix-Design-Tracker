using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class Main
    { 
        private System.Windows.Forms.ListBox lstSubmittalFileList;
        private System.Windows.Forms.Button btnAddSubmittalFile;
        private System.Windows.Forms.Label lblHistory;   
        private void InitializeSubmittalsControls()
        {
            const int ROW_SPACING = 35;
            const int i = 3;
            this.lstSubmittalFileList = new ListBox();
            this.btnAddSubmittalFile = new Button();
            this.lblHistory = new Label();
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING + lblIDDisplay[i].Height + ROW_SPACING);

            // Properties

            btnAdvance[i].Text = "Approved";

            // lstSubmittalFileList
            lstSubmittalFileList.Size = new System.Drawing.Size(spImagePropSplit[i].Panel1.Height - btnAddMaterialsImg.Height - (IMG_PANEL_PADDING * 2), spImagePropSplit[i].Panel1.Width);
            lstSubmittalFileList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            lstSubmittalFileList.Width = spImagePropSplit[i].Width;
            lstSubmittalFileList.Height = IMAGE_PROPERTIES_SPLITTER_DISTANCE - btnAddSubmittalFile.Height - (PANEL_PADDING*2);

            // btnAddSubmittalFile
            btnAddSubmittalFile.Text = "Add Submittal File";
            btnAddSubmittalFile.Anchor = AnchorStyles.Bottom;

            // lblHistory
            lblHistory.Font = fntID;
            lblHistory.Text = "Submittal History";
            lblHistory.AutoSize = true;

            // Locate everything

            lblHistory.Location = pntCurrentLocation;
            pntCurrentLocation.X = (spImagePropSplit[i].Width - btnAddSubmittalFile.Width) / 2;
            pntCurrentLocation.Y = IMAGE_PROPERTIES_SPLITTER_DISTANCE - btnAddSubmittalFile.Height - PANEL_PADDING;
            btnAddSubmittalFile.Location = pntCurrentLocation;
            pntCurrentLocation.X = pntCurrentLocation.Y = 0;
            lstSubmittalFileList.Location = pntCurrentLocation;

            // Put it on the form
            spMainPanel2Split[i].Panel1.Controls.Add(lblHistory);
            spImagePropSplit[i].Panel1.Controls.Add(lstSubmittalFileList);
            spImagePropSplit[i].Panel1.Controls.Add(btnAddSubmittalFile);

        }
    }
}
 
