using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;
using UpdateBuildingPrefix.GUI.CustomTextures;
using UpdateBuildingPrefix.GUI.Helpers;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    class DistSumPolicyIcon : UISprite
    {
        private const int ICON_WIDTH = 40;
        private const int ICON_HEIGHT = 40;

        private const string DEFAULT_POLICY = "DCC.DistrictSummary.Details.Policies.Icons.NoBikes";
        private string _policyName = DEFAULT_POLICY;

        public string PolicyName {
            set {
                _policyName = value;

                Debug.Log($"Policy Name: {_policyName}");

                UpdateSprites();
            }
        }

        public override void Start()
        {
            string[] spriteNames = { "DCC.DistrictSummary.Details.Policies.Icons.AutomatedTolls",
                         "DCC.DistrictSummary.Details.Policies.Icons.BigBusiness",
                         "DCC.DistrictSummary.Details.Policies.Icons.NoBikes",
                         "DCC.DistrictSummary.Details.Policies.Icons.BookFair" };

            atlas = TextureUtil.GenerateLinearAtlas(
                "DCC.DistrictSummary.Details.Policies.Icons.Atlas",
                DistrictSummaryTextures.DistrictPolicyIcons,
                4,
                spriteNames);

            UpdateSprites();

            size = new Vector2(40.0f, 40.0f);
            relativePosition = new Vector3(10f, 200f);
        }       
    
        internal void UpdateSprites()
        {           
            spriteName = _policyName;
            m_SpriteName = _policyName;

            Invalidate();
        }
    }
}
