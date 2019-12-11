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
        public IcnDistrictPhoto icnDetailsIcon { get; private set; }
        public DistSumInfoLabelPanel pnlInfoLabels { get; private set; }
        public UILabel lblDistrictName { get;private set;}
        public BtnViewPolicies btnViewPolicies { get; private set; }
        public BtnUpdateServiceBuildingPrefix btnUpdateServiceBuildingPrefix { get; private set; }
        public PnlDistPolicyIcons pnlPolicyIcons { get; private set; }
        public byte DistrictId { get; set; }

        public override void Awake()
        {
            base.Awake();

            backgroundSprite = "MenuPanel";
            relativePosition = new Vector3(5f, 5f);
            autoLayoutDirection = LayoutDirection.Horizontal;
            wrapLayout = true;
            color = new Color32(255, 255, 255, 240);
            padding = new RectOffset(5, 5, 5, 5);

            lblDistrictName = AddUIComponent<UILabel>();

            //Add district icon
            icnDetailsIcon = AddUIComponent<IcnDistrictPhoto>();

            //Add district policies button shortcut
            btnViewPolicies = AddUIComponent<BtnViewPolicies>();

            //Add update prefix button
            btnUpdateServiceBuildingPrefix = AddUIComponent<BtnUpdateServiceBuildingPrefix>();

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

            pnlInfoLabels = AddUIComponent<DistSumInfoLabelPanel>();
            pnlInfoLabels.InfoLabelIcons = infoLabelIcons;

            pnlPolicyIcons = AddUIComponent<PnlDistPolicyIcons>();

            //Add district policy information

            //foreach(DistrictPolicies.Policies = DistrictPolicies.Policies.a
            //DistrictManager.instance.poli
            //District district = DistrictPark.FromDistrict(DistrictId).GetDistrictObject();
            //district.poli
            //string dps = district.m_servicePolicies.ToString();
            //Debug.Log($"DPS: {dps}");
            //pnlPolicyIcons = AddUIComponent<DistSumPolicyIconPanel>();            
            
            /*icon = AddUIComponent<DistSumPolicyIcon>();
            icon.PolicyName = "DCC.DistrictSummary.Details.Policies.Icons.BigBusiness";
            icon = AddUIComponent<DistSumPolicyIcon>();
            icon.PolicyName = "DCC.DistrictSummary.Details.Policies.Icons.NoBikes";
            icon = AddUIComponent<DistSumPolicyIcon>();
            icon.PolicyName = "DCC.DistrictSummary.Details.Policies.Icons.BookFair";*/
        }
        public override void Start()
        {
            pnlPolicyIcons.DistrictId = DistrictId;
            lblDistrictName.relativePosition = new Vector3(10f, 10f);
            lblDistrictName.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left;

            icnDetailsIcon.anchor = UIAnchorStyle.Left;            

            //btnUpdateServiceBuildingPrefix.eventClick += BtnUpdateServiceBuildingPrefix_eventClick;
        }

        private void BtnUpdateServiceBuildingPrefix_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            
            var FrmUpdateBuildingPrefix = (FrmUpdateBuildingPrefix)UIView.GetAView().AddUIComponent(typeof(FrmUpdateBuildingPrefix));

            //throw new NotImplementedException();
        }

        private void BtnViewPolicies_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            
            throw new NotImplementedException();
        }

        public override void Update()
        {
            base.Update();
            
            width = parent.width - ((DistrictSummaryList)parent).Scrollbar.width - 17f;
            lblDistrictName.text = $"{DistrictParkHelper.FromDistrict(DistrictId).Name} - Population: {DistrictParkHelper.FromDistrict(DistrictId).Population}";
            Invalidate();
        }
    }
}
