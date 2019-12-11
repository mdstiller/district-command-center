using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ColossalFramework.UI;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class BtnViewPolicies : UIButton
    {
        public override void Awake()
        {
            text = "View District Policies";
            size = new Vector2(200, 25);
            normalFgSprite = "ButtonMenu";
            disabledFgSprite = "ButtonMenuDisabled";
            focusedFgSprite = "ButtonMenuFocused";
            hoveredFgSprite = "ButtonMenuHovered";
            pressedFgSprite = "ButtonMenuPressed";
            anchor = UIAnchorStyle.Right | UIAnchorStyle.Bottom;
        }

        public override void Start()
        {
            relativePosition = new Vector3((parent.size.x - size.x) - 10f, (parent.size.y - size.y) - 10f);
            eventClick += BtnViewPolicies_eventClick;
        }

        private void BtnViewPolicies_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            var districtWorldInfoPanel = UIView.GetAView().FindUIComponent<UIPanel>("(Library) DistrictWorldInfoPanel");

            if (districtWorldInfoPanel != null)
            {
                Debug.Log("Policies button found");
                districtWorldInfoPanel.SimulateClick();
            }
            else
            {
                Debug.LogError("Policies button not found.");
            }
        }
    }
}
