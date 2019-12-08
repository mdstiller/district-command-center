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
    public class DistSumDetailsIcon : UISprite
    {
        private const string DISTRICT_ICON = "DCC.DistrictSummary.Details.Icon";
        public override void Awake()
        {
            var spriteNames = new[] { DISTRICT_ICON };
            atlas = TextureUtil.GenerateLinearAtlas(
                DISTRICT_ICON,
                DistrictSummaryTextures.DistrictIcon,
                1, spriteNames);

            spriteName = DISTRICT_ICON;
            size = new Vector2(147, 92);
            relativePosition = new Vector3(8f, 48f);

            Debug.Log($"Icon Added: {name}");

        }
    }
}
