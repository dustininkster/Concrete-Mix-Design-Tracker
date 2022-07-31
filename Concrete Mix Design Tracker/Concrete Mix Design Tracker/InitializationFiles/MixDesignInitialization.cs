using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class MainForm
    {
        protected System.Windows.Forms.RichTextBox rchMixPrototypeSummary;

         private void InitializeMixDesignsControls()
        {
            const int i = 3;
            this.rchMixPrototypeSummary = new RichTextBox();
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(
                PANEL_PADDING,
                PANEL_PADDING+ lblIDDisplay[i].Height + ROW_SPACING);

            // properties

            btnEditSave[i].Visible = false;
            btnAdvance[i].Text = "Print";

            rchMixPrototypeSummary.Width = MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING*2);
            rchMixPrototypeSummary.Height = spMainPanel2Split[i].Height - pntCurrentLocation.Y -
                btnAdvance[i].Height - (PANEL_PADDING*3);
            rchMixPrototypeSummary.Anchor =
                (AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right);
            rchMixPrototypeSummary.Enabled = false;

            // Locate everything
            rchMixPrototypeSummary.Location = pntCurrentLocation;
            pntCurrentLocation.X = pntCurrentLocation.Y = 0;

            // Put everything on the Mix Design form
            spMainPanel2Split[i].Panel1.Controls.Add(rchMixPrototypeSummary);
         }
    }
}
 