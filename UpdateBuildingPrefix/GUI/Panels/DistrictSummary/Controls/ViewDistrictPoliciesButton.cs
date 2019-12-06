using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    class ViewDistrictPoliciesButton : UIButton
    {
        public override void Start()
        {
            size = new Vector2(100, 20);
            text = "Configure District Policies";
            base.Start();
        }
    }
}
