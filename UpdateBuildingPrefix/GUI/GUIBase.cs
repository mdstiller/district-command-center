using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;
using UpdateBuildingPrefix.GUI.Panels.DistrictSummary;

namespace UpdateBuildingPrefix.GUI
{
    public class GUIBase : UICustomControl
    {
        public GUIMainMenuButton MainMenuButton { get; }
        public DistrictCommandCenter MainMenu { get; private set; }
        private bool _uiShown;
        
        public GUIBase()
        {
            UIView uiView = UIView.GetAView();

            //Add the Main Menu button
            MainMenuButton = (GUIMainMenuButton)uiView.AddUIComponent(typeof(GUIMainMenuButton));
            Debug.Log("The Main Menu button has been added.");

            MainMenu = (DistrictCommandCenter)uiView.AddUIComponent(typeof(DistrictCommandCenter));
            Debug.Log("The Main Menu has been added.");
        }

        ~GUIBase()
        {
            UnityEngine.Object.Destroy(MainMenuButton);
            UnityEngine.Object.Destroy(MainMenu);
        }

        public bool IsVisible()
        {
            return _uiShown;
        }

        public void ToggleMainMenu()
        {
            if (IsVisible())
            {
                Close();
            }else
            {
                Show();
            }
        }

        public void Show()
        {
            try
            {
                ToolsModifierControl.mainToolbar.CloseEverything();
            } catch (Exception e)
            {
                Debug.Log("Error on Show(): " + e);
            }

            GetMenu().Show();
            _uiShown = true;
            MainMenuButton.UpdateSprites();
            UIView.SetFocus(MainMenu);
        }

        public void Close()
        {
            GetMenu().Hide();
            _uiShown = false;
            MainMenuButton.UpdateSprites();
        }

        internal DistrictCommandCenter GetMenu()
        {
            return MainMenu;
        }
    }
}
