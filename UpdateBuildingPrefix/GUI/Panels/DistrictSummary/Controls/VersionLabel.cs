using ColossalFramework.UI;
using UnityEngine;
using System.Reflection;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class VersionLabel : UILabel
    {
        public override void Awake()
        {
            text = UpdateBuildingPrefix.ModName;
            relativePosition = new Vector3(15f, 5f);
            textAlignment = UIHorizontalAlignment.Center;

            Debug.Log("Version Label added.");
        }
    }
}
