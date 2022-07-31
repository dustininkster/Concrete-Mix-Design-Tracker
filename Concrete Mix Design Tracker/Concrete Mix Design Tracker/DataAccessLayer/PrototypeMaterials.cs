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
                                    MType = CEMENT,
                                    MaterialID = cementProportions.Cement_ID,
                                    Name = material.Material_Source + " " + material.Grade,
                                    Weight = Convert.ToInt32(cementProportions.Weight_of_Cement),
                                    Volume = cementProportions.Weight_of_Cement/ (material.Relative_Density * UW_OF_WATER)
                                };

            foreach(var item in materialQuery)
            {
                mfReturn.Add(new MaterialField { 
                    MaterialType = item.MType,
                    Material_ID = item.MaterialID,
                    Name = item.Name, 
                    QuantityOne = string.Format("{0:####}", item.Weight), 
                    QuantityTwo = string.Format("{0:F2}",item.Volume)});
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
                                    MType = SCM,
                                    MaterialID = scmProportions.SCM_ID,
                                    Name = material.Material_Source + " " + material.Grade,
                                    Weight = Convert.ToInt32(scmProportions.Weight_of_SCMs),
                                    Volume = scmProportions.Weight_of_SCMs/(UW_OF_WATER * material.Relative_Density)
                                };

            foreach(var item in materialQuery)
            {
                mfReturn.Add(new MaterialField { 
                    MaterialType = item.MType,
                    Material_ID = item.MaterialID,
                    Name = item.Name, 
                    QuantityOne = string.Format("{0:####}", item.Weight), 
                    QuantityTwo = string.Format("{0:F2}",item.Volume)});
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
                                    MType = COARSE_AGGREGATE,
                                    MaterialID = caProportions.CA_ID,
                                    Name = material.Material_Source + " " + material.Grade,
                                    Weight = Convert.ToInt32(caProportions.Weight_of_CA),
                                    Volume = caProportions.Weight_of_CA / (UW_OF_WATER * material.Relative_Density)
                                };

            foreach(var item in materialQuery)
            {
                mfReturn.Add(new MaterialField { 
                    MaterialType = item.MType,
                    Material_ID = item.MaterialID,
                    Name = item.Name, 
                    QuantityOne = string.Format("{0:####}", item.Weight), 
                    QuantityTwo = string.Format("{0:F2}",item.Volume)});
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
                                    MType = FINE_AGGREGATE,
                                    MaterialID = fnProportions.FN_ID,
                                    Name = material.Material_Source + " " + material.Grade,
                                    Weight = Convert.ToInt32(fnProportions.Weight_of_FN),
                                    Volume = fnProportions.Weight_of_FN / (UW_OF_WATER * material.Relative_Density)
                                };

            foreach(var item in materialQuery)
            {
                mfReturn.Add(new MaterialField { 
                    MaterialType = item.MType,
                    Material_ID = item.MaterialID,
                    Name = item.Name, 
                    QuantityOne = string.Format("{0:####}", item.Weight), 
                    QuantityTwo = string.Format("{0:F2}",item.Volume)});
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
                                    MType = ADMIXTURE,
                                    MaterialID = admixProportions.Admixture_ID,
                                    Name = material.Material_Source + " " + material.Grade,
                                    cwt = admixProportions.Admixture_Qty,
                                    yd = admixProportions.Admixture_Qty * GetCWT(ID) 
                                };
            foreach (var item in materialQuery)
                mfReturn.Add(new MaterialField { 
                    MaterialType = item.MType,
                    Material_ID = item.MaterialID,
                    Name = item.Name, 
                    QuantityOne = string.Format("{0:F2}",item.cwt), 
                    QuantityTwo = string.Format("{0:F2}",item.yd)});
            return mfReturn;
        }

 
    }
}
 