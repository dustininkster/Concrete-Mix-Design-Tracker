using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class MainForm
    {
        private System.Windows.Forms.ListBox lstMixFileList;
        private System.Windows.Forms.RichTextBox rchMixPrototypeSummary;

         private void InitializeMixDesignsControls()
        {
            const int ROW_SPACING = 35;
            const int i = 4;
            this.lstMixFileList = new ListBox();
            this.rchMixPrototypeSummary = new RichTextBox();
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(
                PANEL_PADDING,
                PANEL_PADDING+ lblIDDisplay[i].Height + ROW_SPACING);

            // properties

            btnAdvance[i].Visible = false;
            btnEditSave[i].Visible = false;

            lstMixFileList.Width = spImagePropSplit[i].Panel1.Width;
            lstMixFileList.Height = IMAGE_PROPERTIES_SPLITTER_DISTANCE;
            lstMixFileList.Anchor =
                (AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right);

            rchMixPrototypeSummary.Width = MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING*2);
            rchMixPrototypeSummary.Height = spMainPanel2Split[i].Height - pntCurrentLocation.Y -
                PANEL_PADDING;
            rchMixPrototypeSummary.Anchor =
                (AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right);

            // Locate everything
            rchMixPrototypeSummary.Location = pntCurrentLocation;
            pntCurrentLocation.X = pntCurrentLocation.Y = 0;
            lstMixFileList.Location = pntCurrentLocation;

            // Put everything on the Mix Design form
            spImagePropSplit[i].Panel1.Controls.Add(lstMixFileList);
            spMainPanel2Split[i].Panel1.Controls.Add(rchMixPrototypeSummary);
         }
    }
}
 