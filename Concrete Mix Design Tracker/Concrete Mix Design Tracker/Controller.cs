using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete_Mix_Design_Tracker
{
    static class Controller
    {        
        const byte
            MATERIALS = 0,
            PROTOTYPE = 1,
            TRIAL_BATCH = 2,
            SUBMITTAL = 3,
            MIX_DESIGN = 4;

        /// <summary>
        /// Loads the combobox for filtering with a list of types from the database
        /// </summary>
        /// <param name="tabcount">Number of tabs to populate</param>
        public static void LoadFilterSelect(byte tabcount)
        {
            for (byte i = 0; i < tabcount; i++)
            {
                Program.main.DisplayInFilterBox(i, Model.GetFilters(i));
            }

        }
        /// <summary>
        /// Called from the event trigger for a change in selection in the listbox
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="selection"></param>
        public static void FilterSelectionChanged(byte tab, string selection)
        {
            Program.main.DisplayInListBox(tab, Model.GetList(tab, selection));
        }
        public static void MaterialSelectionChanged(string selection)
        {
            byte i = 0;
            byte selectionIndex = ParseSelection(selection);

            Program.main.DisplaySelectionName(i, Model.GetName(i, selectionIndex));
            Program.main.DisplayMaterialInfo(
                Model.GetMaterialName(selectionIndex),
                Model.GetMaterialSource(selectionIndex),
                Model.GetMaterialGrade(selectionIndex),
                Model.GetMaterialRelative_Density(selectionIndex),
                Model.GetMaterialType(selectionIndex)
                );
            Program.main.DisplayProperties(i, Model.GetProperties(i, selectionIndex));
        }

        public static void PrototypeSelectionChanged(string selection)
        {
            byte i = 1;
            byte selectionIndex = ParseSelection(selection);

            Program.main.DisplaySelectionName(i, Model.GetName(i, selectionIndex));
            Program.main.DisplayWaterCMRatio(Model.GetWaterCMRatio(selectionIndex));
            Program.main.ClearPrototypePanel();
            Program.main.DisplayMaterials(
                Model.GetPrototypeCement(selectionIndex),
                Model.GetPrototypeSCM(selectionIndex),
                Model.GetPrototypeCA(selectionIndex),
                Model.GetPrototypeFN(selectionIndex),
                Model.GetPrototypeAdmixture(selectionIndex));
            Program.main.DisplayProperties(i, Model.GetProperties(i, selectionIndex));
        }

        public static void TrialBatchSelectionChanged(string selection)
        {
            byte i = 2;
            byte selectionIndex = ParseSelection(selection);
            Program.main.DisplaySelectionName(i, Model.GetName(i, selectionIndex));
            Program.main.DisplayProperties(i, Model.GetProperties(i, selectionIndex));
        }

        public static void SubmittalSelectionChanged(string selection)
        {
            byte i = 3;
            byte selectionIndex = ParseSelection(selection);
            Program.main.DisplaySelectionName(i, Model.GetName(i, selectionIndex));
            Program.main.DisplayProperties(i, Model.GetProperties(i, selectionIndex));
         }

        public static void MixDesignSelectionChanged(string selection)
        {
            byte i = 4;
            byte selectionIndex = ParseSelection(selection);
            Program.main.DisplaySelectionName(i, Model.GetName(i, selectionIndex));
            Program.main.DisplayProperties(i, Model.GetProperties(i, selectionIndex));
        }

        private static byte ParseSelection(string selection)
        {
            return byte.Parse(selection.Substring(0, 3));
        }

        /// <summary>
        /// Thaw the screen to allow editing
        /// </summary>
        /// <param name="tab"></param>
        public static void Edit(byte tab)
        {
            Program.main.ThawTab(tab);
        }
        public static void Save(byte tab, byte ID)
        {
            Program.main.FreezeTab(tab);
            switch (tab)
            {
                case MATERIALS:
                    Model.UpdateMaterial(ID,
                        Program.main.GetEnteredMaterialVendor(),
                        Program.main.GetEnteredMaterialSource(),
                        Program.main.GetEnteredGrade(),
                        Program.main.GetEnteredRelativeDensity(),
                        Program.main.GetEnteredMaterialType());


                    break;
                case PROTOTYPE:
                    break;
                case TRIAL_BATCH:
                    break;
                case SUBMITTAL:
                    break;
                case MIX_DESIGN:
                    break;
                default:
                    break;
            }
            Program.main.DisplayInListBox(tab, Model.GetList(tab, Program.main.GetFilterSelection(tab)));

        }
        public static void CopyPrototype()
        {

        }
        public static void NextImage(byte tab)
        {

        }
        public static void PreviousImage(byte tab)
        {

        }
        public static void NewImage(byte tab)
        {

        }
        public static void AddFile()
        {

        }

        public static void NewMaterial()
        {
            Edit(MATERIALS);
            Model.CreateNewMaterial(Program.main.GetEnteredMaterialType());
            Program.main.DisplayInListBox(MATERIALS, Model.GetList(MATERIALS));
            Program.main.SelectLastIndex(MATERIALS);
        }
        public static void NewMaterialSelect()
        {
            Program.main.MaterialSelectMode();
        }

        public static void Advance(byte tab)
        {
            switch (tab)
            {
                case MATERIALS:
                    Program.main.DisplayPrototypesInSelectionListBox(Model.GetPrototypesWithoutMaterial(
                        Program.main.GetSelectedMaterialID(),
                        Program.main.GetEnteredMaterialType()
                        ));
                    Program.main.SelectPrototypeToAddMaterialMode();
                    break;
            }
        }

        public static void PrototypeSelectedForMaterialAdd(string selection)
        {
            if (selection == "Create New Prototype")
                Program.main.ClassSelectMode();
            else
                Model.AddMaterialToPrototype(ParseSelection(selection), Program.main.GetSelectedMaterialID());
            Program.main.OpenTab(PROTOTYPE);
            Program.main.ThawTab(MATERIALS);
        }
    }
}
