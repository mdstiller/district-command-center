using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class DistrictSummaryDetails : UIPanel
    {
        public DistSumDetailsIcon DetailsIcon { get; private set; }
        public DistSumInfoLabelPanel InfoLabelPanel { get; private set; }

        public string DistrictName { get; private set; }
        public int DistrictId { get; set; }

        public override void Start()
        {
            backgroundSprite = "MenuPanel";
            relativePosition = new Vector3(5f, 5f);
            autoLayoutDirection = LayoutDirection.Horizontal;
            color = new Color32(255, 255, 255, 100);
            padding = new RectOffset(-5, -5, -5, -5);
            autoLayout = true;

            AddUIComponent<UILabel>();

            //AddUIComponent<DistSumPolicyIconPanel>();            
            /*icon = AddUIComponent<DistSumPolicyIcon>();
            icon.PolicyName = "DCC.DistrictSummary.Details.Policies.Icons.BigBusiness";
            icon = AddUIComponent<DistSumPolicyIcon>();
            icon.PolicyName = "DCC.DistrictSummary.Details.Policies.Icons.NoBikes";
            icon = AddUIComponent<DistSumPolicyIcon>();
            icon.PolicyName = "DCC.DistrictSummary.Details.Policies.Icons.BookFair";*/
        }

        public void AddDistrictDetailComponents(ushort districtId)
        {
            Debug.Log($"DistrictSummaryDetails for district (ID:{DistrictId}) added.");
            DistrictId = districtId;

            DistrictNamePanel.text = "Update after getting district name.";

            DetailsIcon = AddUIComponent<DistSumDetailsIcon>();
            //InfoLabelPanel = AddUIComponent<DistSumInfoLabelPanel>();

            /*Debug.Log($"Count of InfoLabels from DSD Panel: {InfoLabelPanel.InfoLabels.Count}");

            foreach (DistSumInfoLabel infoLabel in InfoLabelPanel.InfoLabels)
            {
                Debug.Log($"InfoLabel old text: {infoLabel.text}");
                infoLabel.text = "Update during add panel components";
                Debug.Log($"InfoLabel new text: {infoLabel.text}");
                infoLabel.Invalidate();
            }*/
        }

        public void RefreshPanel(ushort districtId)
        {
            

            Invalidate();
        }
    }
}
