using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class Scrollbar : UIScrollbar
    {
        public override void Start()
        {
            width = 10f;
            height = parent.height;
            orientation = UIOrientation.Vertical;
            pivot = UIPivotPoint.BottomLeft;
            AlignTo((UIComponent)parent, UIAlignAnchor.TopRight);
            minValue = 0.0f;
            value = 0.0f;
            incrementAmount = (float)height;

            UISlicedSprite uiSlicedSprite1 = AddUIComponent<UISlicedSprite>();
            uiSlicedSprite1.relativePosition = (Vector3)Vector2.zero;
            uiSlicedSprite1.autoSize = true;
            uiSlicedSprite1.size = uiSlicedSprite1.parent.size;
            uiSlicedSprite1.fillDirection = UIFillDirection.Vertical;
            uiSlicedSprite1.spriteName = "ScrollbarTrack";

            trackObject = (UIComponent)uiSlicedSprite1;

            UISlicedSprite uiSlicedSprite2 = uiSlicedSprite1.AddUIComponent<UISlicedSprite>();
            uiSlicedSprite2.relativePosition = (Vector3)Vector2.zero;
            uiSlicedSprite2.fillDirection = UIFillDirection.Vertical;
            uiSlicedSprite2.autoSize = true;
            uiSlicedSprite2.width = uiSlicedSprite2.parent.width - 4f;
            uiSlicedSprite2.spriteName = "ScrollbarThumb";

            thumbObject = (UIComponent)uiSlicedSprite2;
        }
    }
}
