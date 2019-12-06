using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ICities;
using UnityEngine;
using ColossalFramework.UI;

namespace UpdateBuildingPrefix.Panels
{
    public class DistrictInfoWindow : UIPanel
    {
        const float _vertPadding = 10;
        //UI Elements
        UITextField _districtName;
        ushort _selectedDistrict;
        UILabel _descriptionLabel;
        ColossalFramework.UI.UIButton _updateBuildingPrefix;        

        public DistrictWorldInfoPanel baseDistrictWindow;     

        public override void Awake()
        {
            Debug.Log("District Panel: Awake()");
            _updateBuildingPrefix = AddUIComponent<UIButton>();
            _descriptionLabel = AddUIComponent<UILabel>();                
            base.Awake();
            Debug.Log("District Panel: End Awake()");
        }

        public override void Start()
        {
            float y = _vertPadding;

            Debug.Log("District Panel: Start()");

            _updateBuildingPrefix.text = "Update Building Prefixes";
            _updateBuildingPrefix.autoSize = false;
            _updateBuildingPrefix.width = size.x;
            _updateBuildingPrefix.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left | UIAnchorStyle.Right;

            /*UISprite sprite = baseDistrictWindow.GetComponentInChildren<UISprite>();

            backgroundSprite = "MenuPanel2";
            isVisible = true;
            canFocus = true;
            isInteractive = true;

            _barWidth = size.x - 28;*/
            

            _descriptionLabel.textScale = 0.7f;
            _descriptionLabel.wordWrap = true;
            _descriptionLabel.autoSize = false;
            _descriptionLabel.autoHeight = true;
            _descriptionLabel.width = size.x;
            _descriptionLabel.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left | UIAnchorStyle.Right;

            y += _vertPadding;
            height = y;

            base.Start();
        }

        public override void Update()
        {
            if (!WorldInfoPanel.AnyWorldInfoPanelOpen())
                return;

            var instanceId = WorldInfoPanel.GetCurrentInstanceID();
            if(instanceId.Type == InstanceType.District && instanceId.District != 0)
            {
                ushort district = instanceId.District;
                if(baseDistrictWindow != null && enabled && isVisible && Singleton<DistrictManager>.exists && ((Singleton<SimulationManager>.instance.m_currentFrameIndex & 15u) == 15u || _selectedDistrict != district))
                {
                    DistrictManager instance = Singleton<DistrictManager>.instance;
                    UpdateDistrictInfo(district, instance.m_districts.m_buffer[district]);
                    _selectedDistrict = district;
                }
            }
        }
        private void UpdateDistrictInfo(ushort districtId, District district)
        {
            if (baseDistrictWindow != null)
            {
                _districtName = baseDistrictWindow.Find<UITextField>("DistrictName");

                _updateBuildingPrefix.Show();
                _updateBuildingPrefix.relativePosition = new Vector3(14, -40);

                _descriptionLabel.text = string.Format("I did it. I added a custom description for district {0}. The population is now {1}.", _districtName.text, district.m_populationData.m_finalCount);
                _descriptionLabel.Show();
                _descriptionLabel.relativePosition = new Vector3(14, -70);

                if (_districtName == null)
                {
                    
                    _districtName.maxLength = 70;
                    _districtName.textScale = 0.5f;
                }
                if(_districtName != null)
                {
                    var districtName = _districtName.text;

                    _districtName.text = districtName ?? _districtName.text;
                }
            }
    }
    }
    


}
