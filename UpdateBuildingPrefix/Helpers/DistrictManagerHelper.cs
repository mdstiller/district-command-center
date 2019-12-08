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

            //Max array size is 128
            for (int i = 0; i < 128; i++)
            {
                //Debug.Log($"Getting details for district #{i}...");

                try
                {
                    string districtName = districtManager.GetDistrictName(i);

                    if (!districtName.Equals(""))
                    {
                        //Debug.Log($"Adding District Name: {districtName} (#{i})");
                        DistrictIds.Add(i);
                    }
                } catch
                {
                    //Debug.LogWarning($"No district exists at position {i}");
                }
            }
        }
    }
}
