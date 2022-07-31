using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete_Mix_Design_Tracker
{
    /// <summary>
    /// The Model class is a static class that contains all of the DAL (data access layer) for the desktop application.
    /// It is broken up into several files for organizational purposes.  This file focuses on methods to create new records
    /// in the database.
    /// </summary>
    static partial class DataAccess
    {
        static public void CreateNewMaterial(byte materialType)
        {
            // code to get last ID number
            var lastID = db.Materials.Max(material => material.Material_ID);
            lastID++;

            // code to create new material
            Material mat = new Material();
            mat.Material_ID = lastID;
            mat.Material_Name = "None";
            mat.Material_Source = "None";
            mat.Grade = "None";
            mat.Relative_Density = (decimal)0.0;
            mat.Material_Type_ID = materialType;
            db.Materials.InsertOnSubmit(mat);
                
            // variable to store subID
            int lastSubID;
            int lastAggID;
            // code to creat new subtable(s)
            switch (materialType)
            {
                case CEMENT:
                    // code to get last ID number
                    lastSubID = db.Cements.Max(cement => cement.Cement_ID);

                    // code to create new cement
                    Cement cem = new Cement();
                    cem.Cement_ID = Convert.ToByte(lastSubID + 1);
                    cem.Material_ID = lastID;
                    db.Cements.InsertOnSubmit(cem);
                    break;

                case SCM:
                    // code to get last ID number
                    lastSubID = db.SCMs.Max(scm => scm.SCM_ID);

                    // code to create new SCM
                    SCM sCM = new SCM();
                    sCM.SCM_ID = Convert.ToByte(lastSubID + 1);
                    sCM.Material_ID = lastID;
                    db.SCMs.InsertOnSubmit(sCM);
                    break;
                case COARSE_AGGREGATE:
                case FINE_AGGREGATE:
                    // code to get last ID number
                    lastSubID = db.Aggregates.Max(aggregate => aggregate.Aggregate_ID);
                    lastAggID = db.Coarse_Aggregates.Max(ca => ca.CA_ID);

                    Aggregate agg = new Aggregate();
                    agg.Aggregate_ID = Convert.ToByte(lastSubID + 1);
                    agg.Absorption = (decimal)0.0;
                    agg.Material_ID = lastID;
                    db.Aggregates.InsertOnSubmit(agg);

                    if (materialType == COARSE_AGGREGATE)
                    {
                        lastAggID = db.Coarse_Aggregates.Max(fa => fa.CA_ID);
                        Coarse_Aggregate cAgg = new Coarse_Aggregate();
                        cAgg.CA_ID = Convert.ToByte(lastAggID + 1);
                        cAgg.CA_Size = 0;
                        cAgg.CA_UW = 0;
                        cAgg.Aggregate_ID = (byte)(lastSubID + 1);
                        db.Coarse_Aggregates.InsertOnSubmit(cAgg);
                    }
                    else
                    {
                        lastAggID = db.Fine_Aggregates.Max(fa => fa.FN_ID);
                        Fine_Aggregate fAgg = new Fine_Aggregate();
                        fAgg.FN_ID = Convert.ToByte(lastAggID + 1);
                        fAgg.Fineness_Modulus = (decimal)0;
                        fAgg.Aggregate_ID = (byte)(lastSubID + 1);
                        db.Fine_Aggregates.InsertOnSubmit(fAgg);
                    }
                    break;
                case ADMIXTURE:
                    // code to get last ID number
                    lastSubID = db.Admixtures.Max(admixture => admixture.Admixture_ID);
                    Admixture admix = new Admixture();
                    admix.Admixture_ID = Convert.ToByte(lastSubID + 1);
                    admix.Maximum_Dosage = (decimal)0;
                    admix.Minimum_Dosage = (decimal)0;
                    admix.Material_ID = lastID;
                    db.Admixtures.InsertOnSubmit(admix);
                    break;
            }
            // submit all of the changes we made
            db.SubmitChanges();
        }

        /// <summary>
        /// Creates a new record for a Prototype
        /// </summary>
        static public void CreateNewPrototype(string concreteClass)
        {
            var lastID = db.Prototypes.Max(prototype => prototype.Prototype_ID);
            var lastSerial = db.Prototypes
                .Where(prototype => prototype.Concrete_Class == concreteClass)
                .Select(item => (byte?)item.Prototype_Serial)
                .Max();
            if (lastSerial == null) lastSerial = 0;
            Prototype proto = new Prototype();
            proto.Prototype_ID = Convert.ToByte(lastID + 1);
            proto.Prototype_Serial = Convert.ToByte(lastSerial + 1);
            proto.Prototype_Name = "P-" + concreteClass + string.Format("-{0:000}",proto.Prototype_Serial);
            proto.Concrete_Class = concreteClass;
            proto.Is_Air_Entrained = false;
            proto.Target_Air = 0;
            proto.Weight_of_Water = 0;
            db.Prototypes.InsertOnSubmit(proto);
            db.SubmitChanges();
        }
        static public void CopyPrototype(int ID)
        {
            var prototypeToCopy = (from prototype in db.Prototypes
                                   where prototype.Prototype_ID == ID
                                   select prototype).First();
            var concreteClass = prototypeToCopy.Concrete_Class;
            var lastID = db.Prototypes.Max(prototype => prototype.Prototype_ID);
            var lastSerial = db.Prototypes
                .Where(prototype => prototype.Concrete_Class == concreteClass)
                .Select(item => (byte?)item.Prototype_Serial)
                .Max();
            if (lastSerial == null) lastSerial = 0;
            Prototype proto = new Prototype();
            proto.Prototype_ID = Convert.ToByte(lastID + 1);
            proto.Prototype_Serial = Convert.ToByte(lastSerial + 1);
            proto.Prototype_Name = "P-" + concreteClass + string.Format("-{0:000}", proto.Prototype_Serial);
            proto.Concrete_Class = concreteClass;
            proto.Is_Air_Entrained = prototypeToCopy.Is_Air_Entrained;
            proto.Target_Air = prototypeToCopy.Target_Air;
            proto.Weight_of_Water = prototypeToCopy.Weight_of_Water;
            db.Prototypes.InsertOnSubmit(proto);
            db.SubmitChanges();

        }

        /// <summary>
        /// Inserts a new record for a Trial Batch
        /// </summary>
        static public void CreateNewTrialBatch(byte prototypeID)
        {
            var lastID = db.Trial_Batches.Max(trialBatch => trialBatch.TB_ID);
            var lastSerial = (from trialBatch in db.Trial_Batches
                             join prototype in db.Prototypes on trialBatch.Prototype_ID equals prototype.Prototype_ID
                             where prototype.Concrete_Class == db.Prototypes
                             .Where(proto => proto.Prototype_ID == prototypeID)
                             .Select(item => item.Concrete_Class)
                             .First()
                             select (byte?)trialBatch.TB_Serial).Max();
            if (lastSerial == null) lastSerial = 0;
            string concreteClass = (db.Prototypes.Where(prototype => prototype.Prototype_ID == prototypeID).Select(item => item.Concrete_Class)).First();

            Trial_Batch trial = new Trial_Batch();
            trial.TB_ID = Convert.ToByte(lastID + 1);
            trial.TB_Serial = Convert.ToByte(lastSerial + 1);
            trial.TB_Name = "TB-" + concreteClass + string.Format("-{0:000}", trial.TB_Serial);
            trial.Slump = 0;
            trial.Actual_Air = 0;
            trial.Concrete_Temperature = 0;
            trial.Ambient_Temperature = 0;
            trial.Measured_Density = 0;
            trial.Compressive_Strength = 0;
            trial.Prototype_ID = prototypeID;
            trial.Notes = "";
            db.Trial_Batches.InsertOnSubmit(trial);
            db.SubmitChanges();
        }

        /// <summary>
        /// Inserts a new record for a Submittal
        /// </summary>
        /*static public void CreateNewSubmittal(byte tb_ID)
        {
            byte prototypeID = (byte)db.Trial_Batches.Where(trial => trial.TB_ID == tb_ID).Select(trial => trial.Prototype_ID).First(); 
            var lastID = db.Submittals.Max(submittal => (byte?)submittal.Submittal_ID);
            if (lastID == null) lastID = 0;

            Submittal sub = new Submittal();
            sub.Submittal_ID = Convert.ToByte(lastID + 1);
            sub.Owner_Number = 0;
            sub.TB_ID = db.Trial_Batches.Where(trial => trial.Prototype_ID == prototypeID).Select(trial => trial.TB_ID).First();
            sub.Prototype_ID = prototypeID;
            db.Submittals.InsertOnSubmit(sub);
            db.SubmitChanges();
        }/**/


        /// <summary>
        /// Inserts a new record for a Mix Design
        /// </summary>
        static public void CreateNewMixDesign(byte tbID)
        {
            //byte prototypeID = (byte)db.Submittals.Where(sub => sub.Submittal_ID == subID).Select(sub => sub.Prototype_ID).First();
            byte prototypeID = (byte)db.Trial_Batches.Where(trial => trial.TB_ID == tbID).Select(trial => trial.Prototype_ID).First();
            var lastID = db.Mix_Designs.Max(mix => mix.Mix_ID);
            var lastSerial = (from mix in db.Mix_Designs
                              join prototype in db.Prototypes on mix.Prototype_ID equals prototype.Prototype_ID
                              where prototype.Concrete_Class == db.Prototypes.Where(proto => proto.Prototype_ID == prototypeID).Select(item => item.Concrete_Class).First()
                              select (byte?)mix.Mix_Serial).Max();
            if (lastSerial == null) lastSerial = 0;
            string concreteClass = (db.Prototypes.Where(prototype => prototype.Prototype_ID == prototypeID).Select(item => item.Concrete_Class)).First();

            Mix_Design mix_Design = new Mix_Design();
            mix_Design.Mix_ID = Convert.ToByte(lastID + 1);
            mix_Design.Mix_Serial = Convert.ToByte(lastSerial + 1);
            mix_Design.Mix_Name = concreteClass + "-" + string.Format("{0:00}", mix_Design.Mix_Serial);
            mix_Design.Submittal_ID = db.Submittals.Where(sub => sub.Prototype_ID == prototypeID).Select(sub => sub.Submittal_ID).First();
            mix_Design.Prototype_ID = prototypeID;
            db.Mix_Designs.InsertOnSubmit(mix_Design);
            db.SubmitChanges();
        }


    }
}