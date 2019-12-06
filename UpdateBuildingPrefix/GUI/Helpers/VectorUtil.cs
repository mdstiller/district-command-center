using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UpdateBuildingPrefix.GUI.Helpers
{
    public static class VectorUtil
    {
        public static void ClampRectToScreen(ref Rect rect, Vector2 resolution)
        {
            rect.x = Mathf.Clamp(rect.x, 0f, resolution.x - rect.width);
            rect.y = Mathf.Clamp(rect.y, 0f, resolution.y - rect.height);
        }
    }
}
