using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engineering_Database_View;

namespace Concrete_Mix_Design_Tracker
{
    static class Controller
    {        
        const byte
            MATERIALS = 0,
            PROTOTYPE = 1,
            TRIAL_BATCH = 2,
            SUBMITTAL = 4,/**/
            MIX_DESIGN = 3;
            /*MIX_DESIGN = 4/**/

        /// <summary>
        /// Loads the combobox for filtering with a list of types from the database
        /// </summary>
        /// <param name="tabcount">Number of tabs to populate</param>
        public static void LoadFilterSelect(byte tabcount)
        {
            for (byte i = 0; i < tabcount; i++)
            {
                Program.main.DisplayInFilterBox(i, DataAccess.GetFilters(i));
            }

        }
        /// <summary>
        /// Called from the event trigger for a change in selection in the listbox
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="selection"></param>
        public static void FilterSelectionChanged(byte tab, string selection)
        {
            Program.main.DisplayInListBox(tab, DataAccess.GetListOfRecords(tab, selection));
        }
        public static void MaterialSelectionChanged(string selection)
        {
            byte i = 0;
            byte selectionIndex = ParseSelection(selection);

            Program.main.DisplaySelectionName(i, DataAccess.GetName(i, selectionIndex));         // Change label to selected object name
            Program.main.DisplayMaterialInfo(                                               // Display basic info in main window
                DataAccess.GetMaterialName(selectionIndex),
                DataAccess.GetMaterialSource(selectionIndex),                                    // Change txtbox to source
                DataAccess.GetMaterialGrade(selectionIndex),                                     // Change txtbox to grade
                DataAccess.GetMaterialRelative_Density(selectionIndex),                          // Change txtbox to density
                DataAccess.GetMaterialType(selectionIndex)                                       // Change txtbox to type
                );
            Program.main.DisplayProperties(i, DataAccess.GetProperties(i, selectionIndex));      // Display properties
        }

        public static void PrototypeSelectionChanged(string selection)
        {
            byte i = 1;
            byte selectionIndex = ParseSelection(selection);

            Program.main.DisplaySelectionName(i, DataAccess.GetName(i, selectionIndex));
            Program.main.DisplayWaterCMRatio(DataAccess.GetWaterCMRatio(selectionIndex));
            Program.main.ClearPrototypePanel();
            Program.main.DisplayMaterials(
                DataAccess.GetPrototypeCement(selectionIndex),
                DataAccess.GetPrototypeSCM(selectionIndex),
                DataAccess.GetPrototypeCA(selectionIndex),
                DataAccess.GetPrototypeFN(selectionIndex),
                DataAccess.GetPrototypeAdmixture(selectionIndex));
            Program.main.DisplayProperties(i, DataAccess.GetProperties(i, selectionIndex));
        }

        public static void TrialBatchSelectionChanged(string selection)
        {
            byte i = 2;                                                                 // tab index
            byte selectionIndex = ParseSelection(selection);                            // index of the selected trial batch
            byte prototypeTab = 1;
            byte associatedPrototype = (byte)DataAccess.GetAssociatedPrototypeID(selectionIndex, i);

            Program.main.DisplaySelectionName(i, DataAccess.GetName(i, selectionIndex));     // set label to name of selection
            Program.main.DisplayProperties(i, DataAccess.GetProperties(i, selectionIndex));  // display properties of selection

            Program.main.DisplaySummary(
                TRIAL_BATCH,
                DataAccess.GetName(prototypeTab, associatedPrototype),
                DataAccess.GetProperties(prototypeTab, associatedPrototype),
                DataAccess.GetPrototypeCement(associatedPrototype),
                DataAccess.GetPrototypeSCM(associatedPrototype),
                DataAccess.GetPrototypeCA(associatedPrototype),
                DataAccess.GetPrototypeFN(associatedPrototype),
                DataAccess.GetPrototypeAdmixture(associatedPrototype),
                DataAccess.GetTotalCementitious(associatedPrototype),
                DataAccess.GetWaterCMRatio(associatedPrototype),
                DataAccess.GetPrototypeWater(associatedPrototype)
                );                                                                      // display trial batch summary
        }

        /*public static void SubmittalSelectionChanged(string selection)
        {
            byte i = 3;
            byte selectionIndex = ParseSelection(selection);
            Program.main.DisplaySelectionName(i, Model.GetName(i, selectionIndex));
            Program.main.DisplayProperties(i, Model.GetProperties(i, selectionIndex));
         }/**/

