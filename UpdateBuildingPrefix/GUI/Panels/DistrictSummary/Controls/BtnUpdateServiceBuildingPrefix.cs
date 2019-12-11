using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ColossalFramework.UI;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class BtnUpdateServiceBuildingPrefix : UIButton
    {
        private int InstanceId { get; set; } = 0;
        public override void Awake()
        {
            text = "Update Service Building Prefixes";
            size = new Vector2(275, 25);
            normalFgSprite = "ButtonMenu";
            disabledFgSprite = "ButtonMenuDisabled";
            focusedFgSprite = "ButtonMenuFocused";
            hoveredFgSprite = "ButtonMenuHovered";
            pressedFgSprite = "ButtonMenuPressed";
            anchor = UIAnchorStyle.Left | UIAnchorStyle.Bottom;
        }

        public override void Start()
        {
            relativePosition = new Vector3(10f, (parent.size.y - size.y) - 10f);
            eventClick += BtnUpdateServiceBuildingPrefix_eventClick; ;
        }

        private void BtnUpdateServiceBuildingPrefix_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            byte districtId = ((DistrictSummaryDetails)parent).DistrictId;

            DistrictManager.instance.GetDistrictArea(districtId, out int minx, out int minz, out int maxx, out int maxz);
            Debug.Log($"District Area: {minx}, {minz} | {maxx}, {maxz}");
            return;

            //Create the update prefix window...            
            var updateBuildingPrefix = UIView.GetAView().FindUIComponent<FrmUpdateBuildingPrefix>("DCC.UpdateBuildingPrefix");

            if (updateBuildingPrefix == null)
            {
                updateBuildingPrefix = (FrmUpdateBuildingPrefix)UIView.GetAView().AddUIComponent(typeof(FrmUpdateBuildingPrefix));
                updateBuildingPrefix.name = "DCC.UpdateBuildingPrefix";
            }

            updateBuildingPrefix.DistrictId = ((DistrictSummaryDetails)parent).DistrictId;

            if (updateBuildingPrefix.isVisible)
            {
                updateBuildingPrefix.isVisible = false;
            }
            else
            {
                updateBuildingPrefix.isVisible = true;
            }
        }
    }
}