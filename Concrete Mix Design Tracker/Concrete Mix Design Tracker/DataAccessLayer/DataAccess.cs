using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineering_Database_View;

namespace Concrete_Mix_Tracker
{
    /// <summary>
    /// Structure to return quantity information from different types of materials
    /// </summary>
    struct MaterialField
    {
        public int MaterialType;
        public int Material_ID;
        public string Name;
        public string QuantityOne;
        public string QuantityTwo;
    }

    /// <summary>
    /// The Model class is a static class that contains all of the DAL (data access layer) for the desktop application.
    /// It is broken up into several files for organizational purposes.  This file focuses on methods to create new records
    /// in the database.
    /// </summary>
    static partial class DataAccess
    {
        /************
         *  FIELDS  *
         ************/
        const decimal UW_OF_WATER = (decimal)62.4;
        static MixDataDataContext db = new MixDataDataContext();
        const int
            MATERIALS = 0,
            PROTOTYPE = 1,
            TRIAL_BATCH = 2,
            SUBMITTAL = 4,
            MIX_DESIGN = 3;
        const int
            CEMENT = 0,
            SCM = 1,
            COARSE_AGGREGATE = 2,
            FINE_AGGREGATE = 3,
            ADMIXTURE = 4;

        // Data for Navigation
        //***********************************
        /// <summary>
        /// Returns a filtered list of field identifiers
        /// </summary>
        /// <param name="tab">index of the tab to reference</param>
        /// <param name="filter">narrow the search with this item</param>
        /// <returns>List<string></string></returns>
        static public List<string> GetListOfRecords(byte tab, string filter = "No Filter")
        {
            List<string> returnValues = new List<string>();
            // Switch breaks this down into code for each tab
            switch (tab)
            {
                // Code for the materials List
                case MATERIALS:
                    // With no filter selected
                    if (filter != "No Filter")
                    {
                        // Query to get list of formatted names
                        var materialsQuery = from materialType in db.Material_Types
                                             join material in db.Materials on materialType.Material_Type_ID equals material.Material_Type_ID
                                             where materialType.Type_Name == filter
                                             select string.Format("{0:000} {1,-20} {2,10}",material.Material_ID, material.Material_Source, material.Grade);

                        // Add to list
                        foreach (var material in materialsQuery)
                        {
                            returnValues.Add(material);
                        }

                        return returnValues;
                    }
                    // Without a filter
                    else
                    {
                        // Query to get list of formatted names without "where"
                            var materialsQuery = from material in db.Materials
                                             select string.Format("{0:000} {1,-20} {2,8}",material.Material_ID, material.Material_Source, material.Grade);


                        // Add to list
                        foreach (var material in materialsQuery)
                        {
                            returnValues.Add(material);
                        }

                        // return list
                        return returnValues;
                    }
                    // Code for prototype list
                case PROTOTYPE:
                    // with filter selected
                    if (filter != "No Filter")
                    {
                        // Query to get list of formatted names
                        var prototypeQuery = from prototype in db.Prototypes
                            where prototype.Concrete_Class == filter
                            select string.Format("{0:000} {1,20}", prototype.Prototype_ID, prototype.Prototype_Name);

                        // Add to list
                        foreach (var prototype in prototypeQuery)
                        {
                            returnValues.Add(prototype);
                        }
                        
                        // return list
                        return returnValues;
                    }
                    // no filter selected
                    else
                    {
                        // Query to get list of formatted names without "where" clause
                        var prototypeQuery = from prototype in db.Prototypes
                            select string.Format("{0:000} {1,20}", prototype.Prototype_ID, prototype.Prototype_Name);

                        // add to list
                        foreach (var prototype in prototypeQuery)
                        {
                            returnValues.Add(prototype);
                        }

                        // return list
                        return returnValues;
                    }
                    // Generate a list of formatted trial batch names
                case TRIAL_BATCH:
                    // filtered list
                    if (filter != "No Filter")
                    {
                        // Query to get list of formatted names without "where" clause
                        var trialBatchQuery = from trial_batch in db.Trial_Batches
                            join prototype in db.Prototypes on trial_batch.Prototype_ID equals prototype.Prototype_ID
                            where prototype.Concrete_Class == filter
                            select string.Format("{0:00} {1,20}",trial_batch.TB_ID , trial_batch.TB_Name);

                        // add to list
                        foreach(var trialBatch in trialBatchQuery)
                        {
                            returnValues.Add(trialBatch);
                        }

                        // return list
                        return returnValues;
                    }
                    // unfiltered list
                    else
                    {
                        // Query to get list of formatted names without "where" clause
                        var trialBatchQuery = from trial_batch in db.Trial_Batches
                            select string.Format("{0:000} {1,20}",trial_batch.TB_ID , trial_batch.TB_Name);

                        // add to list
                        foreach (var trialBatch in trialBatchQuery)
                        {
                            returnValues.Add(trialBatch);
                        }

                        // return list
                        return returnValues;
                    }
                    // Generate a list of submittal IDs
                case SUBMITTAL:
                    // with a filter
                    if (filter != "No Filter")
                    {
                        // Query to get a formatted list
                        var submittalQuery = from submittal in db.Submittals
                            join prototype in db.Prototypes on submittal.Prototype_ID equals prototype.Prototype_ID
                            where prototype.Concrete_Class == filter
                            select string.Format("{0:000} Owner {1,20}", submittal.Submittal_ID, submittal.Owner_Number.ToString());

                        // Add to list
                        foreach (var submittal in submittalQuery)
                        {
                            returnValues.Add(submittal);
                        }

                        // return list
                        return returnValues;
                    }
                    // without filter
                    else
                    {
                        // Query to get a formatted list of submittal names
                        var submittalQuery = from submittal in db.Submittals
                            select string.Format("{0:000} Owner {1,20}", submittal.Submittal_ID, submittal.Owner_Number.ToString());

                        // add to list
                        foreach (var submittal in submittalQuery)
                        {
                            returnValues.Add( submittal);
                        }

                        // return list
                        return returnValues;
                    }
                    // Get a formatted list of Mix Design names
                case MIX_DESIGN:
                    // with a filter
                    if (filter != "No Filter")
                    {
                        // Query to get a formatted list
                        var mixQuery = from mix_design in db.Mix_Designs
                            join prototype in db.Prototypes on mix_design.Prototype_ID equals prototype.Prototype_ID
                            where prototype.Concrete_Class == filter
                            select string.Format("{0:000} {1,20}",mix_design.Mix_ID, mix_design.Mix_Name);

                        // add to list
                        foreach (var mix in mixQuery)
                        {
                            returnValues.Add( mix);
                        }

                        // return list
                        return returnValues;
                    }
                    // unfiltered list
                    else
                    {
                        // Query to get a formatted list without "where" clause
                       var mixQuery = from mix_design in db.Mix_Designs
                            select string.Format("{0:000} {1,20}",mix_design.Mix_ID, mix_design.Mix_Name);
                        // add to list
                        foreach (var mix in mixQuery)
                        {
                            returnValues.Add(mix);
                        }

                        // return list
                        return returnValues;
                    }
                    // this is just here for intellisense
                default:
                    return null;
            }// switch
        }// GetList

