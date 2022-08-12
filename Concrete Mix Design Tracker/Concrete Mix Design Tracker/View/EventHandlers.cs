using System;
using System.Windows.Forms;


namespace Engineering_Database_View
{
    public partial class MainForm
    {
        const int
            MATERIALS = 0,
            PROTOTYPES = 1,
            TRIAL_BATCHES = 2,
            SUBMITTALS = 3,
            MIX_DESIGNS = 4;

        /************************************************
         * This section has all of the event handlers   *
         ************************************************/
/*
        private void Button_Click(object sender, System.EventArgs e)
        {
            /*
            Button buttonClicked = (Button)sender;
            byte tab;
            byte id;
            switch (buttonClicked.Text)
            {
                case "Edit":
                    buttonClicked.Text = "Save";
                    tab = (byte)Array.IndexOf(btnEditSave, buttonClicked);
                    Controller.Edit(tab);
                    break;
                case "Save":
                    buttonClicked.Text = "Edit";
                    tab = (byte)Array.IndexOf(btnEditSave, buttonClicked);
                    id = byte.Parse(lstItemSelect[tab].Text.Substring(0, 3));
                    Controller.Save(tab, id);
                    break;
                case "Copy":
                    Controller.CopyPrototype();
                    break;
                case ">":
                    if (buttonClicked.Name == "btnMaterialsImgRight")
                        Controller.NextImage(MATERIALS);
                    else if (buttonClicked.Name == "btnTrialBatchImgRight")
                        Controller.NextImage(TRIAL_BATCHES);
                    break;
                case "<":
                    if (buttonClicked.Name == "btnMaterialsImgLeft")
                        Controller.PreviousImage(MATERIALS);
                    else if (buttonClicked.Name == "btnTrialBatchImgLeft")
                        Controller.PreviousImage(TRIAL_BATCHES);
                    break;
                case "Add Image":
                    if (buttonClicked.Name == "btnAddMaterialsImg")
                        Controller.NewImage(MATERIALS);
                    if (buttonClicked.Name == "btnAddTrialBatchImg")
                        Controller.NewImage(TRIAL_BATCHES);
                    break;
                case "Add":
                    Controller.AddFile();
                    break;
                case "Add Material":
                    Controller.NewMaterialSelect();
                    break;
                case "Accept Type":
                    Controller.NewMaterial();
                    break;
                case "Add To":
                    tab = (byte)Array.IndexOf(btnAdvance, buttonClicked);
                    Controller.Advance(tab);
                    break;
            }    

        }

        private void Combo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*
            byte index = (byte)Array.IndexOf(cbFilterSelect, (ComboBox)sender);
            Controller.FilterSelectionChanged(index, cbFilterSelect[index].Text);

            if (lstItemSelect[index].Items.Count > 0)
                lstItemSelect[index].SelectedIndex = 0;
        }

        private void Listbox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*
            ListBox lb = (ListBox)sender;
            if (lb.Text == "")
                return;

            var index = Array.IndexOf(lstItemSelect, (ListBox)sender);
            
            switch (index)
            {
                case MATERIALS:
                    Controller.MaterialSelectionChanged(lstItemSelect[index].Text);
                    break;
                case PROTOTYPES:
                    Controller.PrototypeSelectionChanged(lstItemSelect[index].Text);
                    break;
                case TRIAL_BATCHES:
                    Controller.TrialBatchSelectionChanged(lstItemSelect[index].Text);
                    break;
                case SUBMITTALS:
<<<<<<< HEAD:Concrete Mix Design Tracker/Concrete Mix Design Tracker/View/EventHandlers.cs
                    /*Controller.SubmittalSelectionChanged(lstItemSelect[index].Text);/*
=======
                    Controller.SubmittalSelectionChanged(lstItemSelect[index].Text);
>>>>>>> parent of eec7c06 (This is the final form of my capstone project.):Concrete Mix Design Tracker/Concrete Mix Design Tracker/EventHandlers.cs
                    break;
                case MIX_DESIGNS:
                    Controller.MixDesignSelectionChanged(lstItemSelect[index].Text);
                    break;
            }

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Controller.LoadFilterSelect((byte)tbSelectionTabs.TabCount);
            for (byte i = 0; i < tbSelectionTabs.TabCount; i++)
                cbFilterSelect[i].SelectedIndex = 0;
        }

        private void dgProperties_CellChanged(object sender, System.EventArgs e)
        {

        }

        private void Material_WeightChanged(object sender, System.EventArgs e)
        {
            TextBox textChanged = (TextBox)sender;
            //byte i = (byte)Array.IndexOf(;
        }
        private void Material_VolumeChanged(object sender, System.EventArgs e)
        {

        }
        private void Admixture_CWTChanged(object sender, System.EventArgs e)
        {

        }
        private void Admixture_YDChanged(object sender, System.EventArgs e)
        {

        }
        private void lstAddToPrototypeDoubleClick(object sender, System.EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.Text == "")
                return;
            Controller.PrototypeSelectedForMaterialAdd(lstAddMaterialToPrototype.Text);
        }
<<<<<<< HEAD:Concrete Mix Design Tracker/Concrete Mix Design Tracker/View/EventHandlers.cs
        private void lstConcreteClassSelected(object sender, System.EventArgs e)
        {
            ListBox lst = (ListBox)sender;
            Controller.ClassSelectedForPrototypeAdd(lst.Text);
        }
*/
=======
>>>>>>> parent of eec7c06 (This is the final form of my capstone project.):Concrete Mix Design Tracker/Concrete Mix Design Tracker/EventHandlers.cs
    }
}
