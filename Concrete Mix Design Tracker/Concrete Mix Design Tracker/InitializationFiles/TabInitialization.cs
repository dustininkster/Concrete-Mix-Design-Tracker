using System;
using System.Windows.Forms;


namespace Concrete_Mix_Design_Tracker
{
    public partial class MainForm 
   {        
        private MaterialSelect materialSelect;
        /// <summary>
        /// Declarations of global variables/constants
        /// </summary>
        const int MAIN_SPLITTER_DISTANCE = 250;
        const int MAIN_PANEL2_SPLITTER_DISTANCE = 400;
        const int MAIN_PANEL1_SPLITTER_DISTANCE = 25;
        const int IMAGE_PROPERTIES_SPLITTER_DISTANCE = 200;
        protected const int PANEL_PADDING = 15;
        const int IMG_PANEL_PADDING = 3;
        const int IMG_SELECT_BUTTON_SIZE = 20;
        System.Drawing.Font fntID = new System.Drawing.Font("Times New Roman", 25, System.Drawing.FontStyle.Regular);
        System.Drawing.Font fntSectionHeader = new System.Drawing.Font("Times New Roman", 15, System.Drawing.FontStyle.Regular);


         /// <summary>
        /// Declarations for controls to add
        /// </summary>
        protected System.Windows.Forms.TabControl tbSelectionTabs;
        protected System.Windows.Forms.TabPage tbMaterials;
        protected System.Windows.Forms.TabPage tbPrototypes;
        protected System.Windows.Forms.TabPage tbTrialBatches;
        protected System.Windows.Forms.TabPage tbSubmittals;
        protected System.Windows.Forms.TabPage tbMixDesigns;
        private System.Windows.Forms.SplitContainer[] spMainSplit;
        private System.Windows.Forms.SplitContainer[] spMainPanel2Split;
        private System.Windows.Forms.SplitContainer[] spImagePropSplit;
        private System.Windows.Forms.SplitContainer[] spMainPanel1Split;
        protected System.Windows.Forms.ComboBox[] cbFilterSelect;
        protected System.Windows.Forms.ListBox[] lstItemSelect;
        protected System.Windows.Forms.DataGrid[] dgProperties;
        private System.Windows.Forms.Label[] lblIDDisplay;
        protected System.Windows.Forms.Button[] btnEditSave;
        protected System.Windows.Forms.Button[] btnAdvance;
        protected System.EventHandler buttonEventHandler;
        protected System.EventHandler comboEventHandler;
        protected System.EventHandler listboxEventHandler;

