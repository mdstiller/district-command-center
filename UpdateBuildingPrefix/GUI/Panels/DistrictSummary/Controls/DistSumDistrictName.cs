using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class DistSumDistrictName : UILabel
    {
        public override void Start()
        {
            relativePosition = new Vector3(10f, 10f);
            text = "District Name";
        }
    }
}
