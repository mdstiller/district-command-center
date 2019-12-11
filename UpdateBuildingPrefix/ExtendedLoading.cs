using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;
using ICities;
using UpdateBuildingPrefix.GUI.Panels.DistrictSummary;
using UpdateBuildingPrefix.GUI;

namespace UpdateBuildingPrefix
{
    public class ExtendedLoading : LoadingExtensionBase
    {
        public static bool IsGameLoaded { get; private set; }
        public static GUIBase BaseUI { get; private set; }   
        private DistrictCommandCenter DistrictCommandCenterComponent;
        
        public override void OnLevelLoaded(LoadMode mode)
        {
            SimulationManager.UpdateMode updateMode = SimulationManager.instance.m_metaData.m_updateMode;
            base.OnLevelLoaded(mode);

            IsGameLoaded = false;

            switch (updateMode)
            {
                case SimulationManager.UpdateMode.NewGameFromMap:
                case SimulationManager.UpdateMode.NewGameFromScenario:
                case SimulationManager.UpdateMode.LoadGame:
                    {
                        IsGameLoaded = true;
                        break;
                    }
                default:
                    return;

            }

            /*if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame &&
                mode != LoadMode.LoadScenario && mode != LoadMode.NewGameFromScenario)
                return;

            for (int i = 0; i < UIView.library.m_DynamicPanels.Length; i++)
                switch (UIView.library.m_DynamicPanels[i].instance.GetComponent<DistrictWorldInfoPanel>())
                {
                    case DistrictWorldInfoPanel panel:
                        DistrictInfoWindow districtWindow = panel.component.AddUIComponent<DistrictInfoWindow>();
                        districtWindow.size = panel.component.size;
                        districtWindow.baseDistrictWindow = panel;
                        districtWindow.position = new Vector3(0, 12);
                        break;
                }*/
            
            if(IsGameLoaded && BaseUI == null)
            {
                Debug.Log("Adding Base control UI.");
                BaseUI = ToolsModifierControl.toolController.gameObject.AddComponent<GUIBase>();
                
                /*if (BaseUI != null)
                    RegisterDcc();
                else
                    Debug.LogError("Can't find the UI View!");*/                
            }
        }
        void RegisterDcc()
        {
            GameObject districtCommandCenter = new GameObject("DistrictCommandCenter");

            if (districtCommandCenter == null)
                return;

            DistrictCommandCenterComponent = districtCommandCenter.AddComponent<DistrictCommandCenter>();
            //DistrictCommandCenterComponent.transform.parent = GameNativeUi.transform;
        }
        public override void OnReleased()
        {
            base.OnReleased();

            GUIBase.ReleaseTool();
        }
    }
}
