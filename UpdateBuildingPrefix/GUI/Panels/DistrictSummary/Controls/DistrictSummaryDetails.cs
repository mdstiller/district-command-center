using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using UpdateBuildingPrefix.Helpers;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class DistrictSummaryDetails : UIPanel
    {
        public DistSumDetailsIcon icnDetailsIcon { get; private set; }
        public DistSumInfoLabelPanel pnlInfoLabelPanel { get; private set; }
        public UILabel lblDistrictName { get;private set;}
        public UIButton btnViewPolicies { get; private set; }

        public string DistrictName { get; set; }
        public int DistrictId { get; set; }

        public override void Awake()
        {
            base.Awake();

            backgroundSprite = "MenuPanel";
            relativePosition = new Vector3(5f, 5f);
            autoLayoutDirection = LayoutDirection.Horizontal;
            color = new Color32(255, 255, 255, 240);
            padding = new RectOffset(5, 5, 5, 5);

            lblDistrictName = AddUIComponent<UILabel>();

            //Add district icon
            icnDetailsIcon = AddUIComponent<DistSumDetailsIcon>();

            //Add policies icon
            btnViewPolicies = AddUIComponent<UIButton>();

            //Add district information label panel
            string[] infoLabelIcons = { "ToolbarIconElectricity",
            "ToolbarIconGarbage",
            "ToolbarIconPolice",
            "ToolbarIconHealthcare",
            "ToolbarIconWaterAndSewage",
            "ToolbarIconEducation",
            "ToolbarIconFireDepartment",
            "InfoIconPollution",
            "InfoIconResources"};

            pnlInfoLabelPanel = AddUIComponent<DistSumInfoLabelPanel>();
            pnlInfoLabelPanel.InfoLabelIcons = infoLabelIcons;
         
            //AddUIComponent<DistSumPolicyIconPanel>();            
            /*icon = AddUIComponent<DistSumPolicyIcon>();
            icon.PolicyName = "DCC.DistrictSummary.Details.Policies.Icons.BigBusiness";
            icon = AddUIComponent<DistSumPolicyIcon>();
            icon.PolicyName = "DCC.DistrictSummary.Details.Policies.Icons.NoBikes";
            icon = AddUIComponent<DistSumPolicyIcon>();
            icon.PolicyName = "DCC.DistrictSummary.Details.Policies.Icons.BookFair";*/
        }
        public override void Start()
        {
            lblDistrictName.relativePosition = new Vector3(10f, 10f);
            lblDistrictName.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left;

            icnDetailsIcon.anchor = UIAnchorStyle.Left;

            btnViewPolicies.text = "View District Policies";
            btnViewPolicies.size = new Vector2(200, 25);
            btnViewPolicies.normalFgSprite = "ButtonMenu";
            btnViewPolicies.disabledFgSprite = "ButtonMenuDisabled";
            btnViewPolicies.focusedFgSprite = "ButtonMenuFocused";
            btnViewPolicies.hoveredFgSprite = "ButtonMenuHovered";
            btnViewPolicies.pressedFgSprite = "ButtonMenuPressed";
            btnViewPolicies.anchor = UIAnchorStyle.Right | UIAnchorStyle.Bottom;
            btnViewPolicies.relativePosition = new Vector3((size.x - btnViewPolicies.size.x) - 10f, (size.y - btnViewPolicies.size.y) - 10f);
            btnViewPolicies.eventClick += BtnViewPolicies_eventClick;
        }

        private void BtnViewPolicies_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            
            throw new NotImplementedException();
        }

        public override void Update()
        {
            base.Update();
            width = parent.width - ((DistrictSummaryList)parent).Scrollbar.width - 17f;
            lblDistrictName.text = $"{DistrictName} - Population: {Singleton<DistrictManager>.instance.m_districts.m_buffer[DistrictId].m_populationData.m_finalCount}";
            Invalidate();
        }
    }
}
