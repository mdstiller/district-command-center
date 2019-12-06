using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class ScrollbarPanel : UIPanel
    {
        public override void Start()
        {
            width = 10f;
            height = parent.height;
            autoLayoutDirection = LayoutDirection.Horizontal;
            autoLayoutStart = LayoutStart.TopLeft;
            autoLayoutPadding = new RectOffset(0, 0, 0, 0);
            autoLayout = true;
        }
    }
}
