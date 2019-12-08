using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICities;
using ColossalFramework;
using UnityEngine;
using UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls;

namespace UpdateBuildingPrefix.Helpers
{
    public static class DistrictHelper
    {
        public static void UpdateDistrictLabelData(DistSumInfoLabel infoLabel, int districtId, string spriteName, District district)
        {
            switch (spriteName)
            {
                case "ToolbarIconElectricity":
                    {
                        UpdateLabelContent(infoLabel,
                            spriteName,
                            0,
                            district.GetElectricityCapacity(),
                            district.GetElectricityConsumption(),
                            "Electricity");

                        break;
                    }
                case "ToolbarIconGarbage":
                    {
                        UpdateLabelContent(infoLabel,
                            spriteName,
                            0,
                            district.GetGarbageCapacity(),
                            district.GetGarbageAccumulation(),
                            "Garbage");
                        break;
                    }
                case "ToolbarIconPolice":
                    {
                        UpdateLabelContent(infoLabel,
                            spriteName,
                            0,
                            district.GetCriminalCapacity(),
                            district.GetCriminalAmount(),
                            "Crime");

                        break;
                    }
                case "ToolbarIconHealthcare":
                    {
                        UpdateLabelContent(infoLabel,
                            spriteName,
                            0,
                            district.GetHealCapacity(),
                            district.GetSickCount(),
                            "Healthcare");

                        break;
                    }
                case "ToolbarIconWaterAndSewage":
                    {
                        UpdateLabelContent(infoLabel,
                            spriteName,
                            0,
                            district.GetWaterCapacity(),
                            district.GetWaterConsumption(),
                            "Water");

                        break;
                    }
                case "ToolbarIconEducation":
                    {
                        UpdateLabelContent(infoLabel,
                            spriteName,
                            0,
                            district.GetEducation1Capacity(),
                            district.GetEducation1Rate(),
                            "Elementary School");

                        break;
                    }
                case "ToolbarIconFireDepartment":
                    {
                        UpdateLabelContent(infoLabel,
                            spriteName,
                            0,
                            district.GetShelterCitizenCapacity(),
                            district.GetShelterCitizenNumber(),
                            "Fire Coverage");

                        break;
                    }
                case "InfoIconPollution":
                    {
                        UpdateLabelContent(infoLabel,
                            spriteName,
                            0,
                            district.GetGroundPollution(),
                            district.GetLandValue(),
                            "Land Value");

                        break;
                    }
                case "InfoIconResources":
                    {
                        UpdateLabelContent(infoLabel,
                            spriteName,
                            0,
                            0,
                            district.m_exportData.m_finalOre,
                            "Ore Exports");

                        break;
                    }

            }
        }
        private static void UpdateLabelContent(DistSumInfoLabel label, string spriteName, float minValue, float maxValue, float currValue, string tooltip)
        {
            label.prbStatusBar.minValue = minValue;
            label.prbStatusBar.maxValue = maxValue == 0 ? 1 : maxValue;
            label.prbStatusBar.value = currValue > maxValue ? maxValue : currValue;
            label.prbStatusBar.tooltip = $"{tooltip}: {currValue}/{maxValue}";
        }
    }
}
