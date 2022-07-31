using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class MainForm
    {
  
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.SplitContainer spTrial;
        protected System.Windows.Forms.TextBox txtNotes;
        protected System.Windows.Forms.RichTextBox rchTBPrototypeSummary;
        private void InitializeTrialBatchesControls()
        {
            const int ROW_SPACING = 35;
            const int i = 2;
            this.rchTBPrototypeSummary = new RichTextBox();
            this.lblNotes = new Label();
            this.txtNotes = new TextBox();
            this.spTrial = new SplitContainer();
            System.Drawing.Point pntCurrentLocation = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING + lblIDDisplay[i].Height + ROW_SPACING);
            const int normalFontSize = 10;

            //Properties

            btnAdvance[i].Text = "Approved";

            spTrial.Dock = DockStyle.Fill;
            spTrial.TabStop = false;
            spTrial.Orientation = Orientation.Horizontal;
            spTrial.BorderStyle = BorderStyle.Fixed3D;

            rchTBPrototypeSummary.Width = (int)(MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING * 2));
            rchTBPrototypeSummary.Height = 200 - lblName.Height -(ROW_SPACING*2);
            rchTBPrototypeSummary.Anchor = (AnchorStyles.Left |  AnchorStyles.Right | AnchorStyles.Top|AnchorStyles.Bottom);
            rchTBPrototypeSummary.ReadOnly = true;
            rchTBPrototypeSummary.Font = normalFont;
            rchTBPrototypeSummary.WordWrap = false;


            lblNotes.Text = "Notes";
            lblNotes.Font = fntSectionHeader;
            lblNotes.Anchor = (AnchorStyles.Left|AnchorStyles.Top);

            txtNotes.Multiline = true;
            txtNotes.Width = (int)(MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING * 2));
            txtNotes.Height = 300 - lblNotes.Height - btnEditSave[i].Height - (ROW_SPACING*4);
            txtNotes.Anchor = (AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right|AnchorStyles.Top);
            txtNotes.Enabled = false;


            // Locate everything

            rchTBPrototypeSummary.Location = pntCurrentLocation;
            pntCurrentLocation.Y = 0;
            pntCurrentLocation.X = PANEL_PADDING;
            lblNotes.Location = pntCurrentLocation;
            pntCurrentLocation.Y += ROW_SPACING;
            txtNotes.Location = pntCurrentLocation;
            /*txtNotes.Size = new System.Drawing.Size(
                MAIN_PANEL2_SPLITTER_DISTANCE - (PANEL_PADDING * 2),
                spMainPanel2Split[i].Height - pntCurrentLocation.Y - btnEditSave[i].Height - (PANEL_PADDING*2));
            */

            spMainPanel2Split[i].Panel1.Controls.Add(spTrial);
            spTrial.Panel1.Controls.Add(rchTBPrototypeSummary);
            spTrial.Panel2.Controls.Add(lblNotes);
            spTrial.Panel2.Controls.Add(txtNotes);
        }

    }
}
 