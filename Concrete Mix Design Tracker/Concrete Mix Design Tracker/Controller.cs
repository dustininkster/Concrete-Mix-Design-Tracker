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
                            where material.Material_Type.Contains(filter)
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
                         select material.Material_Type).Distinct();
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

        
    }
}