        /// <summary>
        /// Returns a list of unique items to filter the Selection Listbox 
        /// </summary>
        /// <param name="tab">Tab to return the filters for</param>
        /// <returns>List<string></string></returns>
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

        // Queries for Materials Table
        //**********************************

        /// <summary>
        /// Returns a material name based on ID
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns>string</returns>
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
        /// <returns>string</returns>
        static public string GetMaterialSource(byte ID)
        {
            return (from material in db.Materials
                    where material.Material_ID == ID
                    select material.Material_Source).First();
        }

        /// <summary>
        /// Return a material grade based on ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>string</returns>
        static public string GetMaterialGrade(int ID)
        {
            return (from material in db.Materials
                    where material.Material_ID == ID
                    select material.Grade).First();
        }

        /// <summary>
        /// Return a material type based on ID
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns>int</returns>

        static public int GetMaterialType(int ID)
        {
            return (from material in db.Materials
                    where material.Material_ID == ID
                    select material.Material_Type_ID).First();
        }

        /// <summary>
        /// Return a material type based on Name
        /// </summary>
        /// <param name="ID">material_ID</param>
        /// <returns>string</returns>
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
        /// <returns>decimal</returns>
        static public decimal GetMaterialRelative_Density(byte ID)
        {
            var Q = (from material in db.Materials
                    where material.Material_ID == ID
                    select material.Relative_Density).First();
            return (decimal)Q;
        }
        
