using System;
using ICities;
using UnityEngine;
using System.Reflection;

namespace UpdateBuildingPrefix
{
    public class UpdateBuildingPrefix : IUserMod
    {
        public string Name => "Update Building Prefix per District";

        public string Description => "This doesn't work yet.";

        public static readonly string ModName = $"District Control Center - v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group = helper.AddGroup("Update Building Prefixes - Version 1.0");          
            group.AddCheckbox("Enable Mod", true, enableDistrictPrefix =>
            {
                Debug.Log("Mod was bungled.");
            });
        }
    }
}