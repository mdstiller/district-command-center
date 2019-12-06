using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    class DistSumPolicyIconPanel : UIPanel
    {
        public override void Start()
        {
            backgroundSprite = "MenuPanel2";
            size = new Vector2(160, 50);
            relativePosition = new Vector3(5f, 5f);
            autoLayoutDirection = LayoutDirection.Horizontal;
            autoLayout = true;

            string[] spriteNames = { "DCC.DistrictSummary.Details.Policies.Icons.AutomatedTolls",
                         "DCC.DistrictSummary.Details.Policies.Icons.BigBusiness",
                         "DCC.DistrictSummary.Details.Policies.Icons.NoBikes",
                         "DCC.DistrictSummary.Details.Policies.Icons.BookFair" };
            for (int i = 0; i < 4; i++)
            {
                DistSumPolicyIcon icon = AddUIComponent<DistSumPolicyIcon>();
                icon.PolicyName = spriteNames[i];
            }
        }
    }
}
