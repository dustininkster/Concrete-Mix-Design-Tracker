using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete_Mix_Design_Tracker
{
    static class Controller
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

    }
}
