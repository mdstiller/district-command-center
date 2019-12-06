using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class DistSumInfoLabelPanel : UIPanel
    {        
        public int InfoLabelsCount { get; private set; }

        public override void Start()
        {
            relativePosition = new Vector3(0f, 0f);
            size = new Vector2(400, 40);
            name = parent.name + ".InfoLabelPanel";

            autoLayoutDirection = LayoutDirection.Vertical;
            wrapLayout = true;
            autoLayout = true;

            //Add InfoLabels to InfoLabelPanel
            for (int i = 0; i < 5; i++)
            {
                var dsil = AddUIComponent<UILabel>();
                dsil.text = "Text update during loop";
                //_infoLabels.Add(dsil);
            }            

            Debug.Log($"InfoLabel Count: {m_ChildComponents.Count}");

            for (int i = 0;i < m_ChildComponents.Count;i++)
            {
                UILabel temp = (UILabel)m_ChildComponents[i];
                Debug.Log($"info label text: {temp.text}");       
            }
        }
    }
}
