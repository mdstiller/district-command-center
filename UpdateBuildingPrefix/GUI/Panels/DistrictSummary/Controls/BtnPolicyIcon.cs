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
    public class BtnPolicyIcon : UIButton
    {
        public string PolicyName { get; set; }      
        public DistrictPolicies.Policies Policy { get; set; }

        public override void Awake()
        {
            size = new Vector2(40f, 40f);            
        }
        public override void Start()
        {
            playAudioEvents = true;

            normalFgSprite = $"IconPolicy{PolicyName}";
            pressedFgSprite = $"IconPolicy{PolicyName}Pressed";
            hoveredFgSprite = $"IconPolicy{PolicyName}Hovered";
            disabledFgSprite = $"IconPolicy{PolicyName}Disabled";
            focusedFgSprite = $"IconPolicy{PolicyName}Focused";

            enabled = true;
            
            relativePosition = new Vector3(10f, 10f);

            eventClick += DistSumPolicyIcon_eventClick;
        }

        private void DistSumPolicyIcon_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            byte districtId = ((PnlDistPolicyIcons)parent).DistrictId;

            //Debug.Log($"Modifying Policy {Policy.ToString()} for district {DistrictParkHelper.FromDistrict(districtId).Name} :: Enum {Policy.ToString()}");

            if (DistrictManager.instance.m_districts.m_buffer[districtId].IsPolicySet(Policy))
            {
                Debug.Log($"Unsetting Policy {Policy.ToString()} for district {DistrictParkHelper.FromDistrict(districtId).Name}");
                DistrictManager.instance.UnsetDistrictPolicy(Policy, districtId);                
            }
            else
            {
                Debug.Log($"Setting Policy {Policy.ToString()} for district {DistrictParkHelper.FromDistrict(districtId).Name}");
                DistrictManager.instance.SetDistrictPolicy(Policy, districtId);
                
            }
            Invalidate();
        }

        public override void Update()
        {
            base.Update();            
        }
    }
}
