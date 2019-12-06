using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;
using UpdateBuildingPrefix.GUI.Helpers;
using UpdateBuildingPrefix.GUI.CustomTextures;

namespace UpdateBuildingPrefix.GUI
{
    public class GUIMainMenuButton: UIButton
    {
        private const string MAIN_MENU_BUTTON_BG_BASE = "DCC.MainButton.Base";
        private const string MAIN_MENU_BUTTON_BG_HOVERED = "DCC.MainButton.Hovered";
        private const string MAIN_MENU_BUTTON_BG_ACTIVE = "DCC.MainButton.BgActive";
        private const string MAIN_MENU_BUTTON_FG_BASE = "DCC.MainButton.FgBase";
        private const string MAIN_MENU_BUTTON_FG_HOVERED = "DCC.MainButton.FgHovered";
        private const string MAIN_MENU_BUTTON_FG_ACTIVE = "DCC.MainButton.FgActive";

        private const int BUTTON_WIDTH = 50;
        private const int BUTTON_HEIGHT = 50;

        private UIDragHandle Drag { get; set; }

        private IDisposable confDisposable_;

        public override void Start()
        {
            // Place the button.
            //OnUpdate(GlobalConfig.Instance);

            //confDisposable_ = GlobalConfig.Instance.Subscribe(this);

            // Set the atlas and background/foreground
            var spriteNames = new[] {
                MAIN_MENU_BUTTON_BG_BASE,
                MAIN_MENU_BUTTON_BG_HOVERED,
                MAIN_MENU_BUTTON_BG_ACTIVE,
                MAIN_MENU_BUTTON_FG_BASE,
                MAIN_MENU_BUTTON_FG_HOVERED,
                MAIN_MENU_BUTTON_FG_ACTIVE
            };
            atlas = TextureUtil.GenerateLinearAtlas(
                "DCC.Icons.Atlas",
                TextureResources.MainMenuButtonTexture2D,
                6,
                spriteNames);

            UpdateSprites();

            // Set the button dimensions.
            width = BUTTON_WIDTH;
            height = BUTTON_HEIGHT;

            // Enable button sounds.
            playAudioEvents = true;

            var dragHandler = new GameObject("DCC.MainButton.DragHandler");
            dragHandler.transform.parent = transform;
            dragHandler.transform.localPosition = Vector3.zero;
            Drag = dragHandler.AddComponent<UIDragHandle>();

            Drag.width = width;
            Drag.height = height;
            Drag.enabled = true;

            // Set up the tooltip
            var uiView = GetUIView();
            if (uiView != null)
            {
                m_TooltipBox = uiView.defaultTooltipBox;
            }

            UpdateTooltip();
        }

        /// <summary>
        /// Reset the tooltip (or set for the first time), or if keybinding has changed
        /// </summary>
        public void UpdateTooltip()
        {
            //tooltip = Translation.Menu.Get("Tooltip:Toggle Main Menu")
            //          + GetTooltip();
        }

        public override void OnDestroy()
        {
            confDisposable_?.Dispose();
        }

        internal void SetPosLock(bool lck)
        {
            Drag.enabled = !lck;
        }

        protected override void OnClick(UIMouseEventParameter p)
        {
            try
            {
                //Log._Debug($"Current tool: {ToolManager.instance.m_properties.CurrentTool}");                
                ExtendedLoading.BaseUI.ToggleMainMenu();
                UpdateSprites();
            }
            catch (Exception e)
            {
                Debug.LogError($"Display Main Menu Failed: {e.Message}\r\nStack Trace:\r\n{e.StackTrace.ToString()}");
            }
        }

        protected override void OnPositionChanged()
        {
            /*GlobalConfig config = GlobalConfig.Instance;

            bool posChanged = config.Main.MainMenuButtonX != (int)absolutePosition.x
                              || config.Main.MainMenuButtonY != (int)absolutePosition.y;

            if (posChanged)
            {
                //Log._Debug($"Button position changed to {absolutePosition.x}|{absolutePosition.y}");

                //config.Main.MainMenuButtonX = (int)absolutePosition.x;
                //config.Main.MainMenuButtonY = (int)absolutePosition.y;

                //GlobalConfig.WriteConfig();
            }*/

            base.OnPositionChanged();
        }

        internal void UpdateSprites()
        {
            if (!ExtendedLoading.BaseUI.IsVisible())
            {
                m_BackgroundSprites.m_Normal = m_BackgroundSprites.m_Disabled =
                                                   m_BackgroundSprites.m_Focused =
                                                       MAIN_MENU_BUTTON_BG_BASE;
                m_BackgroundSprites.m_Hovered = MAIN_MENU_BUTTON_BG_HOVERED;
                m_PressedBgSprite = MAIN_MENU_BUTTON_BG_ACTIVE;

                m_ForegroundSprites.m_Normal = m_ForegroundSprites.m_Disabled =
                                                   m_ForegroundSprites.m_Focused =
                                                       MAIN_MENU_BUTTON_FG_BASE;
                m_ForegroundSprites.m_Hovered = MAIN_MENU_BUTTON_FG_HOVERED;
                m_PressedFgSprite = MAIN_MENU_BUTTON_FG_ACTIVE;
            }
            else
            {
                m_BackgroundSprites.m_Normal = m_BackgroundSprites.m_Disabled =
                                                   m_BackgroundSprites.m_Focused =
                                                       m_BackgroundSprites.m_Hovered =
                                                           MAIN_MENU_BUTTON_BG_ACTIVE;
                m_PressedBgSprite = MAIN_MENU_BUTTON_BG_HOVERED;

                m_ForegroundSprites.m_Normal = m_ForegroundSprites.m_Disabled =
                                                   m_ForegroundSprites.m_Focused =
                                                       m_ForegroundSprites.m_Hovered =
                                                           MAIN_MENU_BUTTON_FG_ACTIVE;
                m_PressedFgSprite = MAIN_MENU_BUTTON_FG_HOVERED;
            }

            this.Invalidate();
        }

        /*public void OnUpdate(GlobalConfig config)
        {
            UpdatePosition(new Vector2(config.Main.MainMenuButtonX, config.Main.MainMenuButtonY));
        }*/

        public void UpdatePosition(Vector2 pos)
        {
            Rect rect = new Rect(pos.x, pos.y, BUTTON_WIDTH, BUTTON_HEIGHT);
            Vector2 resolution = UIView.GetAView().GetScreenResolution();
            VectorUtil.ClampRectToScreen(ref rect, resolution);
            //Log.Info($"Setting main menu button position to [{pos.x},{pos.y}]");
            absolutePosition = rect.position;
            Invalidate();
        }

        public void OnGUI()
        {
            /*if (!UIView.HasModalInput()
                && !UIView.HasInputFocus()
                && KeybindSettingsBase.ToggleMainMenu.IsPressed(Event.current))
            {
                if (ExtendedLoading.BaseUI != null)
                {
                    ExtendedLoading.BaseUI.ToggleMainMenu();
                }
            }*/
        }

        private string GetTooltip()
        {
            return string.Empty;
            //return KeybindSettingsBase.ToggleMainMenu.ToLocalizedString("\n");
        }
    }
}
