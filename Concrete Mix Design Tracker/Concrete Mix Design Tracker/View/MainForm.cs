using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engineering_Database_View
{
    public partial class MainForm : Form
    {
        // Declarations of global variables/constants
        const int MAIN_SPLITTER_DISTANCE = 250;
        const int MAIN_PANEL2_SPLITTER_DISTANCE = 400;
        const int MAIN_PANEL1_SPLITTER_DISTANCE = 25;
        const int DETAILS_PROPERTIES_SPLITTER_DISTANCE = 200;
        const int ROW_SPACING = 33;
        const int PANEL_PADDING = 15;
        const int FILTER_STRING_LENGTH = 5;

        // Declarations/ Initializations of Fonts used
        System.Drawing.Font fntID = new System.Drawing.Font("Courier Sans", 25, System.Drawing.FontStyle.Regular);
        System.Drawing.Font fntSectionHeader = new System.Drawing.Font("Courier Sans", 15, System.Drawing.FontStyle.Regular);
        System.Drawing.Font normalFont = new System.Drawing.Font("Courier New", 8, System.Drawing.FontStyle.Regular);

        // Declarations/ Initializations of Arrays/Lists of controls
        // If more than one of the same type of list is used it's stored in an array
        System.Windows.Forms.TabControl tbSelectionTabs;
        List<SplitContainer>[] splitContainers = new List<SplitContainer>[4];
        List<TabPage> tabPages = new List<TabPage>();
        List<ComboBox> comboBoxes = new List<ComboBox>();
        List<ListBox> listBoxes = new List<ListBox>();
        List<DataGridView>[] dataGridViews = new List<DataGridView>[2];
        List<Label> labels = new List<Label>();

        /// <summary>
        /// Constructor for the MainForm.
        /// </summary>
        /// <param name="numTabsToInit">Number of Tabs to start with</param>
        public MainForm()
        {
            //Auto generated form initialization from the form editor
            InitializeComponent();

            //Initialize basic properties of tab control
            tbSelectionTabs = new System.Windows.Forms.TabControl();
            tbSelectionTabs.TabIndex = 0;
            tbSelectionTabs.Dock = DockStyle.Fill;                              // Sets the tabs to fill the entire form
            Controls.Add(this.tbSelectionTabs);

            //Initialize array items for split containers and datagridviews
            splitContainers[0] = new List<SplitContainer>();
            splitContainers[1] = new List<SplitContainer>();
            splitContainers[2] = new List<SplitContainer>();
            splitContainers[3] = new List<SplitContainer>();
            dataGridViews[0] = new List<DataGridView>();
            dataGridViews[1] = new List<DataGridView>();


            // Some code to test tab creation
            /*
            for (int x = 0; x < 20; x++)
                AddNewTab();
            */
        }

        /// <summary>
        /// Method creates a new tab to the main form and adds the common controls
        /// </summary>
        public void AddNewTab()
        {
            const int MAIN_SPLIT = 0;
            const int MAIN_PANEL_1_SPLIT = 1;
            const int MAIN_PANEL_2_SPLIT = 2;
            const int PROP_SPLIT = 3;
            const int TOP_PROPERTIES = 0;
            const int BOTTOM_PROPERTIES = 1;

            tabPages.Add(new TabPage());
            tbSelectionTabs.Controls.Add(tabPages.Last());
            tabPages.Last().Size = tbSelectionTabs.Size;
            tabPages.Last().Text = "Tab " + tbSelectionTabs.TabPages.Count;

            // Instantiate the element in the array
            splitContainers[0].Add(new SplitContainer());
            splitContainers[1].Add(new SplitContainer());
            splitContainers[2].Add(new SplitContainer());
            splitContainers[3].Add(new SplitContainer());

            comboBoxes.Add(new ComboBox());
            listBoxes.Add(new ListBox());
            dataGridViews[0].Add(new DataGridView());
            dataGridViews[1].Add(new DataGridView());
            labels.Add(new Label());

            // Set properties for each element
            // Main split container divider
            splitContainers[MAIN_SPLIT].Last().Dock = DockStyle.Fill;
            splitContainers[MAIN_SPLIT].Last().IsSplitterFixed = true;
            splitContainers[MAIN_SPLIT].Last().TabStop = false;
            splitContainers[MAIN_SPLIT].Last().Orientation = Orientation.Vertical;
            splitContainers[MAIN_SPLIT].Last().BorderStyle = BorderStyle.Fixed3D;/**/
            splitContainers[MAIN_SPLIT].Last().FixedPanel = FixedPanel.Panel1;

            // Split container between combobox and listview
            splitContainers[MAIN_PANEL_1_SPLIT].Last().IsSplitterFixed = true;
            splitContainers[MAIN_PANEL_1_SPLIT].Last().FixedPanel = FixedPanel.Panel1;
            splitContainers[MAIN_PANEL_1_SPLIT].Last().TabStop = false;
            splitContainers[MAIN_PANEL_1_SPLIT].Last().Orientation = Orientation.Horizontal;
            splitContainers[MAIN_PANEL_1_SPLIT].Last().BorderStyle = BorderStyle.Fixed3D;
            splitContainers[MAIN_PANEL_1_SPLIT].Last().Dock = DockStyle.Fill;

            // Split container between the Main panel and the image/properties panels
            splitContainers[MAIN_PANEL_2_SPLIT].Last().MinimumSize = new System.Drawing.Size(10, 10);
            splitContainers[MAIN_PANEL_2_SPLIT].Last().Panel1MinSize = 35;
            splitContainers[MAIN_PANEL_2_SPLIT].Last().Panel2MinSize = 5;
            splitContainers[MAIN_PANEL_2_SPLIT].Last().TabStop = false;
            splitContainers[MAIN_PANEL_2_SPLIT].Last().Orientation = Orientation.Vertical;
            splitContainers[MAIN_PANEL_2_SPLIT].Last().BorderStyle = BorderStyle.Fixed3D;
            splitContainers[MAIN_PANEL_2_SPLIT].Last().Dock = DockStyle.Fill;

            // Split container between image panel and properties panel
            splitContainers[PROP_SPLIT].Last().Panel1MinSize = 10;
            splitContainers[PROP_SPLIT].Last().Panel2MinSize = 5;
            splitContainers[PROP_SPLIT].Last().TabStop = false;
            splitContainers[PROP_SPLIT].Last().Orientation = Orientation.Horizontal;
            splitContainers[PROP_SPLIT].Last().BorderStyle = BorderStyle.Fixed3D;
            splitContainers[PROP_SPLIT].Last().Dock = DockStyle.Fill;

            // Combobox for narrowing choices
            comboBoxes.Last().TabIndex = 1;
            comboBoxes.Last().Height = 5;
            comboBoxes.Last().Dock = DockStyle.Top;

            // List box for selecting the appropriate item
            listBoxes.Last().TabIndex = 2;
            listBoxes.Last().Font = normalFont;
            listBoxes.Last().Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

            // Datagrid for properties
            dataGridViews[BOTTOM_PROPERTIES].Last().TabIndex = 3;
            dataGridViews[BOTTOM_PROPERTIES].Last().Dock = DockStyle.Fill;
            dataGridViews[BOTTOM_PROPERTIES].Last().ColumnCount = 2;
            dataGridViews[BOTTOM_PROPERTIES].Last().Columns[0].Name = "Item";
            dataGridViews[BOTTOM_PROPERTIES].Last().Columns[1].Name = "Value";
            dataGridViews[BOTTOM_PROPERTIES].Last().SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridViews[BOTTOM_PROPERTIES].Last().AllowUserToAddRows = false;
            dataGridViews[BOTTOM_PROPERTIES].Last().AllowUserToDeleteRows = false;
            dataGridViews[BOTTOM_PROPERTIES].Last().AllowUserToOrderColumns = false;
            dataGridViews[BOTTOM_PROPERTIES].Last().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViews[BOTTOM_PROPERTIES].Last().RowHeadersVisible = false;
            dataGridViews[BOTTOM_PROPERTIES].Last().Columns[0].ReadOnly = true;
            dataGridViews[BOTTOM_PROPERTIES].Last().Enabled = false;

            // Static Datagrid for static properties
            dataGridViews[TOP_PROPERTIES].Last().TabStop = false;
            dataGridViews[TOP_PROPERTIES].Last().Dock = DockStyle.Fill;
            dataGridViews[TOP_PROPERTIES].Last().ColumnCount = 2;
            dataGridViews[TOP_PROPERTIES].Last().Columns[0].Name = "Item";
            dataGridViews[TOP_PROPERTIES].Last().Columns[1].Name = "Value";
            dataGridViews[TOP_PROPERTIES].Last().AllowUserToAddRows = false;
            dataGridViews[TOP_PROPERTIES].Last().AllowUserToDeleteRows = false;
            dataGridViews[TOP_PROPERTIES].Last().AllowUserToOrderColumns = false;
            dataGridViews[TOP_PROPERTIES].Last().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViews[TOP_PROPERTIES].Last().RowHeadersVisible = false;
            dataGridViews[TOP_PROPERTIES].Last().ReadOnly = true;


            // Label for ID
            labels.Last().Location = new System.Drawing.Point(PANEL_PADDING, PANEL_PADDING);
            labels.Last().Size = new System.Drawing.Size(900, 42);
            labels.Last().Font = fntID;
            labels.Last().Text = "No ID Selected";

            // Add elements
            tbSelectionTabs.TabPages[tbSelectionTabs.TabPages.Count-1].Controls.Add(splitContainers[MAIN_SPLIT].Last());
            splitContainers[MAIN_SPLIT].Last().Panel2.Controls.Add(splitContainers[MAIN_PANEL_2_SPLIT].Last());
            splitContainers[MAIN_PANEL_2_SPLIT].Last().Panel2.Controls.Add(splitContainers[PROP_SPLIT].Last());
            splitContainers[MAIN_SPLIT].Last().Panel1.Controls.Add(splitContainers[MAIN_PANEL_1_SPLIT].Last());
            splitContainers[MAIN_PANEL_1_SPLIT].Last().Panel1.Controls.Add(comboBoxes.Last());
            splitContainers[MAIN_PANEL_1_SPLIT].Last().Panel2.Controls.Add(listBoxes.Last());
            splitContainers[PROP_SPLIT].Last().Panel2.Controls.Add(dataGridViews[BOTTOM_PROPERTIES].Last());
            splitContainers[PROP_SPLIT].Last().Panel1.Controls.Add(dataGridViews[TOP_PROPERTIES].Last());
            splitContainers[MAIN_PANEL_2_SPLIT].Last().Panel1.Controls.Add(labels.Last());

            /*Unfortunately there is an odd quirk with controls that are Dockstyle.Fill within a
            splitter panel.  As a result I have to change the splitter distance after the controls
            are added to the form.  This has the disadvantage of showing the redraw for a split second
            But is was the best option I could come up with.  Otherwise it would have made some odd 
            assumptions about the drawing space and we'd have a very tiny pane on the left.*/

            splitContainers[MAIN_SPLIT].Last().SplitterDistance = MAIN_SPLITTER_DISTANCE;
            splitContainers[MAIN_PANEL_1_SPLIT].Last().SplitterDistance = MAIN_PANEL1_SPLITTER_DISTANCE;
            splitContainers[MAIN_PANEL_2_SPLIT].Last().SplitterDistance = MAIN_PANEL2_SPLITTER_DISTANCE;
            listBoxes.Last().Size = new Size(splitContainers[MAIN_PANEL_1_SPLIT].Last().Panel2.Width, splitContainers[MAIN_PANEL_2_SPLIT].Last().Panel2.Height);
            splitContainers[PROP_SPLIT].Last().SplitterDistance = DETAILS_PROPERTIES_SPLITTER_DISTANCE;

        }
        public void UpdateSelections(int tabIndex, string[] values)
        {
            if (tabIndex < tbSelectionTabs.TabCount)
            {
                // Clear items from selection listbox and repopulate with the values sent
                listBoxes[tabIndex].Items.Clear();
                foreach (string value in values)
                {
                    listBoxes[tabIndex].Items.Add(value);
                    comboBoxes[tabIndex].Items.Add(value.Substring(0, 5));
                }
            }
            else
                MessageBox.Show("UpdateListBox method called with a tab that does not exist!",
                    "Invalid Tab Reference", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        public void UpdatePropertiesGrid(int tabIndex, int windowIndex, Dictionary<string,string> propertiesDict)
        {
            //here you'll need to determine which grid in the array to add to then just go through the dictionary

        }
        /*
        public void AddEditableField(int tabIndex) //<- This may need to be a template
        public string[] GetEditableFields(int tabIndex)
        public void AddFixedField(int tabIndex) //<- This too
        public void AddNotesField(int tabIndex)
        public string[] GetNotesFields(int tabIndex)
        public void AddDisplayField(int tabIndex)
        public void AddButton(int tabIndex)
        public void AddSelectionBox(int tabIndex)
        public void Print()
         */
    }
}