        // Queries for Materials Subtables
        //****************************************************
        /// <summary>
        /// Return a dictionary with each property and a string of its value
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns>Dictionary<string, string></returns>
        static public Dictionary<string, string> GetMaterialProperties (byte ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();

            // switch to find code based on the type of material returned
            switch (GetMaterialType(ID))
            {
                case CEMENT:
                    // Moved "grade" to main materials table but left cement table for future expansion
                        break;
                case SCM:
                    // Moved "grade" to main materials table but left scm table for future expansion
                    break;

                    // Find sub-material data for properties
                case COARSE_AGGREGATE:
                    // Query to return a single record of combined aggregate and coarse aggregate table information
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

                    // Add item to Dictionary
                    dicReturn.Add(
                        "Absorption", coarseAggregates.Absorption);
                    dicReturn.Add(
                        "Size", coarseAggregates.CA_Size);
                    dicReturn.Add(
                        "Unit Weight", coarseAggregates.CA_UW);
                    break;

                    // Find sub-material data for properties
                case FINE_AGGREGATE:
                    // Query to return a single record of combined aggregate and fine aggregate table information
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

                    // Add to dictionary
                    dicReturn.Add(
                        "Absorption", fineAggregates.Absorption);
                    dicReturn.Add(
                        "Fineness Modulus", fineAggregates.Fineness_Modulus);
                    break;

                    // Find sub-material data for properties
                case ADMIXTURE:
                    // Queries are built in to the add statements to keep you on your toes
                    dicReturn.Add(
                        "Minimum Dosage", db.Admixtures
                        .Where(admixture => admixture.Material_ID == ID)
                        .Select(admixture => admixture.Minimum_Dosage.ToString()).First());
                    dicReturn.Add(
                        "Maximum Dosage", db.Admixtures
                        .Where(admixture => admixture.Material_ID == ID)
                        .Select(admixture => admixture.Maximum_Dosage.ToString()).First());
                    break;
            } // switch

            // return the dictionary
            return dicReturn;
        }// GetMaterialsProperties

        // Queries for

        /// <summary>
        /// Returns the display name of the item (tab)
        /// </summary>
        /// <param name="tab">The tab (Material, Prototype, etc)</param>
        /// <param name="ID">ID of the type (Material_ID, Prototype_ID, etc</param>
        /// <returns>string</returns>
        static public string GetName(byte tab, byte ID)
        {
            // Queries to return an item name depending on the type of item (prototype, material, etc)
            switch (tab)
            {
                case MATERIALS:
                    return db.Materials.Where(material => material.Material_ID == ID).Select(item => item.Material_Source + " " + item.Grade).First();
                case PROTOTYPE:
                    return db.Prototypes.Where(prototype => prototype.Prototype_ID == ID).Select(item => item.Prototype_Name).First();
                case TRIAL_BATCH:
                    return db.Trial_Batches.Where(trialBatch => trialBatch.TB_ID == ID).Select(item => item.TB_Name).First();
                case SUBMITTAL:
                    return db.Submittals.Where(submittal => submittal.Submittal_ID == ID).Select(item => "Owner Submittal: " + item.Owner_Number).First();
                case MIX_DESIGN:
                    return db.Mix_Designs.Where(mix => mix.Mix_ID == ID).Select(item => item.Mix_Name).First();
                default:
                    return "None shall pass.";
            }


        }

