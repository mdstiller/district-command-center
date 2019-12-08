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
        public Scrollbar Scrollbar { get; private set; }
        public override void Awake()
        {            
            backgroundSprite = string.Empty;
            color = new Color32(20, 20, 20, 220);
            autoLayoutStart = LayoutStart.TopLeft;
            autoLayoutPadding = new RectOffset(8, 8, 8, 8);
            autoLayoutDirection = LayoutDirection.Vertical;            
            autoLayout = true;
            clipChildren = true;
            scrollWheelDirection = UIOrientation.Vertical;
            builtinKeyNavigation = true;
            anchor = UIAnchorStyle.All;            
        }

        public override void Start()
        {
            base.Start();

            Debug.Log("Adding Scrollbar Panel to Scrollable Panel");

            Scrollbar = AddUIComponent<Scrollbar>();
            verticalScrollbar = Scrollbar;

            eventMouseWheel += (component, param) => {
                Scrollbar.value += (int)param.wheelDelta * Scrollbar.incrementAmount;
            };

            size = new Vector2(parent.width-10, parent.height-40);
            relativePosition = new Vector3(5f, 35f);
        }

        public override void Update()
        {
            base.Update();
            //size = new Vector2(parent.size.x, parent.size.y);
            Invalidate();
        }
    }
}
