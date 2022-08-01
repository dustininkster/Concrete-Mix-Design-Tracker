 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Concrete_Mix_Tracker
{

    /// <summary>
    /// The Model class is a static class that contains all of the DAL (data access layer) for the desktop application.
    /// It is broken up into several files for organizational purposes.  This file focuses on methods to create new records
    /// in the database.
    /// </summary>
    static partial class DataAccess
    {
        /// <summary>
        /// Returns the properties of the cement table in a dictionary
        /// </summary>
        /// <param name="ID">Material_ID</param>
        /// <returns></returns>
        static private Dictionary<string, string> GetCementProperties(int ID)
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
        static private Dictionary<string, string> GetSCMProperties(int ID)
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
        static private Dictionary<string, string> GetCoarseAggregateProperties(int ID)
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
        static private Dictionary<string, string> GetFineAggregateProperties(int ID)
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
        static private Dictionary<string, string> GetAdmixtureProperties(int ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            var Query = (from admixture in db.Admixtures
                         where admixture.Material_ID == ID
                         select admixture).First();

            dicReturn.Add("Minimum Dosage", Query.Minimum_Dosage.ToString());
            dicReturn.Add("Maximum Dosage", Query.Maximum_Dosage.ToString());
            return dicReturn;
        }
        static private Dictionary<string, string> GetPrototypeProperties(int ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            var Query = (from prototype in db.Prototypes
                         where prototype.Prototype_ID == ID
                         select prototype).First();
            dicReturn.Add("Concrete Class", Query.Concrete_Class);
            dicReturn.Add("Air Entrained", Query.Is_Air_Entrained.ToString());
            dicReturn.Add("Target Air Content", Query.Target_Air.ToString());

            return dicReturn;
        }
        static private Dictionary<string, string> GetTrialBatchProperties(int ID)
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
            /*
            dicReturn.Add("Prototype Name", Query.PrototypeName);
            foreach (var item in GetPrototypeProperties(Query.PrototypeID))
                dicReturn.Add(item.Key, item.Value);
            */
            return dicReturn;
        }
        /*static private Dictionary<string, string> GetSubmittalProperties(int ID)
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
            /*
            dicReturn.Add("Owner Number", Query.Owner_Number.ToString());
            dicReturn.Add("Trial Batch", Query.TrialName);
            foreach (var item in GetTrialBatchProperties(Query.TBID))
                dicReturn.Add(item.Key, item.Value);
            *//*
            return dicReturn;
        }
    /**/
        static private Dictionary<string, string> GetMixDesignProperties(int ID)
        {
            Dictionary<string, string> dicReturn = new Dictionary<string, string>();
            var Query = (from mix in db.Mix_Designs
                         join sub in db.Submittals on mix.Prototype_ID equals sub.Prototype_ID
                         where mix.Mix_ID == ID
                         select mix).First();

            /*
            foreach (var item in GetSubmittalProperties(Query.Submittal_ID))
                dicReturn.Add(item.Key, item.Value);
            */
            return dicReturn;
        }


    }
}


