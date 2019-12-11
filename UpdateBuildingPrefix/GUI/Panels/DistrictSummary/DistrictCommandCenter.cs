using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using ColossalFramework;
using UnityEngine;
using ICities;
using UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls;
using UpdateBuildingPrefix.GUI.CustomTextures;
using UpdateBuildingPrefix.GUI.Helpers;
using UpdateBuildingPrefix.Helpers;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary
{
    public class DistrictCommandCenter : UIPanel
    {
        public class SizeProfile
        {
            public int DETAILS_HEIGHT { get; set; }
            public int MENU_WIDTH { get; set; }
            public int MENU_HEIGHT { get; set; }
            public int TOP_BORDER { get; set; }
        }
        public static readonly SizeProfile[] SIZE_PROFILES =
        {
            new SizeProfile
            {
                TOP_BORDER = 25,
                DETAILS_HEIGHT = 400,

                MENU_WIDTH = 1000,
                MENU_HEIGHT = 700
            }
        };

        //Member private variables
        private SizeProfile _activeProfile;
        private bool _started;


        //Properties
        public UILabel VersionLabel { get; private set; }
        public UIDragHandle Drag { get; private set; }
        public UIResizeHandle Resize { get; private set; }
        public UIButton Close { get; private set; }
        public UISprite ApplicationLogo { get; private set; }
        public DistrictSummaryList DistrictSummaryList { get; private set; } = new DistrictSummaryList();

        public List<DistrictSummaryDetails> DistrictSummaryDetails = new List<DistrictSummaryDetails>();

        public const int DEFAULT_MENU_X = 20;
        public const int DEFAULT_MENU_Y = 20;

        public override void Awake()
        {
            base.Awake();

            DetermineProfile();

            backgroundSprite = "GenericPanel";
            color = new Color32(0, 0, 0, 255);

            ApplicationLogo = AddUIComponent<UISprite>();
            VersionLabel = AddUIComponent<VersionLabel>();
            DistrictSummaryList = AddUIComponent<DistrictSummaryList>();

            var dragHandler = new GameObject("DCC.MainMenu.DragHandler");
            dragHandler.transform.parent = transform;
            dragHandler.transform.localPosition = Vector3.zero;
            Drag = dragHandler.AddComponent<UIDragHandle>();
            Drag.enabled = true;            
            Drag.BringToFront();

            var resizeHandler = new GameObject("DCC.MainMenu.ResizeHandler");
            resizeHandler.transform.parent = transform;
            resizeHandler.transform.localPosition = position;
            Resize = resizeHandler.AddComponent<UIResizeHandle>();
            Resize.enabled = true;
            Resize.backgroundSprite = "ColorPickerIndicator";
            Resize.BringToFront();

            Close = AddUIComponent<UIButton>();
            Close.normalFgSprite = "buttonclose";
            Close.hoveredFgSprite = "buttonclosehover";
            Close.pressedFgSprite = "buttonclosepressed";
            Close.anchor = UIAnchorStyle.Top | UIAnchorStyle.Right;

            var spriteNames = new[] { TextureResources.DistrictCommandCenterLogo.name };
            ApplicationLogo.atlas = TextureUtil.GenerateLinearAtlas(
                TextureResources.DistrictCommandCenterLogo.name,
                TextureResources.DistrictCommandCenterLogo,
                1, spriteNames);

            ApplicationLogo.spriteName = TextureResources.DistrictCommandCenterLogo.name;
            ApplicationLogo = AddUIComponent<UISprite>();
            ApplicationLogo.size = new Vector2(600, 111);
            ApplicationLogo.SendToBack();

            UpdateAllSizes();
            eventVisibilityChanged += OnVisibilityChange;
            Close.eventClick += Close_eventClick;

            _started = true;
        }

        private void Close_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            isVisible = false;
        }

        public override void Start()
        {
            base.Start();
            Debug.Log("Starting District Control Center Panel...");

            OnUpdate();

            Close.relativePosition = new Vector3(size.x - Close.size.x - 10f, 5f);
            VersionLabel.relativePosition = new Vector3(10f, 5f);
            
            foreach (DistrictParkHelper dp in DistrictParkHelper.GetAllDistrictParks())
            {
                Debug.Log($"Adding panel for district #{dp.Name}.");

                try
                {
                    Debug.Log($"Updating panel for {dp.Name} (#{dp.District})");

                    DistrictSummaryDetails temp = DistrictSummaryList.AddUIComponent<DistrictSummaryDetails>();
                    temp.height = _activeProfile.DETAILS_HEIGHT;
                    temp.anchor = UIAnchorStyle.Left;
                    temp.DistrictId = dp.District;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error: {e.Message}\r\n{e.StackTrace}");
                }
            }            
        }

        private void DistrictCommandCenter_eventSizeChanged(UIComponent component, Vector2 value)
        {
            if (_started)
            {
                DistrictSummaryList.size = new Vector2(value.x - 10, value.y - 40);                
            }
        }

        private void DetermineProfile() {
            _activeProfile = SIZE_PROFILES[0];
        }

        public void OnUpdate()
        {
            UpdatePosition(new Vector2(DEFAULT_MENU_X, DEFAULT_MENU_Y));

            if(_started)
            {
                ApplicationLogo.relativePosition = new Vector3(50f, -111f);
                UpdateAllSizes();
                Invalidate();
            }
        }
         
        protected override void OnPositionChanged()
        {
            base.OnPositionChanged();
        }

        private void OnVisibilityChange(UIComponent component, bool value)
        {
            VersionLabel.enabled = value;            
        }
       
        public override void OnDestroy()
        {
            eventVisibilityChanged -= OnVisibilityChange;
        }

        private void UpdateAllSizes()
        {
            UpdateSize();
            UpdateDragSize();
            UpdateScrollPanel();
        }

        private void UpdateSize()
        {
            width = _activeProfile.MENU_WIDTH;
            height = _activeProfile.MENU_HEIGHT;
        }

        private void UpdateDragSize()
        {
            Drag.width = width;
            Drag.height = _activeProfile.TOP_BORDER;
        }

        private void UpdateScrollPanel()
        {
            //DistrictSummaryList.relativePosition = new Vector3(5f, 45f);
            //DistrictSummaryList.width = _activeProfile.MENU_WIDTH - 10;
            DistrictSummaryList.height = _activeProfile.MENU_HEIGHT - 20;
            DistrictSummaryList.Invalidate();
        }

        public void UpdatePosition(Vector2 pos)
        {
            Debug.Log($"Setting main menu position to [{pos.x}, {pos.y}]");

            Rect rect = new Rect(pos, new Vector2(_activeProfile.MENU_WIDTH, _activeProfile.MENU_HEIGHT));
            Debug.Log($"Setting new viewport size: {rect.ToString()}");

            Vector2 resolution = UIView.GetAView().GetScreenResolution();
            Debug.Log($"Current resolution: [{resolution.x},{resolution.y}]");

            VectorUtil.ClampRectToScreen(ref rect, resolution);
            absolutePosition = rect.position;
            Invalidate();
        }
    }
}
