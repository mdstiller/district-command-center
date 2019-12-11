using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    class FrmUpdateBuildingPrefix : UIPanel
    {
        public UILabel lblCaption { get; private set; }
        public UIButton btnClose { get; private set; }
        public UITextField txtPrefix { get; private set; }
        public UIButton btnOk { get; private set; }
        public UIButton btnCancel { get; private set; }
        public UIDragHandle Drag { get; private set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        

        public override void Awake()
        {
            size = new Vector2(500, 150);
            backgroundSprite = "MenuPanel";

            lblCaption = AddUIComponent<UILabel>();
            lblCaption.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left;
            lblCaption.size = new Vector2(200, 25);

            txtPrefix = AddUIComponent<UITextField>();
            txtPrefix.size = new Vector2(190, 30);
            txtPrefix.normalBgSprite = "TextFieldPanel";
            txtPrefix.hoveredBgSprite = "TextFieldPanelHovered";
            txtPrefix.focusedBgSprite = "TextFieldPanel";
            txtPrefix.selectionSprite = "EmptySprite";
            txtPrefix.color = Color.black;
            txtPrefix.cursorBlinkTime = 0.45f;
            txtPrefix.cursorWidth = 1;
            txtPrefix.text = "Default...";
            txtPrefix.isInteractive = true;
            txtPrefix.readOnly = false;
            txtPrefix.enabled = true;
            txtPrefix.builtinKeyNavigation = true;

            btnOk = AddUIComponent<UIButton>();
            btnOk.text = "Update";
            btnOk.size = new Vector2(200, 25);
            btnOk.normalFgSprite = "ButtonMenu";
            btnOk.disabledFgSprite = "ButtonMenuDisabled";
            btnOk.focusedFgSprite = "ButtonMenuFocused";
            btnOk.hoveredFgSprite = "ButtonMenuHovered";
            btnOk.pressedFgSprite = "ButtonMenuPressed";

            var dragHandler = new GameObject("DCC.UpdatePrefix.DragHandler");
            dragHandler.transform.parent = transform;
            dragHandler.transform.localPosition = Vector3.zero;
            Drag = dragHandler.AddComponent<UIDragHandle>();
            Drag.enabled = true;
            Drag.BringToFront();
        }

        public override void Start()
        {
            base.Start();
            
            absolutePosition = new Vector3(300f, 300f);            
            lblCaption.relativePosition = new Vector3(10f, 10f);
            txtPrefix.relativePosition = new Vector3(15f, 50f);

            btnOk.anchor = UIAnchorStyle.Left | UIAnchorStyle.Bottom;
            btnOk.relativePosition = new Vector3(10f, (size.y - btnOk.size.y) - 10f);
            btnOk.eventClick += FrmUpdateBuildingPrefix_eventClick;

            txtPrefix.Focus();
        }

        private void FrmUpdateBuildingPrefix_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            string prefixText = txtPrefix.text;
            /*BuildingManager
            DistrictManager = Singleton<DistrictManager>.instance.GetDistrictArea
            BuildingManager = Singleton<BuildingManager>.instance.get//.m_buildings.m_buffer[].m_position;

            //DistrictManager = Singleton<DistrictManager>.instance.m_districts.m_buffer[DistrictId]*/
        }

        public override void Update()
        {
            lblCaption.text = $"{DistrictName}: Update Building Prefixes";
            width = lblCaption.size.x + 30f;
            Invalidate();
        }
    }
}
