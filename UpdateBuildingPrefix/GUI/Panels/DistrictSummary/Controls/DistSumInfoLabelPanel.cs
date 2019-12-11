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
    public class DistSumInfoLabelPanel : UIPanel
    {
        public int InfoLabelsCount { get; private set; }
        public string[] InfoLabelIcons { get; set; }
        public byte DistrictId { get; set; }
        public List<DistSumInfoLabel> InfoLabels { get; private set; } = new List<DistSumInfoLabel>();

        public override void Awake()
        {
            autoLayoutDirection = LayoutDirection.Vertical;
            wrapLayout = true;
            autoLayout = true;
            size = new Vector2(600, 60);
            anchor = UIAnchorStyle.Right;
            autoSize = true;
        }

        public override void Start()
        {
            base.Start();

            byte DistrictId = ((DistrictSummaryDetails)parent).DistrictId;
            relativePosition = new Vector3(160f, 48f);
        }

        public override void Update()
        {
            base.Update();

            UpdateInfoPanel();

            width = parent.width - position.x - 17f;
            Invalidate();
        }

        internal void UpdateInfoPanel()
        {

            if (InfoLabels.Count == 0)
            {
                Debug.Log("Updating info panel labels from string array...");

                for (int i = 0; i < InfoLabelIcons.Length; i++)
                {
                    var dsil = AddUIComponent<DistSumInfoLabel>();

                    //Debug.Log($"Attempting to set icon to {InfoLabelIcons[i]}");

                    dsil.icnIcon.spriteName = InfoLabelIcons[i];
                    InfoLabels.Add(dsil);
                }
            }
            
            District district = DistrictManager.instance.m_districts.m_buffer[DistrictId];

            string spriteName = "";

            //Debug.Log("Updating info panel labels from district...");
            foreach (DistSumInfoLabel infoLabel in InfoLabels)
            {
                spriteName = infoLabel.icnIcon.spriteName;

                DistrictHelper.UpdateDistrictLabelData(infoLabel, DistrictId, spriteName, district);
            }
        }
    }
}