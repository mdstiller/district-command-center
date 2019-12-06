using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class DistrictSummaryList : UIScrollablePanel
    {
        public ScrollbarPanel ScrollbarPanel { get; private set; }
        public Scrollbar Scrollbar { get; private set; }
        public override void Start()
        {
            size = new Vector2(parent.width-10, parent.height-40);
            backgroundSprite = "GenericPanel";
            color = new Color32(20, 20, 20, 220);
            relativePosition = new Vector3(5f, 35f);
            autoLayoutStart = LayoutStart.TopLeft;
            autoLayoutPadding = new RectOffset(8, 8, 8, 8);
            autoLayoutDirection = LayoutDirection.Vertical;            
            autoLayout = true;
            clipChildren = true;

            Debug.Log("Adding Scrollbar Panel to Scrollable Panel");

            ScrollbarPanel = AddUIComponent<ScrollbarPanel>();

            Scrollbar = AddUIComponent<Scrollbar>();

            eventMouseWheel += ((component, param) => scrollPosition += new Vector2(0.0f, Mathf.Sign(param.wheelDelta) * -1f * Scrollbar.incrementAmount));

            //FitToContents();
        }
    }
}
