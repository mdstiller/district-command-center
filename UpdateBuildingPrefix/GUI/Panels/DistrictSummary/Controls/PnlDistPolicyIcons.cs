using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework.UI;
using UnityEngine;
using UpdateBuildingPrefix.Helpers;

namespace UpdateBuildingPrefix.GUI.Panels.DistrictSummary.Controls
{
    public class PnlDistPolicyIcons : UIPanel
    {        
        public byte DistrictId { get; set; }
        public List<BtnPolicyIcon> PolicyIcons { get; private set; } = new List<BtnPolicyIcon>();
        public override void Awake()
        {
            autoLayoutDirection = LayoutDirection.Horizontal;
            wrapLayout = true;
            autoLayout = true;
            size = new Vector2(700, 60);
            autoSize = true;
            anchor = UIAnchorStyle.Right;
        }

        public override void Start()
        {
            relativePosition = new Vector3(160f, 110f);

            var values = Enum.GetValues(typeof(DistrictPolicies.Policies));
            
            foreach (DistrictPolicies.Policies policy in values)
            {
                BtnPolicyIcon policyIcon = AddUIComponent<BtnPolicyIcon>();
                policyIcon.PolicyName = policy.ToString();
                policyIcon.Policy = policy;
            }
          
            District district = DistrictParkHelper.FromDistrict(DistrictId).GetDistrictObject();

            string dps = $"{district.m_cityPlanningPolicies}, {district.m_eventPolicies}, {district.m_servicePolicies}, {district.m_specializationPolicies}, {district.m_specialPolicies}, {district.m_taxationPolicies}";
            dps = dps.Replace("None,", "").Replace(" ", "");
            String[] activePolicies = dps.Split(',');

            foreach (string activePolicy in activePolicies)
            {
                foreach (BtnPolicyIcon policyIcon in PolicyIcons)
                {
                    if (policyIcon.PolicyName.Equals(activePolicy))
                    {
                        policyIcon.color = new Color32(100, 200, 100, 50);
                        policyIcon.Invalidate();
                    }
                }
            }
        }
    }
}
