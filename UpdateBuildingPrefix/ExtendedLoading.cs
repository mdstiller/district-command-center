using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;
using ICities;
using UpdateBuildingPrefix.GUI;
using UpdateBuildingPrefix.Panels;

namespace UpdateBuildingPrefix
{
    public class ExtendedLoading : LoadingExtensionBase
    {
        public static bool IsGameLoaded { get; private set; }
        public static GUIBase BaseUI { get; private set; }
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
            }
        }
    }
}