       /// <summary>
        /// Returns the total cementitious materials by weight as an int
        /// </summary>
        /// <param name="ID">ID of prototype to total</param>
        /// <returns></returns>
        static public int GetTotalCementitious (byte ID)
        {
            int cementSum;
            int scmSum;

            var cementQuery = from prototypeCement in db.Cement_Proportions
                              where prototypeCement.Prototype_ID == ID
                              select prototypeCement.Weight_of_Cement;
            var scmQuery = from prototypeSCM in db.SCM_Proportions
                           where prototypeSCM.Prototype_ID == ID
                           select prototypeSCM.Weight_of_SCMs;
            if (cementQuery.Count() == 0)
                cementSum = 0;
            else
                cementSum = (int)cementQuery.Sum();
            if (scmQuery.Count() == 0)
                scmSum = 0;
            else
                scmSum = (int)scmQuery.Sum();

            return cementSum + scmSum;

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
            decimal wcm = 0;
            if (GetTotalCementitious(ID) != 0)
                wcm = (decimal)((decimal)GetPrototypeWater(ID) / (decimal)GetTotalCementitious(ID));
            return wcm;
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

        static public Dictionary<string, string> GetProperties(int tab, int ID)
        {
            if (tab == MATERIALS)
            {
                int materialType = (from material in db.Materials
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
                    /*
                case SUBMITTAL:
                    return GetSubmittalProperties(ID);
                    /**/
                case MIX_DESIGN:
                    return GetMixDesignProperties(ID);
                default:
                    return null;
            }
        }

        static public List<string> GetPrototypesWithoutMaterial(byte materialID, byte type)
        {
            List<string> lstReturn = new List<string>();
            IEnumerable<int> remaining;
            IEnumerable<int> exclude;

            switch (type)
            {
                case CEMENT:
                    int cementID = db.Cements.Where(cement => cement.Material_ID == materialID).Select(cement => cement.Cement_ID).First();
                    exclude = (from cementProportion in db.Cement_Proportions
                                where cementProportion.Cement_ID == cementID
                                select cementProportion.Prototype_ID).Distinct();
                    remaining = db.Prototypes.Select(prototype => prototype.Prototype_ID).Except(exclude);
                    break;
                case SCM:
                    int scmID = db.SCMs.Where(scm => scm.Material_ID == materialID).Select(scm => scm.SCM_ID).First();
                    exclude = (from scmProportion in db.SCM_Proportions
                                where scmProportion.SCM_ID == scmID
                                select scmProportion.Prototype_ID).Distinct();
                    remaining = db.Prototypes.Select(prototype => prototype.Prototype_ID).Except(exclude);
                    break;
                case COARSE_AGGREGATE:
                    int caID = (from aggregate in db.Aggregates
                                join ca in db.Coarse_Aggregates on aggregate.Aggregate_ID equals ca.Aggregate_ID
                                where aggregate.Material_ID == materialID
                                select ca.CA_ID).First();

                    exclude = (from caProportion in db.CA_Proportions
                               where caProportion.CA_ID == caID
                               select caProportion.Prototype_ID).Distinct();
                    remaining = db.Prototypes.Select(prototype => prototype.Prototype_ID).Except(exclude);

                    break;
                case FINE_AGGREGATE:
                    int fnID = (from aggregate in db.Aggregates
                                join fn in db.Fine_Aggregates on aggregate.Aggregate_ID equals fn.Aggregate_ID
                                where aggregate.Material_ID == materialID
                                select fn.FN_ID).First();

                    exclude = (from fnProportion in db.FN_Proportions
                               where fnProportion.FN_ID == fnID
                               select fnProportion.Prototype_ID).Distinct();
                    remaining = db.Prototypes.Select(prototype => prototype.Prototype_ID).Except(exclude);
                    break;
                case ADMIXTURE:
                    int admixID = db.Admixtures.Where(admix => admix.Material_ID == materialID).Select(admix => admix.Admixture_ID).First();
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

        static public void AddMaterialToPrototype(int prototypeID, int materialID)
        {
            int aggID;

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

        /// <summary>
        /// Returns the prototype associated with a subtable ID: Trial Batch, Submittal, Mix Design
        /// </summary>
        /// <param name="ID">The subtable ID: TB_ID, Submittal_ID, or Mix_ID</param>
        /// <param name="tab">The type of ID (which tab it came from)</param>
        /// <returns>int representing the prototype ID</returns>
        static public int GetAssociatedPrototypeID(int ID, int tab)
        {
            // tab is the type of ID we are swapping from
            switch (tab)
            {
                // if we are switching from the trial batch ID do a query that returns the prototype ID
                case TRIAL_BATCH:
                    return (from trial in db.Trial_Batches
                                where trial.TB_ID == ID
                                select trial.Prototype_ID).First();
                // if we are switching from the submittal ID do a query that returns the prototype ID
                /*case SUBMITTAL:
                    return (from sub in db.Submittals
                            where sub.Submittal_ID == ID
                            select sub.Prototype_ID).First();/**/
                // if we are switching from the submittal ID do a query that returns the prototype ID
                case MIX_DESIGN:
                    return (from mix in db.Mix_Designs
                            where mix.Mix_ID == ID
                            select mix.Prototype_ID).First();
                // this is here to shut intellisense up
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Returns the TrialBatch ID associated with a Prototype ID
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns>An int reperesenting the ID of the Trial Batch</returns>
        static public int GetAssociatedTrialBatchID(int ID)
        {
            return (from trial in db.Trial_Batches
                    where trial.Prototype_ID == ID
                    select trial.TB_ID).First();
 
        }

        /// <summary>
        /// Return the Submittal ID associated with a Prototype ID
        /// </summary>
        /// <param name="ID">prototype_ID</param>
        /// <returns>An int representing the ID of the Submittal</returns>
        /*static public int GetAssociatedSubmittalID(int ID)
        {
            return (from submittal in db.Submittals
                    where submittal.Prototype_ID == ID
                    select submittal.Submittal_ID).First();
        }/**/
    }
}
