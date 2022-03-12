using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete_Mix_Design_Tracker
{
    static class Model
    {
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
            WATER = 4,
            ADMIXTURE = 5;

        /// <summary>
        /// Returns an IEnumerable list of identifiers for each tab
        /// </summary>
        /// <param name="tab">index of the tab to reference</param>
        /// <param name="filter">narrow the search with this item</param>
        /// <returns></returns>
        static public IEnumerable<string> GetList(byte tab, string filter = "")
        {
            switch (tab)
            {
                case MATERIALS:
                    if (filter != "")
                    {
                        return
                            from material in db.Materials
                            join materialType in db.Material_Types on material.Material_Type_ID equals materialType.Material_Type_ID
                            where materialType.Type_Name.Contains(filter)
                            select material.Material_ID.ToString() + " " + material.Material_Name + " " + material.Material_Source;
                    }
                    else
                    {
                        return
                            from material in db.Materials
                            select material.Material_ID.ToString() + " " + material.Material_Name + " " + material.Material_Source;
                    }
                case PROTOTYPE:
                    if (filter != "")
                    {
                        return
                            from prototype in db.Prototypes
                            where prototype.Concrete_Class.Contains(filter)
                            select prototype.Prototype_Name;
                    }
                    else
                    {
                        return
                            from prototype in db.Prototypes
                            select prototype.Prototype_Name;
                    }
                case TRIAL_BATCH:
                    if (filter != "")
                    {
                        return
                            from trial_batch in db.Trial_Batches
                            join prototype in db.Prototypes on trial_batch.Prototype_ID equals prototype.Prototype_ID
                            where prototype.Concrete_Class.Contains(filter)
                            select trial_batch.TB_Name;
                    }
                    else
                    {
                        return
                            from trial_batch in db.Trial_Batches
                            select trial_batch.TB_Name;
                    }
                case SUBMITTAL:
                    if (filter != "")
                    {
                        return
                            from submittal in db.Submittals
                            join prototype in db.Prototypes on submittal.Prototype_ID equals prototype.Prototype_ID
                            where prototype.Concrete_Class.Contains(filter)
                            select submittal.Owner_Number.ToString();
                    }
                    else
                    {
                        return
                            from submittal in db.Submittals
                            select submittal.Owner_Number.ToString();
                    }
                default:
                    if (filter != "")
                    {
                        return
                            from mix_design in db.Mix_Designs
                            join prototype in db.Prototypes on mix_design.Prototype_ID equals prototype.Prototype_ID
                            where prototype.Concrete_Class.Contains(filter)
                            select mix_design.Mix_Name;
                    }
                    else
                    {
                        return
                           from mix_design in db.Mix_Designs
                           select mix_design.Mix_Name;
                    }
            }
        }

        /// <summary>
        /// Returns an IEnumerable list of filters
        /// </summary>
        /// <param name="tab">Tab to return the filters for</param>
        /// <returns></returns>
        static public IEnumerable<string> GetFilters(byte tab)
        {
            switch (tab)
            {
                case MATERIALS:
                    return
                        (from material in db.Materials
                         select material.Material_Type.ToString()).Distinct();
                case PROTOTYPE:
                    return
                        (from prototype in db.Prototypes
                         select prototype.Concrete_Class).Distinct();
                case TRIAL_BATCH:
                    return
                        (from trial_batch in db.Trial_Batches
                         join prototype in db.Prototypes on trial_batch.Prototype_ID equals prototype.Prototype_ID
                         select prototype.Concrete_Class).Distinct();
                case SUBMITTAL:
                    return
                        (from submittal in db.Submittals
                         join prototype in db.Prototypes on submittal.Prototype_ID equals prototype.Prototype_ID
                         select prototype.Concrete_Class).Distinct();
                default:
                    return
                        (from mix_design in db.Mix_Designs
                         join prototype in db.Prototypes on mix_design.Prototype_ID equals prototype.Prototype_ID
                         select prototype.Concrete_Class).Distinct();
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
                    select material.Material_Name).First();
         }

        /// <summary>
        /// Return a material source based on ID
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns></returns>
        static public string GeteMaterialSource(byte ID)
        {
            return (from material in db.Materials
                    select material.Material_Source).First();
        }

        /// <summary>
        /// Return a material type based on ID
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns></returns>
        static public byte GetMaterialType(byte ID)
        {
            return (from material in db.Materials
                    select Convert.ToByte(material.Material_Type)).First();
        }

        /// <summary>
        /// Return the relative density of a material based on ID
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns></returns>
        static public decimal GetMaterialRelative_Density(byte ID)
        {
            return (from material in db.Materials
                    select Convert.ToDecimal(material.Relative_Density)).First();
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
                    dicReturn.Add(
                        "Cement Type", db.Cements
                        .Where(cement => cement.Material_ID == ID)
                        .Select(cement => cement.Cement_Type).First());
                        break;
                case SCM:
                    dicReturn.Add(
                        "Class or Grade", db.SCMs
                        .Where(scm => scm.Material_ID == ID)
                        .Select(scm => scm.SCM_Class).First());
                    break;
                case COARSE_AGGREGATE:
                    var coarseAggregates =
                        db.Aggregates
                        .Join(db.Coarse_Aggregates, coarse => coarse.Aggregate_ID,
                        aggregate => aggregate.Aggregate_ID,
                        (aggregate, coarse) => new
                        {
                            Material_ID = aggregate.Material_ID,
                            Aggregate_Grade = aggregate.Aggregate_Grade,
                            Absorption = aggregate.Absorption.ToString(),
                            CA_Size = coarse.CA_Size.ToString(),
                            CA_UW = coarse.CA_UW.ToString(),
                        }).Where(aggregate => aggregate.Material_ID == ID).First();
                    dicReturn.Add(
                        "Aggregate Grade", coarseAggregates.Aggregate_Grade);
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
                            Aggregate_Grade = aggregate.Aggregate_Grade,
                            Absorption = aggregate.Absorption.ToString(),
                            Fineness_Modulus = fine.Fineness_Modulus.ToString(),
                        }).Where(aggregate => aggregate.Material_ID == ID).First();
                    dicReturn.Add(
                        "Aggregate Grade", fineAggregates.Aggregate_Grade);
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
                        "Dose Type", db.Admixtures
                        .Where(admixture => admixture.Material_ID == ID)
                        .Select(admixture => admixture.Is_ByCWT).First() ? "CWT" : "YD");
                    break;

            }
            return dicReturn;
        }

         /// <summary>
        /// Return a dictionary with each cement material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public Dictionary<string, int> GetPrototypeCement (byte ID)
        {
            Dictionary<string, int> dicReturn = new Dictionary<string, int>();

            var materialQuery = from cementProportions in db.Cement_Proportions
                                join cement in db.Cements on cementProportions.Cement_ID equals cement.Cement_ID
                                join material in db.Materials on cement.Material_ID equals material.Material_ID
                                select new
                                {
                                    Name = material.Material_Name,
                                    Qty = Convert.ToInt32(cementProportions.Weight_of_Cement),
                                };

            foreach(var item in materialQuery)
            {
                dicReturn.Add(item.Name, item.Qty);
            }

            return dicReturn;
        }

       /// <summary>
        /// Return a dictionary with each SCM material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public Dictionary<string, int> GetPrototypeSCM (byte ID)
        {
            Dictionary<string, int> dicReturn = new Dictionary<string, int>();

            var materialQuery = from scmProportions in db.SCM_Proportions
                                join scm in db.SCMs on scmProportions.SCM_ID equals scm.SCM_ID
                                join material in db.Materials on scm.Material_ID equals material.Material_ID
                                select new
                                {
                                    Name = material.Material_Name,
                                    Qty = Convert.ToInt32(scmProportions.Weight_of_SCMs),
                                };

            foreach(var item in materialQuery)
            {
                dicReturn.Add(item.Name, item.Qty);
            }

            return dicReturn;
        }

        /// <summary>
        /// Return a dictionary with each CA material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public Dictionary<string, int> GetPrototypeCA (byte ID)
        {
            Dictionary<string, int> dicReturn = new Dictionary<string, int>();

            var materialQuery = from caProportions in db.CA_Proportions
                                join ca in db.Coarse_Aggregates on caProportions.CA_ID equals ca.CA_ID
                                join aggregate in db.Aggregates on ca.Aggregate_ID equals aggregate.Aggregate_ID
                                join material in db.Materials on aggregate.Material_ID equals material.Material_ID
                                select new
                                {
                                    Name = material.Material_Name,
                                    Qty = Convert.ToInt32(caProportions.Weight_of_CA),
                                };

            foreach(var item in materialQuery)
            {
                dicReturn.Add(item.Name, item.Qty);
            }

            return dicReturn;
        }

       /// <summary>
        /// Return a dictionary with each FN material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public Dictionary<string, int> GetPrototypeFN (byte ID)
        {
            Dictionary<string, int> dicReturn = new Dictionary<string, int>();

            var materialQuery = from fnProportions in db.FN_Proportions
                                join fn in db.Fine_Aggregates on fnProportions.FN_ID equals fn.FN_ID
                                join aggregate in db.Aggregates on fn.Aggregate_ID equals aggregate.Aggregate_ID
                                join material in db.Materials on aggregate.Material_ID equals material.Material_ID
                                select new
                                {
                                    Name = material.Material_Name,
                                    Qty = Convert.ToInt32(fnProportions.Weight_of_FN),
                                };

            foreach(var item in materialQuery)
            {
                dicReturn.Add(item.Name, item.Qty);
            }

            return dicReturn;
        }

        /// <summary>
        /// Return a dictionary with each Admixture material and its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public Dictionary<string, int> GetPrototypeAdmixture (byte ID)
        {
            Dictionary<string, int> dicReturn = new Dictionary<string, int>();

            var materialQuery = from admixProportions in db.Admixture_Proportions
                                join admixture in db.Admixtures on admixProportions.Admixture_ID equals admixture.Admixture_ID
                                join material in db.Materials on admixture.Material_ID equals material.Material_ID
                                select new
                                {
                                    Name = material.Material_Name,
                                    Qty = admixProportions.Admixture_Qty
                                };
            return dicReturn;
        }

        /// <summary>
        /// Return a key value pair with Water as the key and the qty as the value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public int GetPrototypeWater (byte ID)
        {
            int materialQuery = Convert.ToInt32(from prototype in db.Prototypes
                                select prototype.Weight_of_Water);

            return materialQuery;
        }

        /// <summary>
        /// Returns a double representing the water/ cement ratio
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns></returns>
        static public double GetWaterCMRatio (byte ID)
        {
            int accumulator = 0;
            foreach(var kvp in GetPrototypeCement(ID))
            {
                accumulator += kvp.Value;
            }
            foreach(var kvp in GetPrototypeSCM(ID))
            {
                accumulator += kvp.Value;
            }

            return Convert.ToDouble(GetPrototypeWater(ID) / accumulator);
        }

        /// <summary>
        /// Inserts a new record into the appropriate tables for a new material
        /// </summary>
        /// <param name="materialType">The type of material to make</param>
        static public void CreateNewMaterial(byte materialType)
        {
            // code to get last ID number
            var lastID = db.Materials.Max(material => material.Material_ID);

            // code to create new material
            Material mat = new Material();
            mat.Material_ID = ++lastID;
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
                    db.Cements.InsertOnSubmit(cem);
                    break;

                case SCM:
                    // code to get last ID number
                    lastSubID = db.SCMs.Max(scm => scm.SCM_ID);

                    // code to create new SCM
                    SCM sCM = new SCM();
                    sCM.SCM_ID = Convert.ToByte(lastSubID + 1);
                    db.SCMs.InsertOnSubmit(sCM);
                    break;
                case COARSE_AGGREGATE:
                case FINE_AGGREGATE:
                    // code to get last ID number
                    lastSubID = db.Aggregates.Max(aggregate => aggregate.Aggregate_ID);
                    lastAggID = db.Coarse_Aggregates.Max(ca => ca.CA_ID);

                    Aggregate agg = new Aggregate();
                    agg.Aggregate_ID = Convert.ToByte(lastSubID + 1);
                    db.Aggregates.InsertOnSubmit(agg);

                    if (materialType == COARSE_AGGREGATE)
                    {
                        lastAggID = db.Coarse_Aggregates.Max(fa => fa.CA_ID);
                        Coarse_Aggregate cAgg = new Coarse_Aggregate();
                        cAgg.CA_ID = Convert.ToByte(lastAggID + 1);
                        db.Coarse_Aggregates.InsertOnSubmit(cAgg);
                    }
                    else
                    {
                        lastAggID = db.Fine_Aggregates.Max(fa => fa.FN_ID);
                        Fine_Aggregate fAgg = new Fine_Aggregate();
                        fAgg.FN_ID = Convert.ToByte(lastAggID + 1);
                        db.Fine_Aggregates.InsertOnSubmit(fAgg);
                    }
                    break;
                case ADMIXTURE:
                    // code to get last ID number
                    lastSubID = db.Admixtures.Max(admixture => admixture.Admixture_ID);
                    Admixture admix = new Admixture();
                    admix.Admixture_ID = Convert.ToByte(lastSubID + 1);
                    db.Admixtures.InsertOnSubmit(admix);
                    break;
            }
            // submit all of the changes we made
            db.SubmitChanges();
        }

        /// <summary>
        /// Creates a new record for a Prototype
        /// </summary>
        static public void CreateNewPrototype()
        {
            var LastID = db.Prototypes.Max(prototype => prototype.Prototype_ID);

            Prototype proto = new Prototype();
            proto.Prototype_ID = Convert.ToByte(LastID + 1);
            db.Prototypes.InsertOnSubmit(proto);
            db.SubmitChanges();
        }

        /// <summary>
        /// Inserts a new record for a Trial Batch
        /// </summary>
        static public void CreateNewTrialBatch()
        {
            var lastID = db.Trial_Batches.Max(trialBatch => trialBatch.TB_ID);
            Trial_Batch trial = new Trial_Batch();
            trial.TB_ID = Convert.ToByte(lastID + 1);
            db.Trial_Batches.InsertOnSubmit(trial);
            db.SubmitChanges();
        }

        /// <summary>
        /// Inserts a new record for a Submittal
        /// </summary>
        static public void CreateNewSubmittal()
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
        static public void CreateNewMixDesign()
        {
            var lastID = db.Mix_Designs.Max(mix => mix.Mix_ID);

            Mix_Design mix_Design = new Mix_Design();
            mix_Design.Mix_ID = Convert.ToByte(lastID + 1);
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
            decimal relativeDensity,
            byte materialTypeID
            )
        {
            var recordToUpdate = (from material in db.Materials
                                 where material.Material_ID == materialID
                                 select material).First();
            recordToUpdate.Material_Name = materialName;
            recordToUpdate.Material_Source = materialSource;
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
            var recordToUpdate = (from cement in db.Cements
                                 where cement.Material_ID == materialID
                                 select cement).First();

            recordToUpdate.Cement_Type = cementType;
            db.SubmitChanges();
        }

        static public void UpdateSCM(
            byte materialID,
            string SCM_class
            )
        {
            var recordToUpdate = (from scm in db.SCMs
                                  where scm.Material_ID == materialID
                                  select scm).First();
            recordToUpdate.SCM_Class = SCM_class;
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
            recordToUpdate.Aggregate_Grade = aggregateGrade;
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
    }
}
