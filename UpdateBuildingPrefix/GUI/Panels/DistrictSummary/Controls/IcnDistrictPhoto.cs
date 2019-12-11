using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using ColossalFramework;
using UnityEngine;
using UpdateBuildingPrefix.GUI.CustomTextures;
using UpdateBuildingPrefix.GUI.Helpers;
using UpdateBuildingPrefix.Helpers;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class IcnDistrictPhoto : UISprite
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

            //Debug.Log($"Icon Added: {name}");
        }

        public override void Start()
        {
            base.Start();

            eventClick += IcnDistrictPhoto_eventClick;
        }

        private void IcnDistrictPhoto_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            byte districtId = ((DistrictSummaryDetails)parent).DistrictId;
            District district = Singleton<DistrictManager>.instance.m_districts.m_buffer[districtId];
            CameraHelper.MoveCameraToDistrict(districtId);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
