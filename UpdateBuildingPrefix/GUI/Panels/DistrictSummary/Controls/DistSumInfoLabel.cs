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
    public class DistSumInfoLabel : UIPanel
    {
        public UISprite icnIcon { get; private set; }
        public UIProgressBar prbStatusBar { get; private set; }

        public override void Awake()
        {
            //Initial Info Label properties
            autoLayoutDirection = LayoutDirection.Horizontal;
            autoLayout = true;
            size = new Vector2(105f, 25f);
            padding = new RectOffset(0, 5, 0, 5);

            icnIcon = AddUIComponent<UISprite>();
            icnIcon.size = new Vector2(20f, 20f);
            icnIcon.relativePosition = new Vector3(0f, 0f);
            icnIcon.spriteName = "ToolbarIconTemp";

            prbStatusBar = AddUIComponent<UIProgressBar>();
            prbStatusBar.backgroundSprite = "LevelBarBackground";
            prbStatusBar.progressSprite = "LevelBarForeground";
            prbStatusBar.size = new Vector2(80f, 20f);
            prbStatusBar.relativePosition = new Vector3(0f, 0f);

            //Debug.Log($"Running DistSumInfoLabel.Start()");
            /*size = new Vector2(250, 30f);
            relativePosition = new Vector3(5f, 5f);
            padding = new RectOffset(5, 5, 0, 0);*/

            //color = new Color32(120, 120, 120, 255);            
        }

        public override void Update()
        {
            base.Update();            
        }
    }
}