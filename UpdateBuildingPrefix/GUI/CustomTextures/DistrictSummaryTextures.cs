using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UpdateBuildingPrefix.GUI.CustomTextures.TextureResources;
using UnityEngine;

namespace UpdateBuildingPrefix.GUI.CustomTextures
{
    public static class DistrictSummaryTextures
    {
        public static readonly Texture2D DistrictIcon;
        public static readonly Texture2D DistrictPolicyIcons;

        static DistrictSummaryTextures()
        {
            DistrictIcon = LoadDllResource("District.district-icon-3.png", 147, 92);
            DistrictIcon.name = "DCC.DistrictSummary.Details.Icon";

            DistrictPolicyIcons = LoadDllResource("District.PolicyIcons.policy-icons.png", 160, 40);
            DistrictPolicyIcons.name = "DCC.DistrictSummary.Details.Policies.Icons";
        }
    }
}