        /// <summary>
        /// Initializes the tabs and all of the controls that are common to all of the tabs
        /// We create an array for each control with each element corresponding to the index
        /// of the tab.
        /// </summary>
        private void InitializeTabs()
        {
            // Here we initialize all of the tabs and the main tab control
            this.tbSelectionTabs = new System.Windows.Forms.TabControl();
            this.tbMaterials = new System.Windows.Forms.TabPage();
            this.tbPrototypes = new System.Windows.Forms.TabPage();
            this.tbTrialBatches = new System.Windows.Forms.TabPage();
            this.tbSubmittals = new System.Windows.Forms.TabPage();
            this.tbMixDesigns = new System.Windows.Forms.TabPage();

            // Set the labels on each of the tabs
            tbMaterials.Text = "Materials";
            tbPrototypes.Text = "Prototypes";
            tbTrialBatches.Text = "Trial Batches";
            tbSubmittals.Text = "Submittals";            
            tbMixDesigns.Text = "Mix Designs";

            // Set the properties of the main
            tbSelectionTabs.SelectedIndex = 0;
            tbSelectionTabs.TabIndex = 0;
            tbSelectionTabs.Dock = DockStyle.Fill;                              // Sets the tabs to fill the entire form

            // Now that everything is set up add the main tab control to the form, then each tab
            this.Controls.Add(this.tbSelectionTabs);    
            tbSelectionTabs.Controls.Add(this.tbMaterials);
            tbSelectionTabs.Controls.Add(this.tbPrototypes);
            tbSelectionTabs.Controls.Add(this.tbTrialBatches);
            tbSelectionTabs.Controls.Add(this.tbSubmittals);
            tbSelectionTabs.Controls.Add(this.tbMixDesigns);

            // Set the properties of the tabs
            tbMaterials.Size = this.tbSelectionTabs.Size;
            tbPrototypes.Size = this.tbSelectionTabs.Size;
            tbTrialBatches.Size = this.tbSelectionTabs.Size;
            tbSubmittals.Size = this.tbSelectionTabs.Size;
            tbMixDesigns.Size = this.tbSelectionTabs.Size;


            // Here we initialize all of the controls that are constant for each tab
            // Each control will actually be an array with each element appearing on each tab
            this.spMainSplit = new System.Windows.Forms.SplitContainer[tbSelectionTabs.TabCount];
            this.spImagePropSplit = new System.Windows.Forms.SplitContainer[tbSelectionTabs.TabCount];
            this.spMainPanel1Split = new System.Windows.Forms.SplitContainer[tbSelectionTabs.TabCount];
            this.spMainPanel2Split = new System.Windows.Forms.SplitContainer[tbSelectionTabs.TabCount];
            this.cbFilterSelect = new System.Windows.Forms.ComboBox[tbSelectionTabs.TabCount];
            this.lstItemSelect = new System.Windows.Forms.ListBox[tbSelectionTabs.TabCount];
            this.dgProperties = new System.Windows.Forms.DataGrid[tbSelectionTabs.TabCount];
            this.lblIDDisplay = new System.Windows.Forms.Label[tbSelectionTabs.TabCount];
            this.btnEditSave = new System.Windows.Forms.Button[tbSelectionTabs.TabCount];
            this.btnAdvance = new System.Windows.Forms.Button[tbSelectionTabs.TabCount];

            // Here we initialize the common event handlers
            buttonEventHandler = new EventHandler(this.Button_Click);
            comboEventHandler = new EventHandler(this.Combo_SelectedIndexChanged);
            listboxEventHandler = new EventHandler(this.Listbox_SelectedIndexChanged);
            

            for(int i = 0; i < tbSelectionTabs.TabCount; i++)
            {
                // Instantiate the element in the array
                spMainSplit[i] = new SplitContainer();
                spMainPanel1Split[i] = new SplitContainer();
                spImagePropSplit[i] = new SplitContainer();
                spMainPanel2Split[i] = new SplitContainer();
                cbFilterSelect[i] = new ComboBox();
                lstItemSelect[i] = new ListBox();
                dgProperties[i] = new DataGrid();
                lblIDDisplay[i] = new Label();
                btnEditSave[i] = new Button();
                btnAdvance[i] = new Button();

                // Set properties for each element
                // Main split container divider
                spMainSplit[i].Dock = DockStyle.Fill;
                spMainSplit[i].IsSplitterFixed = true;
                spMainSplit[i].TabStop = false;
                spMainSplit[i].Orientation = Orientation.Vertical;
                spMainSplit[i].BorderStyle = BorderStyle.Fixed3D;/**/
                spMainSplit[i].FixedPanel = FixedPanel.Panel1;

                // Split container between the Main panel and the image/properties panels
                spMainPanel2Split[i].MinimumSize = new System.Drawing.Size(10, 10);
                spMainPanel2Split[i].Panel1MinSize = 35;
                spMainPanel2Split[i].Panel2MinSize = 5;
                spMainPanel2Split[i].TabStop = false;
                spMainPanel2Split[i].Orientation = Orientation.Vertical;
                spMainPanel2Split[i].BorderStyle = BorderStyle.Fixed3D;
                spMainPanel2Split[i].Dock = DockStyle.Fill;

                // Split container between image panel and properties panel
                spImagePropSplit[i].Panel1MinSize = 10;
                spImagePropSplit[i].Panel2MinSize = 5;
                spImagePropSplit[i].TabStop = false;
                spImagePropSplit[i].Orientation = Orientation.Horizontal;
                spImagePropSplit[i].BorderStyle = BorderStyle.Fixed3D;
                spImagePropSplit[i].Dock = DockStyle.Fill;

                // Combobox for narrowing choices
                cbFilterSelect[i].TabIndex = 1;
                cbFilterSelect[i].Height = 5;
                cbFilterSelect[i].Dock = DockStyle.Top;
                cbFilterSelect[i].SelectedIndexChanged += comboEventHandler;

                // List box for selecting the appropriate item
                lstItemSelect[i].TabIndex = 2;
                lstItemSelect[i].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
                lstItemSelect[i].SelectedIndexChanged += listboxEventHandler;

                // Datagrid for properties
                dgProperties[i].TabIndex = 3;
                dgProperties[i].Dock = DockStyle.Fill;

                // Label for ID
                lblIDDisplay[i].Location = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING);
                lblIDDisplay[i].Size = new System.Drawing.Size(300, 42);
                lblIDDisplay[i].Font = fntID;
                lblIDDisplay[i].Text = "No ID Selected";

                // Split container between combobox and listview
                spMainPanel1Split[i].IsSplitterFixed = true;
                spMainPanel1Split[i].FixedPanel = FixedPanel.Panel1;
                spMainPanel1Split[i].TabStop = false;
                spMainPanel1Split[i].Orientation = Orientation.Horizontal;
                spMainPanel1Split[i].BorderStyle = BorderStyle.Fixed3D;
                spMainPanel1Split[i].Dock = DockStyle.Fill;

                // Edit Save button
                btnEditSave[i].Text = "Edit";
                btnEditSave[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                btnEditSave[i].Click += buttonEventHandler;

                // Advance button
                btnAdvance[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                btnAdvance[i].Click += buttonEventHandler;

                // Add elements
                tbSelectionTabs.TabPages[i].Controls.Add(this.spMainSplit[i]);
                spMainSplit[i].Panel2.Controls.Add(this.spMainPanel2Split[i]);
                spMainPanel2Split[i].Panel2.Controls.Add(this.spImagePropSplit[i]);
                spMainSplit[i].Panel1.Controls.Add(this.spMainPanel1Split[i]);
                spMainPanel1Split[i].Panel1.Controls.Add(this.cbFilterSelect[i]);
                spMainPanel1Split[i].Panel2.Controls.Add(this.lstItemSelect[i]);
                spImagePropSplit[i].Panel2.Controls.Add(this.dgProperties[i]);
                spMainPanel2Split[i].Panel1.Controls.Add(this.lblIDDisplay[i]);
                spMainPanel2Split[i].Panel1.Controls.Add(this.btnEditSave[i]);
                spMainPanel2Split[i].Panel1.Controls.Add(this.btnAdvance[i]);

                /*Unfortunately there is an odd quirk with controls that are Dockstyle.Fill within a
                splitter panel.  As a result I have to change the splitter distance after the controls
                are added to the form.  This has the disadvantage of showing the redraw for a split second
                But is was the best option I could come up with.  Otherwise it would have made some odd 
                assumptions about the drawing space and we'd have a very tiny pane on the left.*/
                //TODO: Get rid of the magic numbers and replace with constants.

                spMainSplit[i].SplitterDistance = MAIN_SPLITTER_DISTANCE;
                spMainPanel1Split[i].SplitterDistance = MAIN_PANEL1_SPLITTER_DISTANCE;
                spMainPanel2Split[i].SplitterDistance = MAIN_PANEL2_SPLITTER_DISTANCE;
                btnEditSave[i].Location = new System.Drawing.Point(PANEL_PADDING, spMainPanel2Split[i].Panel1.Height - btnEditSave[i].Height - PANEL_PADDING);
                btnAdvance[i].Location = new System.Drawing.Point((PANEL_PADDING*2) + btnEditSave[i].Width, spMainPanel2Split[i].Panel1.Height - btnAdvance[i].Height - PANEL_PADDING);
                lstItemSelect[i].Size = new System.Drawing.Size(spMainPanel1Split[i].Panel2.Width, spMainPanel1Split[i].Panel2.Height);
                spImagePropSplit[i].SplitterDistance = IMAGE_PROPERTIES_SPLITTER_DISTANCE;
            }


        }
    }
}