using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ColossalFramework;
using ICities;

namespace UpdateBuildingPrefix.Helpers
{
    public sealed class DistrictManagerHelper
    {
        public static List<int> DistrictIds { get; private set; } = new List<int>();

        public static District GetDistrict(int districtId, DistrictManager districtManager)
        {
            return districtManager.m_districts.m_buffer[districtId];
        }

        public static string GetName(int districtId, DistrictManager districtManager)
        {
            return districtManager.GetDistrictName(districtId);
        }

        /*public DistrictManager GetDistrictManagerInstance(ref DistrictManager districtManager)
        {
            DistrictManagerInstance = districtManager;
            return DistrictManagerInstance;
        }*/

        public static void RefreshDistricts(DistrictManager districtManager)
        {
            Debug.Log("Refreshing district details...");
            DistrictIds.Clear();

            for (byte i = 1; i < DistrictManager.MAX_DISTRICT_COUNT; i++)
            {
                if ((districtManager.m_districts.m_buffer[i].m_flags & global::District.Flags.Created) != 0)
                    DistrictIds.Add(i);
            }
        }
    }
}
