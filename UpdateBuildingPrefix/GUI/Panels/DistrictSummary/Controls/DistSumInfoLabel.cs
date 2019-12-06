using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ColossalFramework.UI;
using UpdateBuildingPrefix.Helpers;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class DistSumInfoLabel : UILabel
    {
        public string InfoLabelText
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public override void Start()
        {
            //Debug.Log($"Running DistSumInfoLabel.Start()");
            size = new Vector2(250, 50);
            relativePosition = new Vector3(5f, 5f);
            text = "Instantiation Text.";
            padding = new RectOffset(5, 5, 0, 0);
                       
            //color = new Color32(120, 120, 120, 255);            
        }        
    }
}