        public static void MixDesignSelectionChanged(string selection)
        {
            byte i = 3;
            byte selectionIndex = ParseSelection(selection);                            // index of the selected mix design
            byte prototypeTab = 1;
            byte trialBatchTab = 2;
            //byte submittalTab = 3;
            byte associatedPrototype = (byte)DataAccess.GetAssociatedPrototypeID(selectionIndex, i);
            byte associatedTrialBatch = (byte)DataAccess.GetAssociatedTrialBatchID(associatedPrototype);
            /*byte associatedSubmittal = (byte)Model.GetAssociatedSubmittalID(associatedPrototype);/**/

            Program.main.DisplaySelectionName(i, DataAccess.GetName(i, selectionIndex));
            Program.main.DisplayProperties(i, DataAccess.GetProperties(i, selectionIndex));
            Program.main.DisplaySummary(
                MIX_DESIGN,
                DataAccess.GetName(prototypeTab, associatedPrototype),
                DataAccess.GetProperties(prototypeTab, associatedPrototype),
                DataAccess.GetPrototypeCement(associatedPrototype),
                DataAccess.GetPrototypeSCM(associatedPrototype),
                DataAccess.GetPrototypeCA(associatedPrototype),
                DataAccess.GetPrototypeFN(associatedPrototype),
                DataAccess.GetPrototypeAdmixture(associatedPrototype),
                DataAccess.GetTotalCementitious(associatedPrototype),
                DataAccess.GetWaterCMRatio(associatedPrototype),
                DataAccess.GetPrototypeWater(associatedPrototype),
                DataAccess.GetName(trialBatchTab,associatedTrialBatch)/*,
                Model.GetName(submittalTab,associatedSubmittal)/**/
                );                                                                      // display trial batch summary
 
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
                    DataAccess.UpdateMaterial(ID,
                        Program.main.GetEnteredMaterialVendor(),
                        Program.main.GetEnteredMaterialSource(),
                        Program.main.GetEnteredGrade(),
                        Program.main.GetEnteredRelativeDensity(),
                        Program.main.GetEnteredMaterialType());


                    break;
                case PROTOTYPE:

                    DataAccess.UpdatePrototype(ID,

                    Program.main.GetPrototypeInput(
                            DataAccess.GetPrototypeCement(ID),
                            DataAccess.GetPrototypeSCM(ID),
                            DataAccess.GetPrototypeCA(ID),
                            DataAccess.GetPrototypeFN(ID),
                            DataAccess.GetPrototypeAdmixture(ID)).concreteClass,

                        Program.main.GetPrototypeInput(
                            DataAccess.GetPrototypeCement(ID),
                            DataAccess.GetPrototypeSCM(ID),
                            DataAccess.GetPrototypeCA(ID),
                            DataAccess.GetPrototypeFN(ID),
                            DataAccess.GetPrototypeAdmixture(ID)).IsAirEntrained,
                        
                        (double)Program.main.GetPrototypeInput(
                            DataAccess.GetPrototypeCement(ID),
                            DataAccess.GetPrototypeSCM(ID),
                            DataAccess.GetPrototypeCA(ID),
                            DataAccess.GetPrototypeFN(ID),
                            DataAccess.GetPrototypeAdmixture(ID)).TargetAir,
  
                        Program.main.GetPrototypeInput(
                            DataAccess.GetPrototypeCement(ID),
                            DataAccess.GetPrototypeSCM(ID),
                            DataAccess.GetPrototypeCA(ID),
                            DataAccess.GetPrototypeFN(ID),
                            DataAccess.GetPrototypeAdmixture(ID)).WaterContent,
 

                        Program.main.GetPrototypeInput(
                            DataAccess.GetPrototypeCement(ID),
                            DataAccess.GetPrototypeSCM(ID),
                            DataAccess.GetPrototypeCA(ID),
                            DataAccess.GetPrototypeFN(ID),
                            DataAccess.GetPrototypeAdmixture(ID)).CementQty,
 
                        Program.main.GetPrototypeInput(
                            DataAccess.GetPrototypeCement(ID),
                            DataAccess.GetPrototypeSCM(ID),
                            DataAccess.GetPrototypeCA(ID),
                            DataAccess.GetPrototypeFN(ID),
                            DataAccess.GetPrototypeAdmixture(ID)).SCMQty,
 
                        Program.main.GetPrototypeInput(
                            DataAccess.GetPrototypeCement(ID),
                            DataAccess.GetPrototypeSCM(ID),
                            DataAccess.GetPrototypeCA(ID),
                            DataAccess.GetPrototypeFN(ID),
                            DataAccess.GetPrototypeAdmixture(ID)).CAQty,
 
                        Program.main.GetPrototypeInput(
                            DataAccess.GetPrototypeCement(ID),
                            DataAccess.GetPrototypeSCM(ID),
                            DataAccess.GetPrototypeCA(ID),
                            DataAccess.GetPrototypeFN(ID),
                            DataAccess.GetPrototypeAdmixture(ID)).FNQty,
 
                        Program.main.GetPrototypeInput(
                            DataAccess.GetPrototypeCement(ID),
                            DataAccess.GetPrototypeSCM(ID),
                            DataAccess.GetPrototypeCA(ID),
                            DataAccess.GetPrototypeFN(ID),
                            DataAccess.GetPrototypeAdmixture(ID)).AdmixQty



                        );
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
            Program.main.DisplayInListBox(tab, DataAccess.GetListOfRecords(tab, Program.main.GetFilterSelection(tab)));

        }
        public static void NewMaterial()
        {
            Edit(MATERIALS);
            DataAccess.CreateNewMaterial(Program.main.GetEnteredMaterialType());
            Program.main.DisplayInListBox(MATERIALS, DataAccess.GetListOfRecords(MATERIALS));
            Program.main.SelectLastIndex(MATERIALS);
        }
        public static void NewMaterialSelect()
        {
            Program.main.MaterialSelectMode();
        }

