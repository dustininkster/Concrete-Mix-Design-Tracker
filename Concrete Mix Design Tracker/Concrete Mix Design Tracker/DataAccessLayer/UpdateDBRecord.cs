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
        /// Inserts a new record into the appropriate tables for a new material
        /// </summary>
        /// <param name="materialType">The type of material to make</param>
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
            int lookUpValue = (from agg in db.Aggregates
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
            int lookUpValue = (from agg in db.Aggregates
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
            db.SubmitChanges();
        }

        static public void UpdatePrototype(
            byte prototypeID,
            string concreteClass,
            bool isAirEntrained,
            double targetAir,
            int weightOfWater,
            List<MaterialField> cements,
            List<MaterialField> scms,
            List<MaterialField> cas,
            List<MaterialField> fns,
            List<MaterialField> admixs)
        {
            var recordToUpdate = (from prototype in db.Prototypes
                                  where prototype.Prototype_ID == prototypeID
                                  select prototype).First();
            recordToUpdate.Concrete_Class = concreteClass;
            recordToUpdate.Is_Air_Entrained = isAirEntrained;
            recordToUpdate.Target_Air = (decimal)targetAir;
            recordToUpdate.Weight_of_Water = weightOfWater;
            db.SubmitChanges();

            //Update proportions
            foreach (var cement in cements)
            {
                WriteCementProportions(prototypeID, cement.Material_ID, Int32.Parse(cement.QuantityOne));
                Console.WriteLine("Writing cement proportions {0} {1} {2}", prototypeID, cement.Material_ID, cement.QuantityOne);
            }

            foreach (var scm in scms)
                WriteSCMProportions(prototypeID, scm.Material_ID, Int32.Parse(scm.QuantityOne));
            foreach (var ca in cas)
                WriteSCMProportions(prototypeID, ca.Material_ID, Int32.Parse(ca.QuantityOne));
            foreach (var fn in fns)
                WriteFNProportions(prototypeID, fn.Material_ID, Int32.Parse(fn.QuantityOne));
            foreach (var admix in admixs)
                WriteAdmixProportions(prototypeID, admix.Material_ID, decimal.Parse(admix.QuantityOne));
            db.SubmitChanges();
        }

        static public void UpdateTrialBatch(
            int TB_ID,
            decimal slump,
            decimal actualAir,
            byte concreteTemp,
            byte ambientTemp,
            decimal measuredDensity,
            int compressiveStrength,
            string notes)
        {
            var recordToUpdate = (from trialBatch in db.Trial_Batches
                                  where trialBatch.TB_ID == TB_ID
                                  select trialBatch).First();
            recordToUpdate.Slump = slump;
            recordToUpdate.Actual_Air = actualAir;
            recordToUpdate.Concrete_Temperature = concreteTemp;
            recordToUpdate.Ambient_Temperature = ambientTemp;
            recordToUpdate.Measured_Density = measuredDensity;
            recordToUpdate.Compressive_Strength = compressiveStrength;
            recordToUpdate.Notes = notes;
            db.SubmitChanges();
        }

        /// <summary>
        /// Either adds or edits cement proportions
        /// </summary>
        /// <param name="prototypeID"></param>
        /// <param name="cementID"></param>
        /// <param name="weight"></param>
        static private void WriteCementProportions(int prototypeID, int cementID, int weight)
        {
            var prototypeIDQuery = from cementProportion in db.Cement_Proportions
                                   where cementProportion.Prototype_ID == prototypeID
                                   select cementProportion.Cement_ID;
            if (prototypeIDQuery.Contains(cementID))
            {
                var recordToUpdate = (from cementProportion in db.Cement_Proportions
                                      where cementProportion.Prototype_ID == prototypeID
                                      where cementProportion.Cement_ID == cementID
                                      select cementProportion).First();
                recordToUpdate.Weight_of_Cement = weight;
            }
            else
            {
                Cement_Proportion cp = new Cement_Proportion();
                cp.Prototype_ID = (from cementProportion in db.Cement_Proportions
                                   select cementProportion.Cement_Proportions_ID).Max() + 1;
                cp.Prototype_ID = prototypeID;
                cp.Cement_ID = cementID;
                cp.Weight_of_Cement = weight;
            }
            db.SubmitChanges();
        }

        /// <summary>
        /// Either adds or edits scm proportions
        /// </summary>
        /// <param name="prototypeID"></param>
        /// <param name="scmID"></param>
        /// <param name="weight"></param>
        static private void WriteSCMProportions(int prototypeID, int scmID, int weight)
        {
            var prototypeIDQuery = from scmProportion in db.SCM_Proportions
                                   where scmProportion.Prototype_ID == prototypeID
                                   select scmProportion.SCM_ID;
            if (prototypeIDQuery.Contains(scmID))
            {
                var recordToUpdate = (from scmProportion in db.SCM_Proportions
                                      where scmProportion.Prototype_ID == prototypeID
                                      where scmProportion.SCM_ID == scmID
                                      select scmProportion).First();
                recordToUpdate.Weight_of_SCMs = weight;
            }
            else
            {
                SCM_Proportion sp = new SCM_Proportion();
                sp.Prototype_ID = (from scmProportion in db.SCM_Proportions
                                   select scmProportion.SCM_Proportions_ID).Max() + 1;
                sp.Prototype_ID = prototypeID;
                sp.SCM_ID = scmID;
                sp.Weight_of_SCMs = weight;
            }
            db.SubmitChanges();
        }


        /// <summary>
        /// Either adds or edits ca proportions
        /// </summary>
        /// <param name="prototypeID"></param>
        /// <param name="caID"></param>
        /// <param name="weight"></param>
        static private void WriteCAProportions(int prototypeID, int caID, int weight)
        {
            var prototypeIDQuery = from caProportion in db.CA_Proportions
                                   where caProportion.Prototype_ID == prototypeID
                                   select caProportion.CA_ID;
            if (prototypeIDQuery.Contains(caID))
            {
                var recordToUpdate = (from caProportion in db.CA_Proportions
                                      where caProportion.Prototype_ID == prototypeID
                                      where caProportion.CA_ID == caID
                                      select caProportion).First();
                recordToUpdate.Weight_of_CA = weight;
            }
            else
            {
                CA_Proportion cap = new CA_Proportion();
                cap.Prototype_ID = (from caProportion in db.CA_Proportions
                                   select caProportion.CA_Proportions_ID).Max() + 1;
                cap.Prototype_ID = prototypeID;
                cap.CA_ID = caID;
                cap.Weight_of_CA = weight;
            }
            db.SubmitChanges();
        }

        /// <summary>
        /// Either adds or edits fn proportions
        /// </summary>
        /// <param name="prototypeID"></param>
        /// <param name="fnID"></param>
        /// <param name="weight"></param>
        static private void WriteFNProportions(int prototypeID, int fnID, int weight)
        {
            var prototypeIDQuery = from fnProportion in db.FN_Proportions
                                   where fnProportion.Prototype_ID == prototypeID
                                   select fnProportion.FN_ID;
            if (prototypeIDQuery.Contains(fnID))
            {
                var recordToUpdate = (from fnProportion in db.FN_Proportions
                                      where fnProportion.Prototype_ID == prototypeID
                                      where fnProportion.FN_ID == fnID
                                      select fnProportion).First();
                recordToUpdate.Weight_of_FN = weight;
            }
            else
            {
                FN_Proportion fnp = new FN_Proportion();
                fnp.Prototype_ID = (from fnProportion in db.FN_Proportions
                                   select fnProportion.FN_Proportions_ID).Max() + 1;
                fnp.Prototype_ID = prototypeID;
                fnp.FN_ID = fnID;
                fnp.Weight_of_FN = weight;
            }
            db.SubmitChanges();
        }

        /// <summary>
        /// Either adds or edits admix proportions
        /// </summary>
        /// <param name="prototypeID"></param>
        /// <param name="admixID"></param>
        /// <param name="weight"></param>
        static private void WriteAdmixProportions(int prototypeID, int admixID, decimal dose)
        {
            var prototypeIDQuery = from admixProportion in db.Admixture_Proportions
                                   where admixProportion.Prototype_ID == prototypeID
                                   select admixProportion.Admixture_ID;
            if (prototypeIDQuery.Contains(admixID))
            {
                var recordToUpdate = (from admixProportion in db.Admixture_Proportions
                                      where admixProportion.Prototype_ID == prototypeID
                                      where admixProportion.Admixture_ID == admixID
                                      select admixProportion).First();
                recordToUpdate.Admixture_Qty = dose;
            }
            else
            {
                Admixture_Proportion admixp = new Admixture_Proportion();
                admixp.Prototype_ID = (from admixProportion in db.Admixture_Proportions
                                   select admixProportion.Admixture_Proportions_ID).Max() + 1;
                admixp.Prototype_ID = prototypeID;
                admixp.Admixture_ID = admixID;
                admixp.Admixture_Qty = dose;
            }
            db.SubmitChanges();
        }
    }
}
 