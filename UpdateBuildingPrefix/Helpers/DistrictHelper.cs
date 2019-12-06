using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICities;
using ColossalFramework;
using UnityEngine;

namespace UpdateBuildingPrefix.Helpers
{
    public sealed class DistrictHelper
    {
        private DistrictManagerHelper _distManagerHelper;
        private District _currentDistrict;
        private ushort _districtId;

        public uint Population
        {
            get
            {
                return _currentDistrict.m_populationData.m_finalCount;
            }
        }

        public int WorkerCount
        {
            get
            {
                return _currentDistrict.GetWorkerCount();
            }
        }

        public int Workplaces
        {
            get
            {
                return _currentDistrict.GetWorkplaceCount();
            }
        }

        /*public string Name {
            get
            {
                return _distManagerHelper.GetDistrictManagerInstance().GetDistrictName(_districtId);
            }
        }*/

        
        //return _distManagerHelper.DistrictManagerInstance.m_districts.m_buffer[districtId];
         
    }
}