        public static void Advance(byte tab, byte selection)
        {
            switch (tab)
            {
                case MATERIALS:
                    Program.main.DisplayPrototypesInSelectionListBox(DataAccess.GetPrototypesWithoutMaterial(
                        Program.main.GetSelectedID(MATERIALS),
                        Program.main.GetEnteredMaterialType()
                        ));
                    Program.main.SelectPrototypeToAddMaterialMode();
                    break;
                case PROTOTYPE:
                    DataAccess.CreateNewTrialBatch(selection);
                    Program.main.DisplayInListBox(TRIAL_BATCH, DataAccess.GetListOfRecords(TRIAL_BATCH));
                    Program.main.SelectLastIndex(TRIAL_BATCH);
                    Program.main.ThawTab(TRIAL_BATCH);
                    Program.main.OpenTab(TRIAL_BATCH);
                    break;
                case TRIAL_BATCH:
                    /*Model.CreateNewSubmittal(selection);/**/
                    /*
                    Program.main.DisplayInListBox(SUBMITTAL, Model.GetList(SUBMITTAL));
                    Program.main.SelectLastIndex(SUBMITTAL);
                    Program.main.ThawTab(SUBMITTAL);
                    Program.main.OpenTab(SUBMITTAL);
                    break;
                case SUBMITTAL:
                    /**/
                    DataAccess.CreateNewMixDesign(selection);
                    Program.main.DisplayInListBox(MIX_DESIGN, DataAccess.GetListOfRecords(MIX_DESIGN));
                    Program.main.SelectLastIndex(MIX_DESIGN);
                    Program.main.FreezeTab(MIX_DESIGN);
                    Program.main.OpenTab(MIX_DESIGN);
                    break;
                case MIX_DESIGN:
                    Program.main.PrintMixDesign();
                    break;
                default:
                    Console.WriteLine("Something went wrong!");
                    break;
            }
        }

        public static void PrototypeSelectedForMaterialAdd(string selection)
        {
            if (selection == "Create New Prototype")
                Program.main.ClassSelectMode();
            else
            {
                DataAccess.AddMaterialToPrototype(ParseSelection(selection), Program.main.GetSelectedID(MATERIALS));
                Program.main.ThawTab(MATERIALS);
            }
            Program.main.OpenTab(PROTOTYPE);
        }

        public static void ClassSelectedForPrototypeAdd(string selection)
        {
            selection = selection.Remove(0, 6);     //Remove the "Class " at the beginning
            DataAccess.CreateNewPrototype(selection);
            Program.main.SelectLastIndex(PROTOTYPE);
            Program.main.ThawTab(PROTOTYPE);
        }

        public static void CopyPrototype(byte ID)
        {
            DataAccess.CopyPrototype(ID);
            Program.main.DisplayInListBox(PROTOTYPE, DataAccess.GetListOfRecords(PROTOTYPE));
            Program.main.SelectLastIndex(PROTOTYPE);
            Program.main.ThawTab(PROTOTYPE);
        }

    }
}
