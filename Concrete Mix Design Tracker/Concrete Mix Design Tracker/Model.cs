using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete_Mix_Design_Tracker
{
    struct MaterialField
    {
        public string Name;
        public string QuantityOne;
        public string QuantityTwo;
    }
    static class Model
    {
        const decimal UW_OF_WATER = (decimal)62.4;
        static MixDataDataContext db = new MixDataDataContext();
        const byte
            MATERIALS = 0,
            PROTOTYPE = 1,
            TRIAL_BATCH = 2,
            SUBMITTAL = 3,
            MIX_DESIGN = 4;
        const byte
            CEMENT = 0,
            SCM = 1,
            COARSE_AGGREGATE = 2,
            FINE_AGGREGATE = 3,
            ADMIXTURE = 4;

        /// <summary>
        /// Returns an IEnumerable list of identifiers for each tab
        /// </summary>
        /// <param name="tab">index of the tab to reference</param>
        /// <param name="filter">narrow the search with this item</param>
        /// <returns></returns>
        static public List<string> GetList(byte tab, string filter = "No Filter")
        {
            List<string> returnValues = new List<string>();
            switch (tab)
            {
                case MATERIALS:
                    if (filter != "No Filter")
                    {
                        var materialsQuery = from materialType in db.Material_Types
                                             join material in db.Materials on materialType.Material_Type_ID equals material.Material_Type_ID
                                             where materialType.Type_Name == filter
                                             select string.Format("{0:000} {1,-20} {2,10}",material.Material_ID, material.Material_Source, material.Grade);

                        foreach (var material in materialsQuery)
                        {
                            returnValues.Add(material);
                        }

                        return returnValues;
                    }
                    else
                    {
                            var materialsQuery = from material in db.Materials
                                             select string.Format("{0:000} {1,-20} {2,8}",material.Material_ID, material.Material_Source, material.Grade);


                        foreach (var material in materialsQuery)
                        {
                            returnValues.Add(material);
                        }
                        return returnValues;
                    }
                case PROTOTYPE:
                    if (filter != "No Filter")
                    {
                        var prototypeQuery = from prototype in db.Prototypes
                            where prototype.Concrete_Class == filter
                            select string.Format("{0:000} {1,20}", prototype.Prototype_ID, prototype.Prototype_Name);

                        foreach (var prototype in prototypeQuery)
                        {
                            returnValues.Add(prototype);
                        }

                        return returnValues;
                    }
                    else
                    {
                        var prototypeQuery = from prototype in db.Prototypes
                            select string.Format("{0:000} {1,20}", prototype.Prototype_ID, prototype.Prototype_Name);

                        foreach (var prototype in prototypeQuery)
                        {
                            returnValues.Add(prototype);
                        }

                        return returnValues;
                    }
                case TRIAL_BATCH:
                    if (filter != "No Filter")
                    {
                        var trialBatchQuery = from trial_batch in db.Trial_Batches
                            join prototype in db.Prototypes on trial_batch.Prototype_ID equals prototype.Prototype_ID
                            where prototype.Concrete_Class == filter
                            select string.Format("{0:00} {1,20}",trial_batch.TB_ID , trial_batch.TB_Name);
                        foreach(var trialBatch in trialBatchQuery)
                        {
                            returnValues.Add(trialBatch);
                        }

                        return returnValues;
                    }
                    else
                    {
                        var trialBatchQuery = from trial_batch in db.Trial_Batches
                            select string.Format("{0:000} {1,20}",trial_batch.TB_ID , trial_batch.TB_Name);
                        foreach (var trialBatch in trialBatchQuery)
                        {
                            returnValues.Add(trialBatch);
                        }

                        return returnValues;
                    }
                case SUBMITTAL:
                    if (filter != "No Filter")
                    {
                        var submittalQuery = from submittal in db.Submittals
                            join prototype in db.Prototypes on submittal.Prototype_ID equals prototype.Prototype_ID
                            where prototype.Concrete_Class == filter
                            select string.Format("{0:000} Owner {1,20}", submittal.Submittal_ID, submittal.Owner_Number.ToString());
                        foreach (var submittal in submittalQuery)
                        {
                            returnValues.Add(submittal);
                        }

                        return returnValues;
                    }
                    else
                    {
                        var submittalQuery = from submittal in db.Submittals
                            select string.Format("{0:000} Owner {1,20}", submittal.Submittal_ID, submittal.Owner_Number.ToString());

                        foreach (var submittal in submittalQuery)
                        {
                            returnValues.Add( submittal);
                        }

                        return returnValues;
                    }
                case MIX_DESIGN:
                    if (filter != "No Filter")
                    {
                        var mixQuery = from mix_design in db.Mix_Designs
                            join prototype in db.Prototypes on mix_design.Prototype_ID equals prototype.Prototype_ID
                            where prototype.Concrete_Class == filter
                            select string.Format("{0:000} {1,20}",mix_design.Mix_ID, mix_design.Mix_Name);

                        foreach (var mix in mixQuery)
                        {
                            returnValues.Add( mix);
                        }

                        return returnValues;
                    }
                    else
                    {
                       var mixQuery = from mix_design in db.Mix_Designs
                            select string.Format("{0:000} {1,20}",mix_design.Mix_ID, mix_design.Mix_Name);
                        foreach (var mix in mixQuery)
                        {
                            returnValues.Add(mix);
                        }
                        return returnValues;
                    }
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns an IEnumerable list of filters
        /// </summary>
        /// <param name="tab">Tab to return the filters for</param>
        /// <returns></returns>
        static public List<string> GetFilters(byte tab)
        {
            List<string> returnValue = new List<string>();
            int i;
            IEnumerable<string> Query;
            returnValue.Add("No Filter");
            switch (tab)
            {
                case MATERIALS:
                    Query = (from material in db.Materials
                             join materialType in db.Material_Types on material.Material_Type_ID equals materialType.Material_Type_ID
                         select materialType.Type_Name).Distinct();

                    foreach(var item in Query)
                    {
                        returnValue.Add(item);
                    }
                    return returnValue;

                case PROTOTYPE:
                    Query = (from prototype in db.Prototypes
                         select prototype.Concrete_Class).Distinct();

                    i = 0;
                    foreach(var item in Query)
                    {
                        returnValue.Add(item);
                    }
                    return returnValue;
                    
               case TRIAL_BATCH:
                    Query = (from trial_batch in db.Trial_Batches
                         join prototype in db.Prototypes on trial_batch.Prototype_ID equals prototype.Prototype_ID
                         select prototype.Concrete_Class).Distinct();
 
                    i = 0;
                    foreach(var item in Query)
                    {
                        returnValue.Add(item);
                    }
                    return returnValue;
               case SUBMITTAL:
                    Query = (from submittal in db.Submittals
                         join prototype in db.Prototypes on submittal.Prototype_ID equals prototype.Prototype_ID
                         select prototype.Concrete_Class).Distinct();
 
                    i = 0;
                    foreach(var item in Query)
                    {
                        returnValue.Add(item);
                    }
                    return returnValue;
               default:
                    Query = (from mix_design in db.Mix_Designs
                         join prototype in db.Prototypes on mix_design.Prototype_ID equals prototype.Prototype_ID
                         select prototype.Concrete_Class).Distinct();
 
                    i = 0;
                    foreach(var item in Query)
                    {
                        returnValue.Add(item);
                    }
                    return returnValue;
           }

        }

        /// <summary>
        /// Returns a material name based on ID
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns></returns>
        static public string GetMaterialName(byte ID)
         {
            return (from material in db.Materials
                    where material.Material_ID == ID
                    select material.Material_Name).First();
         }

        /// <summary>
        /// Return a material source based on ID
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns></returns>
        static public string GetMaterialSource(byte ID)
        {
            return (from material in db.Materials
                    where material.Material_ID == ID
                    select material.Material_Source).First();
        }
        static public string GetMaterialGrade(byte ID)
        {
            return (from material in db.Materials
                    where material.Material_ID == ID
                    select material.Grade).First();
        }

        /// <summary>
        /// Return a material type based on ID
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns></returns>
        static public byte GetMaterialType(byte ID)
        {
            return (from material in db.Materials
                    where material.Material_ID == ID
                    select material.Material_Type_ID).First();
        }
        static public string GetMaterialTypeName(byte ID)
        {
            var Q =(from material in db.Materials
                    join materialType in db.Material_Types on material.Material_Type_ID equals materialType.Material_Type_ID
                    where material.Material_ID == ID
                    select materialType.Type_Name).First();
            return Q;
        }

        /// <summary>
        /// Return the relative density of a material based on ID
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns></returns>
        static public decimal GetMaterialRelative_Density(byte ID)
        {
            var Q = (from material in db.Materials
                    where material.Material_ID == ID
                    select material.Relative_Density).First();
            return (decimal)Q;
        }
        
        /// <summary>
        /// Return a dictionary with each property and a string of its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public Dictionary<string, string> GetMaterialProperties (byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            switch (GetMaterialType(ID))
            {
                case CEMENT:
                    // Moved "grade" to main materials table but left cement table for future expansion
                        break;
                case SCM:
                    // Moved "grade" to main materials table but left scm table for future expansion
                    break;
                case COARSE_AGGREGATE:
                    var coarseAggregates =
                        db.Aggregates
                        .Join(db.Coarse_Aggregates, coarse => coarse.Aggregate_ID,
                        aggregate => aggregate.Aggregate_ID,
                        (aggregate, coarse) => new
                        {
                            Material_ID = aggregate.Material_ID,
                            Absorption = aggregate.Absorption.ToString(),
                            CA_Size = coarse.CA_Size.ToString(),
                            CA_UW = coarse.CA_UW.ToString(),
                        }).Where(aggregate => aggregate.Material_ID == ID).First();
                    dicReturn.Add(
                        "Absorption", coarseAggregates.Absorption);
                    dicReturn.Add(
                        "Size", coarseAggregates.CA_Size);
                    dicReturn.Add(
                        "Unit Weight", coarseAggregates.CA_UW);
                    break;
                case FINE_AGGREGATE:
                    var fineAggregates =
                        db.Aggregates
                        .Join(db.Fine_Aggregates, fine => fine.Aggregate_ID,
                        aggregate => aggregate.Aggregate_ID,
                        (aggregate, fine) => new
                        {
                            Material_ID = aggregate.Material_ID,
                            Absorption = aggregate.Absorption.ToString(),
                            Fineness_Modulus = fine.Fineness_Modulus.ToString(),
                        }).Where(aggregate => aggregate.Material_ID == ID).First();
                    dicReturn.Add(
                        "Absorption", fineAggregates.Absorption);
                    dicReturn.Add(
                        "Fineness Modulus", fineAggregates.Fineness_Modulus);
                    break;
                case ADMIXTURE:
                    dicReturn.Add(
                        "Minimum Dosage", db.Admixtures
                        .Where(admixture => admixture.Material_ID == ID)
                        .Select(admixture => admixture.Minimum_Dosage.ToString()).First());
                    dicReturn.Add(
                        "Maximum Dosage", db.Admixtures
                        .Where(admixture => admixture.Material_ID == ID)
                        .Select(admixture => admixture.Maximum_Dosage.ToString()).First());
                    dicReturn.Add(
                        "Dose Type", (bool)db.Admixtures
                        .Where(admixture => admixture.Material_ID == ID)
                        .Select(admixture => admixture.Is_ByCWT).First() ? "CWT" : "YD");
                    break;

            }
            return dicReturn;
        }
        /// <summary>
        /// Returns the display name of the item
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        static public string GetName(byte tab, byte ID)
        {
            switch (tab)
            {
                case MATERIALS:
                    return db.Materials.Where(material => material.Material_ID == ID).Select(item => item.Material_Source + " " + item.Grade).First();
                case PROTOTYPE:
                    return db.Prototypes.Where(prototype => prototype.Prototype_ID == ID).Select(item => item.Prototype_Name).First();
                case TRIAL_BATCH:
                    return db.Trial_Batches.Where(trialBatch => trialBatch.TB_ID == ID).Select(item => item.TB_Name).First();
                case SUBMITTAL:
                    return db.Submittals.Where(submittal => submittal.Submittal_ID == ID).Select(item => "Owner: " + item.Owner_Number).First();
                case MIX_DESIGN:
                    return db.Mix_Designs.Where(mix => mix.Mix_ID == ID).Select(item => item.Mix_Name).First();
                default:
                    return "None shall pass.";
            }


        }

         /// <summary>
        /// Return a dictionary with each cement material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public List<MaterialField> GetPrototypeCement (byte ID)
        {
            List<MaterialField> mfReturn = new List<MaterialField>();

            var materialQuery = from cementProportions in db.Cement_Proportions
                                join cement in db.Cements on cementProportions.Cement_ID equals cement.Cement_ID
                                join material in db.Materials on cement.Material_ID equals material.Material_ID
                                where cementProportions.Prototype_ID == ID
                                select new
                                {
                                    Name = material.Material_Source + " " + material.Grade,
                                    Weight = Convert.ToInt32(cementProportions.Weight_of_Cement),
                                    Volume = cementProportions.Weight_of_Cement/ (material.Relative_Density * UW_OF_WATER)
                                };

            foreach(var item in materialQuery)
            {
                mfReturn.Add(new MaterialField { Name = item.Name, QuantityOne = item.Weight.ToString(), QuantityTwo = item.Volume.ToString() });
            }

            return mfReturn;
        }

       /// <summary>
        /// Return a dictionary with each SCM material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public List<MaterialField> GetPrototypeSCM (byte ID)
        {
            List<MaterialField> mfReturn = new List<MaterialField>();

            var materialQuery = from scmProportions in db.SCM_Proportions
                                join scm in db.SCMs on scmProportions.SCM_ID equals scm.SCM_ID
                                join material in db.Materials on scm.Material_ID equals material.Material_ID
                                where scmProportions.Prototype_ID == ID
                                select new
                                {
                                    Name = material.Material_Source + " " + material.Grade,
                                    Weight = Convert.ToInt32(scmProportions.Weight_of_SCMs),
                                    Volume = scmProportions.Weight_of_SCMs/(UW_OF_WATER * material.Relative_Density)
                                };

            foreach(var item in materialQuery)
            {
                mfReturn.Add(new MaterialField {Name = item.Name, QuantityOne = item.Weight.ToString(), QuantityTwo = item.Volume.ToString() });
            }

            return mfReturn;
        }

        /// <summary>
        /// Return a dictionary with each CA material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public List<MaterialField> GetPrototypeCA (byte ID)
        {
            List<MaterialField> mfReturn = new List<MaterialField>();

            var materialQuery = from caProportions in db.CA_Proportions
                                join ca in db.Coarse_Aggregates on caProportions.CA_ID equals ca.CA_ID
                                join aggregate in db.Aggregates on ca.Aggregate_ID equals aggregate.Aggregate_ID
                                join material in db.Materials on aggregate.Material_ID equals material.Material_ID
                                where caProportions.Prototype_ID == ID
                                select new
                                {
                                    Name = material.Material_Source + " " + material.Grade,
                                    Weight = Convert.ToInt32(caProportions.Weight_of_CA),
                                    Volume = caProportions.Weight_of_CA / (UW_OF_WATER * material.Relative_Density)
                                };

            foreach(var item in materialQuery)
            {
                mfReturn.Add(new MaterialField { Name = item.Name, QuantityOne = item.Weight.ToString(), QuantityTwo = item.Volume.ToString() });
            }

            return mfReturn;
        }

       /// <summary>
        /// Return a dictionary with each FN material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public List<MaterialField> GetPrototypeFN (byte ID)
        {
            List<MaterialField> mfReturn = new List<MaterialField>();

            var materialQuery = from fnProportions in db.FN_Proportions
                                join fn in db.Fine_Aggregates on fnProportions.FN_ID equals fn.FN_ID
                                join aggregate in db.Aggregates on fn.Aggregate_ID equals aggregate.Aggregate_ID
                                join material in db.Materials on aggregate.Material_ID equals material.Material_ID
                                where fnProportions.Prototype_ID == ID
                                select new
                                {
                                    Name = material.Material_Source + " " + material.Grade,
                                    Weight = Convert.ToInt32(fnProportions.Weight_of_FN),
                                    Volume = fnProportions.Weight_of_FN / (UW_OF_WATER * material.Relative_Density)
                                };

            foreach(var item in materialQuery)
            {
                mfReturn.Add(new MaterialField { Name = item.Name, QuantityOne = item.Weight.ToString(), QuantityTwo = item.Volume.ToString() });
            }

            return mfReturn;
        }

        /// <summary>
        /// Return a dictionary with each Admixture material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public List<MaterialField> GetPrototypeAdmixture (byte ID)
        {
            List<MaterialField> mfReturn = new List<MaterialField>();


            var materialQuery = from admixProportions in db.Admixture_Proportions
                                join admixture in db.Admixtures on admixProportions.Admixture_ID equals admixture.Admixture_ID
                                join material in db.Materials on admixture.Material_ID equals material.Material_ID
                                where admixProportions.Prototype_ID == ID
                                select new
                                {
                                    Name = material.Material_Source + " " + material.Grade,
                                    cwt = admixProportions.Admixture_Qty,
                                    yd = admixProportions.Admixture_Qty * GetCWT(ID) 
                                };
            foreach (var item in materialQuery)
                mfReturn.Add(new MaterialField { Name = item.Name, QuantityOne = item.cwt.ToString(), QuantityTwo = item.yd.ToString() });
            return mfReturn;
        }

        /// <summary>
        /// Returns the total cementitious materials by weight as an int
        /// </summary>
        /// <param name="ID">ID of prototype to total</param>
        /// <returns></returns>
        static public int GetTotalCementitious (byte ID)
        {
            return
                (int)db.Cement_Proportions.Where(cement => cement.Prototype_ID == ID).Sum(item => item.Weight_of_Cement) +
                (int)db.SCM_Proportions.Where(scm => scm.Prototype_ID == ID).Sum(item => item.Weight_of_SCMs);  
        }

        /// <summary>
        /// Returns the hundred weight of cement as a decimal
        /// </summary>
        /// <param name="ID">ID of prototype to total</param>
        /// <returns></returns>
        static public decimal GetCWT (byte ID)
        {
            return
                (decimal)GetTotalCementitious(ID) / 100;
        }

        /// <summary>
        /// Return a key value pair with Water as the key and the qty as the value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public int GetPrototypeWater (byte ID)
        {
            int materialQuery = Convert.ToInt32((from prototype in db.Prototypes
                                select prototype.Weight_of_Water).First());

            return materialQuery;
        }

        /// <summary>
        /// Returns a double representing the water/ cement ratio
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public decimal GetWaterCMRatio (byte ID)
        {
            return (decimal) ((decimal)GetPrototypeWater(ID)/(decimal)GetTotalCementitious(ID));
        }

        /// <summary>
        /// Inserts a new record into the appropriate tables for a new material
        /// </summary>
        /// <param name="materialType">The type of material to make</param>
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
            byte lastSubID;
            byte lastAggID;
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
                    admix.Is_ByCWT = true;
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
            var lastSerial = db.Prototypes.Where(prototype => prototype.Concrete_Class == concreteClass).Max(item => item.Prototype_Serial);

            Prototype proto = new Prototype();
            proto.Prototype_ID = Convert.ToByte(lastID + 1);
            proto.Prototype_Serial = Convert.ToByte(lastSerial + 1);
            proto.Prototype_Name = "P-" + concreteClass + string.Format("{0:000}",proto.Prototype_Serial.ToString());
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
                             where prototype.Concrete_Class == db.Prototypes.Where(proto => proto.Prototype_ID == prototypeID).Select(item => item.Concrete_Class).First()
                             select trialBatch.TB_Serial).Max();
            string concreteClass = (db.Prototypes.Where(prototype => prototype.Prototype_ID == prototypeID).Select(item => item.Concrete_Class)).First();

            Trial_Batch trial = new Trial_Batch();
            trial.TB_ID = Convert.ToByte(lastID + 1);
            trial.TB_Serial = Convert.ToByte(lastSerial + 1);
            trial.TB_Name = "TB-" + concreteClass + string.Format("{0:000}", trial.TB_Serial.ToString());
            db.Trial_Batches.InsertOnSubmit(trial);
            db.SubmitChanges();
        }

        /// <summary>
        /// Inserts a new record for a Submittal
        /// </summary>
        static public void CreateNewSubmittal(byte ID)
        {
            var lastID = db.Submittals.Max(submittal => submittal.Submittal_ID);

            Submittal sub = new Submittal();
            sub.Submittal_ID = Convert.ToByte(lastID + 1);
            db.Submittals.InsertOnSubmit(sub);
            db.SubmitChanges();
        }

        /// <summary>
        /// Inserts a new record for a Mix Design
        /// </summary>
        static public void CreateNewMixDesign(byte prototypeID)
        {
            var lastID = db.Mix_Designs.Max(mix => mix.Mix_ID);
            var lastSerial = (from mix in db.Mix_Designs
                              join prototype in db.Prototypes on mix.Prototype_ID equals prototype.Prototype_ID
                              where prototype.Concrete_Class == db.Prototypes.Where(proto => proto.Prototype_ID == prototypeID).Select(item => item.Concrete_Class).First()
                              select mix.Mix_Serial).Max();
            string concreteClass = (db.Prototypes.Where(prototype => prototype.Prototype_ID == prototypeID).Select(item => item.Concrete_Class)).First();

            Mix_Design mix_Design = new Mix_Design();
            mix_Design.Mix_ID = Convert.ToByte(lastID + 1);
            mix_Design.Mix_Serial = Convert.ToByte(lastSerial + 1);
            mix_Design.Mix_Name = concreteClass + "-" + string.Format("{0:00}", mix_Design.Mix_Serial.ToString());
            db.Mix_Designs.InsertOnSubmit(mix_Design);
            db.SubmitChanges();
        }

        /// <summary>
        /// Updates an existing Material record
        /// </summary>
        /// <param name="materialID">Material_ID</param>
        /// <param name="materialName">Material_Name</param>
        /// <param name="materialSource">Material_Source</param>
        /// <param name="relativeDensity">Relative_Density</param>
        /// <param name="materialTypeID">Material_Type_ID</param>
        static public void UpdateMaterial(
            byte materialID,
            string materialName,
            string materialSource,
            string materialGrade,
            decimal relativeDensity,
            byte materialTypeID
            )
        {
            var recordToUpdate = (from material in db.Materials
                                 where material.Material_ID == materialID
                                 select material).First();
            recordToUpdate.Material_Name = materialName;
            recordToUpdate.Material_Source = materialSource;
            recordToUpdate.Grade = materialGrade;
            recordToUpdate.Relative_Density = relativeDensity;
            recordToUpdate.Material_Type_ID = materialTypeID;

            db.SubmitChanges();
        }

        /// <summary>
        /// Updates an existing cement record
        /// </summary>
        /// <param name="materialID">Material_ID</param>
        /// <param name="cementType">Cement_Type</param>
        static public void UpdateCement(
            byte materialID,
            string cementType
            )
        {
            // This is a stub for future expansion
            db.SubmitChanges();
        }

        static public void UpdateSCM(
            byte materialID,
            string SCM_class
            )
        {
            // This is a stub for future expansion
            db.SubmitChanges();
        }

        /// <summary>
        /// Updates an existing Aggregate Record
        /// </summary>
        /// <param name="materialID"></param>
        /// <param name="aggregateGrade"></param>
        /// <param name="absorption"></param>
        static public void UpdateAggregate(
            byte materialID,
            string aggregateGrade,
            decimal absorption
            )
        {
            var recordToUpdate = (from agg in db.Aggregates
                                  where agg.Material_ID == materialID
                                  select agg).First();
            recordToUpdate.Absorption = absorption;
            db.SubmitChanges();
        }

        /// <summary>
        /// Updates an existing Coarse Aggregate Record
        /// </summary>
        /// <param name="materialID"></param>
        /// <param name="caSize"></param>
        /// <param name="caUnitWeight"></param>
        static public void UpdateCoarseAggregate(
            byte materialID,
            decimal caSize,
            decimal caUnitWeight)
        {
            byte lookUpValue = (from agg in db.Aggregates
                              where agg.Material_ID == materialID
                              select agg.Aggregate_ID).First();

            var recordToUpdate = (from ca in db.Coarse_Aggregates
                                  where ca.Aggregate_ID == lookUpValue
                                  select ca).First();
            recordToUpdate.CA_Size = caSize;
            recordToUpdate.CA_UW = caUnitWeight;
            db.SubmitChanges();
        }

        /// <summary>
        /// Updates an existing Fine Aggregate Record
        /// </summary>
        /// <param name="materialID">Material_ID</param>
        /// <param name="finenessModulus">Fineness_Modulus</param>
        static public void UpdateFineAggregate(
            byte materialID,
            decimal finenessModulus
            )
        {
            byte lookUpValue = (from agg in db.Aggregates
                                where agg.Material_ID == materialID
                                select agg.Aggregate_ID).First();
            var recordToUpdate = (from fn in db.Fine_Aggregates
                                  where fn.Aggregate_ID == lookUpValue
                                  select fn).First();
            recordToUpdate.Fineness_Modulus = finenessModulus;
            db.SubmitChanges();
        }

        /// <summary>
        /// Updates an existing Admixture Record
        /// </summary>
        /// <param name="materialID">Material_ID</param>
        /// <param name="minDose">Minimum_Dosage</param>
        /// <param name="maxDose">Maximum_Dosage</param>
        /// <param name="isByCWT">Is_ByCWT</param>
        static public void UpdateAdmixture(
            byte materialID,
            decimal minDose,
            decimal maxDose,
            bool isByCWT
            )
        {
            var recordToUpdate = (from admix in db.Admixtures
                                  where admix.Material_ID == materialID
                                  select admix).First();
            recordToUpdate.Minimum_Dosage = minDose;
            recordToUpdate.Maximum_Dosage = maxDose;
            recordToUpdate.Is_ByCWT = isByCWT;
            db.SubmitChanges();
        }

        static public void UpdatePrototype(
            byte prototypeID,
            //TODO: naming scheme for prototypes etc need to be added to create
            string concreteClass,
            bool isAirEntrained,
            double targetAir,
            int totalCM,
            double totalCA,
            double calculatedDensity,
            int weightOfWater)
        {
            var recordToUpdate = (from prototype in db.Prototypes
                                  where prototype.Prototype_ID == prototypeID
                                  select prototype).First();
            recordToUpdate.Concrete_Class = concreteClass;
            recordToUpdate.Is_Air_Entrained = isAirEntrained;
            recordToUpdate.Target_Air = (decimal)targetAir;
            recordToUpdate.Total_CM = totalCM;
            recordToUpdate.Total_CA = (decimal)totalCA;
            recordToUpdate.Calculated_Density = (decimal)calculatedDensity;
            recordToUpdate.Weight_of_Water = weightOfWater;
        }
        /// <summary>
        /// Returns the properties of the cement table in a dictionary
        /// </summary>
        /// <param name="ID">Material_ID</param>
        /// <returns></returns>
        static private Dictionary<string, string> GetCementProperties(byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();

            // This is an area for future expansion
            return dicReturn;
        }

        /// <summary>
        /// Returns the properties of the SCM table in a dictionary
        /// </summary>
        /// <param name="ID">Material_ID</param>
        /// <returns></returns>
        static private Dictionary<string, string> GetSCMProperties(byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();

            // This is a stub for future expansion
            return dicReturn;
        }

        /// <summary>
        /// Returns the properties of the Aggregate and Course aggregate table in a dictionary
        /// </summary>
        /// <param name="ID">Material_ID</param>
        /// <returns>Dictionary of Properties</returns>
        static private Dictionary<string, string> GetCoarseAggregateProperties(byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();

            var Query = (from aggregate in db.Aggregates
                         join ca in db.Coarse_Aggregates on aggregate.Aggregate_ID equals ca.Aggregate_ID
                         where aggregate.Material_ID == ID
                         select new
                         {
                             Absorption = aggregate.Absorption,
                             Size = ca.CA_Size,
                             UnitWeight = ca.CA_UW
                         }).First();
            dicReturn.Add("Absorption", Query.Absorption.ToString());
            dicReturn.Add("Size", Query.Size.ToString());
            dicReturn.Add("Unit Weight", Query.UnitWeight.ToString());

            return dicReturn;
        }

        /// <summary>
        /// Returns a dictionary containing all of the aggregate and fine aggregate table properties
        /// </summary>
        /// <param name="ID">Material_ID</param>
        /// <returns>Dictionary containing fine aggregate properties</returns>
        static private Dictionary<string, string> GetFineAggregateProperties(byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            var Query = (from aggregate in db.Aggregates
                         join fn in db.Fine_Aggregates on aggregate.Aggregate_ID equals fn.Aggregate_ID
                         where aggregate.Material_ID == ID
                         select new
                         {
                             Absorption = aggregate.Absorption,
                             FM = fn.Fineness_Modulus
                         }).First();
            dicReturn.Add("Absorption", Query.Absorption.ToString());
            dicReturn.Add("Fineness Modulus", Query.FM.ToString());

            return dicReturn;
        }

        /// <summary>
        /// Returns a dictionary containing all of the admixture properties from the admixture table
        /// </summary>
        /// <param name="ID">Material_ID</param>
        /// <returns>Dictionary with all of the admixture properties</returns>
        static private Dictionary<string, string> GetAdmixtureProperties(byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            var Query = (from admixture in db.Admixtures
                         where admixture.Material_ID == ID
                         select admixture).First();

            dicReturn.Add("Minimum Dosage", Query.Minimum_Dosage.ToString());
            dicReturn.Add("Maximum Dosage", Query.Maximum_Dosage.ToString());
            dicReturn.Add("Dosed by CWT?", Query.Is_ByCWT.ToString());
            return dicReturn;
        }
        static private Dictionary<string, string> GetPrototypeProperties(byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            var Query = (from prototype in db.Prototypes
                         where prototype.Prototype_ID == ID
                         select prototype).First();
            dicReturn.Add("Concrete Class", Query.Concrete_Class);
            dicReturn.Add("Air Entrained", Query.Is_Air_Entrained.ToString());
            dicReturn.Add("Target Air Content", Query.Target_Air.ToString());
            dicReturn.Add("Total Cementitious Material", Query.Total_CM.ToString());
            dicReturn.Add("Coarse Aggregate % by Volume", Query.Total_CA.ToString());
            dicReturn.Add("Calculated Density", Query.Calculated_Density.ToString());

            return dicReturn;
        }
        static private Dictionary<string, string> GetTrialBatchProperties(byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            var Query = (from trial in db.Trial_Batches
                         join prototype in db.Prototypes on trial.Prototype_ID equals prototype.Prototype_ID
                         where trial.TB_ID == ID
                         select new
                         {
                             Slump = trial.Slump,
                             Actual_Air = trial.Actual_Air,
                             Concrete_Temperature = trial.Concrete_Temperature,
                             Ambient_Temperature = trial.Ambient_Temperature,
                             Measured_Density = trial.Measured_Density,
                             Compressive_Strength = trial.Compressive_Strength,
                             PrototypeID = prototype.Prototype_ID,
                             PrototypeName = prototype.Prototype_Name
                         }
                         ).First();

            dicReturn.Add("Slump", Query.Slump.ToString());
            dicReturn.Add("Actual Air Content", Query.Actual_Air.ToString());
            dicReturn.Add("Fresh Concrete Temperature", Query.Concrete_Temperature.ToString());
            dicReturn.Add("Ambient Temperature", Query.Ambient_Temperature.ToString());
            dicReturn.Add("Measured Concrete Density", Query.Measured_Density.ToString());
            dicReturn.Add("Compressive Strength at 28 days", Query.Compressive_Strength.ToString());
            dicReturn.Add("Prototype Name", Query.PrototypeName);
            foreach (var item in GetPrototypeProperties(Query.PrototypeID))
                dicReturn.Add(item.Key, item.Value);
            return dicReturn;
        }
        static private Dictionary<string, string> GetSubmittalProperties(byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            var Query = (from submittal in db.Submittals
                         join prototype in db.Prototypes on submittal.Prototype_ID equals prototype.Prototype_ID
                         join trial in db.Trial_Batches on prototype.Prototype_ID equals trial.Prototype_ID
                         where submittal.Submittal_ID == ID
                         select new
                         {
                             Owner_Number = submittal.Owner_Number,
                             TrialName = trial.TB_Name,
                             TBID = trial.TB_ID,
                         }).First();
            dicReturn.Add("Owner Number", Query.Owner_Number.ToString());
            dicReturn.Add("Trial Batch", Query.TrialName);
            foreach (var item in GetTrialBatchProperties(Query.TBID))
                dicReturn.Add(item.Key, item.Value);
            return dicReturn;
        }
        static private Dictionary<string, string> GetMixDesignProperties(byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            var Query = (from mix in db.Mix_Designs
                         join sub in db.Submittals on mix.Prototype_ID equals sub.Prototype_ID
                         where mix.Mix_ID == ID
                         select mix).First();

            foreach (var item in GetSubmittalProperties(Query.Submittal_ID))
                dicReturn.Add(item.Key, item.Value);
            return dicReturn;
        }


        /// <summary>
        /// Returns a volume calculated from weight for a given material
        /// </summary>
        /// <param name="ID">Material_ID</param>
        /// <param name="weight">Weight to be converted</param>
        /// <returns>Decimal representing a volume of material</returns>
        static public decimal GetVolumeFromWeight (byte ID, int weight)
        {
            var Query = (from material in db.Materials
                        where material.Material_ID == ID
                        select material).First();

            return (decimal)weight / (UW_OF_WATER * (decimal)Query.Relative_Density);
        }

        /// <summary>
        /// Returns a weight calculated from volume for a given material
        /// </summary>
        /// <param name="ID">Material_ID</param>
        /// <param name="volume">Volume to be converted</param>
        /// <returns>A weight of material in lbs</returns>
        static public int GetWeightFromVolume (byte ID, decimal volume)
        {
            var Query = (from material in db.Materials
                         where material.Material_ID == ID
                         select material).First();
            return (int)(volume * UW_OF_WATER * Query.Relative_Density);
        }

        static public string GetMixSummary (byte ID)
        {
            string returnstring = "";
            // Build a string to summarize the mix design

            return returnstring;
        }

        static public Dictionary<string, string> GetProperties(byte tab, byte ID)
        {
            if (tab == MATERIALS)
            {
                byte materialType = (from material in db.Materials
                                     where material.Material_ID == ID
                                     select material.Material_Type_ID).First();
                 if (materialType == CEMENT)
                    return GetCementProperties(ID);
                else if (materialType == SCM)
                    return GetSCMProperties(ID);
                else if (materialType == COARSE_AGGREGATE)
                    return GetCoarseAggregateProperties(ID);
                else if (materialType == FINE_AGGREGATE)
                    return GetFineAggregateProperties(ID);
                else if (materialType == ADMIXTURE)
                    return GetAdmixtureProperties(ID);
                else return null;           
            }
        switch (tab)
            {
                case PROTOTYPE:
                    return GetPrototypeProperties(ID);
                case TRIAL_BATCH:
                    return GetTrialBatchProperties(ID);
                case SUBMITTAL:
                    return GetSubmittalProperties(ID);
                case MIX_DESIGN:
                    return GetMixDesignProperties(ID);
                default:
                    return null;
            }
        }



        static public List<string> GetPrototypesWithoutMaterial(byte materialID, byte type)
        {
            List<string> lstReturn = new List<string>();
            IEnumerable<byte> remaining;
            IEnumerable<byte> exclude;

            switch (type)
            {
                case CEMENT:
                    byte cementID = db.Cements.Where(cement => cement.Material_ID == materialID).Select(cement => cement.Cement_ID).First();
                    exclude = (from cementProportion in db.Cement_Proportions
                                where cementProportion.Cement_ID == cementID
                                select cementProportion.Prototype_ID).Distinct();
                    remaining = db.Prototypes.Select(prototype => prototype.Prototype_ID).Except(exclude);
                    break;
                case SCM:
                    byte scmID = db.SCMs.Where(scm => scm.Material_ID == materialID).Select(scm => scm.SCM_ID).First();
                    exclude = (from scmProportion in db.SCM_Proportions
                                where scmProportion.SCM_ID == scmID
                                select scmProportion.Prototype_ID).Distinct();
                    remaining = db.Prototypes.Select(prototype => prototype.Prototype_ID).Except(exclude);
                    break;
                case COARSE_AGGREGATE:
                    byte caID = (from aggregate in db.Aggregates
                                join ca in db.Coarse_Aggregates on aggregate.Aggregate_ID equals ca.Aggregate_ID
                                where aggregate.Material_ID == materialID
                                select ca.CA_ID).First();

                    exclude = (from caProportion in db.CA_Proportions
                               where caProportion.CA_ID == caID
                               select caProportion.Prototype_ID).Distinct();
                    remaining = db.Prototypes.Select(prototype => prototype.Prototype_ID).Except(exclude);

                    break;
                case FINE_AGGREGATE:
                    byte fnID = (from aggregate in db.Aggregates
                                join fn in db.Fine_Aggregates on aggregate.Aggregate_ID equals fn.Aggregate_ID
                                where aggregate.Material_ID == materialID
                                select fn.FN_ID).First();

                    exclude = (from fnProportion in db.FN_Proportions
                               where fnProportion.FN_ID == fnID
                               select fnProportion.Prototype_ID).Distinct();
                    remaining = db.Prototypes.Select(prototype => prototype.Prototype_ID).Except(exclude);
                    break;
                case ADMIXTURE:
                    byte admixID = db.Admixtures.Where(admix => admix.Material_ID == materialID).Select(admix => admix.Admixture_ID).First();
                    exclude = (from admixProportion in db.Admixture_Proportions
                                where admixProportion.Admixture_ID == admixID
                                select admixProportion.Prototype_ID).Distinct();
                    remaining = db.Prototypes.Select(prototype => prototype.Prototype_ID).Except(exclude);
                    break;

               default:
                    remaining = null;
                    break;
            }

            foreach(var item in remaining)
            {
                var proto = db.Prototypes.Where(prototype => prototype.Prototype_ID == item).First();
                lstReturn.Add(string.Format("{0:000} {1}", proto.Prototype_ID, proto.Prototype_Name));
            }
            return lstReturn;
        }

        static public void AddMaterialToPrototype(byte prototypeID, byte materialID)
        {
            byte aggID;

            switch (db.Materials.Where(material => material.Material_ID == materialID).Select(material => material.Material_Type_ID).First())
            {
                case CEMENT:
                    Cement_Proportion cem = new Cement_Proportion();
                    cem.Prototype_ID = prototypeID;
                    cem.Cement_ID = db.Cements.Where(cement => cement.Material_ID == materialID).Select(cement => cement.Cement_ID).First();
                    cem.Weight_of_Cement = 0;
                    db.Cement_Proportions.InsertOnSubmit(cem);
                    break;
                case SCM:
                    SCM_Proportion scm = new SCM_Proportion();
                    scm.Prototype_ID = prototypeID;
                    scm.SCM_ID = db.SCMs.Where(sCM => sCM.Material_ID == materialID).Select(sCM => sCM.SCM_ID).First();
                    scm.Weight_of_SCMs = 0;
                    db.SCM_Proportions.InsertOnSubmit(scm);
                    break;
                case COARSE_AGGREGATE:
                    CA_Proportion ca = new CA_Proportion();
                    ca.Prototype_ID = prototypeID;
                    aggID = db.Aggregates.Where(agg => agg.Material_ID == materialID).Select(agg => agg.Aggregate_ID).First();
                    ca.CA_ID = db.Coarse_Aggregates.Where(cagg => cagg.Aggregate_ID == aggID).Select(cagg => cagg.CA_ID).First();
                    ca.Weight_of_CA = 0;
                    db.CA_Proportions.InsertOnSubmit(ca);
                    break;
                case FINE_AGGREGATE:
                    FN_Proportion fn = new FN_Proportion();
                    fn.Prototype_ID = prototypeID;
                    aggID = db.Aggregates.Where(agg => agg.Material_ID == materialID).Select(agg => agg.Aggregate_ID).First();
                    fn.FN_ID = db.Fine_Aggregates.Where(fagg => fagg.Aggregate_ID == aggID).Select(fagg => fagg.FN_ID).First();
                    fn.Weight_of_FN = 0;
                    break;
                case ADMIXTURE:
                    Admixture_Proportion admix = new Admixture_Proportion();
                    admix.Prototype_ID = prototypeID;
                    admix.Admixture_ID = db.Admixtures.Where(ad => ad.Material_ID == materialID).Select(ad => ad.Admixture_ID).First();
                    admix.Admixture_Qty = 0;
                    db.Admixture_Proportions.InsertOnSubmit(admix);
                    break;
            }
            db.SubmitChanges();

        }
    }
}
