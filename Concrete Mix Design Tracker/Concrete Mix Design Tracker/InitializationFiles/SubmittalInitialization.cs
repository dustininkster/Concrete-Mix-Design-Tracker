using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class MainForm
    {
        private System.Windows.Forms.Label lblHistory;   
       private void InitializeSubmittalsControls()
       {
            const int i = 3;
            this.lblHistory = new Label();
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING + lblIDDisplay[i].Height + ROW_SPACING);

            // Properties

            btnAdvance[i].Text = "Approved";


            // btnAddSubmittalFile

            // lblHistory
            lblHistory.Font = fntID;
            lblHistory.Text = "Submittal History";
            lblHistory.AutoSize = true;

            // Locate everything

            lblHistory.Location = pntCurrentLocation;

            // Put it on the form
            spMainPanel2Split[i].Panel1.Controls.Add(lblHistory);

    }
}
}
 
