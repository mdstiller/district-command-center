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
                DETAILS_HEIGHT = 200,

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
        public DistrictSummaryList DistrictSummaryList { get; private set; } = new DistrictSummaryList();

        public List<DistrictSummaryDetails> DistrictSummaryDetails = new List<DistrictSummaryDetails>();

        public const int DEFAULT_MENU_X = 45;
        public const int DEFAULT_MENU_Y = 45;

        public override void Start()
        {
            DetermineProfile();
            OnUpdate();

            backgroundSprite = "GenericPanel";
            color = new Color32(0, 0, 0, 235);

            VersionLabel = AddUIComponent<VersionLabel>();
            DistrictSummaryList = AddUIComponent<DistrictSummaryList>();

            DistrictManager districtManagerHandle = Singleton<DistrictManager>.instance;

            DistrictManagerHelper.RefreshDistricts(districtManagerHandle);
           
            foreach (int districtId in DistrictManagerHelper.DistrictIds)
            {
                Debug.Log($"Adding panel for district #{districtId}.");

                string districtName = DistrictManagerHelper.GetName(districtId, districtManagerHandle);

                try
                {
                    Debug.Log($"Updating panel for {districtName} (#{districtId})");

                    DistrictSummaryDetails temp = DistrictSummaryList.AddUIComponent<DistrictSummaryDetails>();
                    temp.height = _activeProfile.DETAILS_HEIGHT;                   
                    temp.DistrictId = districtId;
                    //DistrictSummaryDetails.Add(temp);

                    temp.AddDistrictDetailComponents(i);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error: {e.Message}\r\n{e.StackTrace}");
                }
            }

            //for (ushort i = 0; i < _dmh.DistrictCount; i++)
                //UpdateInfoLabels(0);

            var dragHandler = new GameObject("DCC.MainMenu.DragHandler");
            dragHandler.transform.parent = transform;
            dragHandler.transform.localPosition = Vector3.zero;
            Drag = dragHandler.AddComponent<UIDragHandle>();
            Drag.enabled = true;            
            Drag.BringToFront();

            UpdateAllSizes();
            eventVisibilityChanged += OnVisibilityChange;

            _started = true;
        }
        private void DetermineProfile() {
            _activeProfile = SIZE_PROFILES[0];
        }

        private void UpdateInfoLabels(ushort districtId)
        {
            /*foreach(DistSumInfoLabel in )
            //for (int i = 0; i < 5; i++)
            //{
                int i = 0;
            string labelName = "";
            DistSumInfoLabel dsil = 
            

                if (dsil == null)
                {
                    Debug.LogError($"\r\nInfoLabel {labelName} was not found!");
                }
                else
                {
                    labelName = dsil.name;//$"DCC.DistrictSummary.Details.PanelDistrict{districtId.ToString()}.InfoLabelPanel.InfoLabel{i}";
                    Debug.Log($"Updating label:\r\n{labelName}");
                dsil.InfoLabelText = "Set some test text.";
                }
            //}*/
        }

        public void OnUpdate()
        {
            UpdatePosition(new Vector2(DEFAULT_MENU_X, DEFAULT_MENU_Y));

            if(_started)
            {
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
            DistrictSummaryList.height = _activeProfile.MENU_HEIGHT - 10;
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
