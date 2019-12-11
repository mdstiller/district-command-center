using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UpdateBuildingPrefix
{
    /// <summary>
    /// Each building/citizen/segment may simultaneously belong to a district and/or a park.
    /// </summary>
    public struct DistrictParkHelper : IComparable<DistrictParkHelper>
    {
        /// <summary>
        /// Max number of districts + parks.
        /// </summary>
        public const int MAX_DISTRICT_PARK_COUNT = DistrictManager.MAX_DISTRICT_COUNT + DistrictManager.MAX_DISTRICT_COUNT;

        public byte District;
        public byte Park;

        /// <summary>
        /// Constructs a DistrictPark from the given district.
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        public static DistrictParkHelper FromDistrict(byte district)
        {
            return new DistrictParkHelper
            {
                District = district
            };
        }

        /// <summary>
        /// Constructs a DistrictPark from the given park.
        /// </summary>
        /// <param name="park"></param>
        /// <returns></returns>
        public static DistrictParkHelper FromPark(byte park)
        {
            return new DistrictParkHelper
            {
                Park = park
            };
        }

        /// <summary>
        /// Returns the district and/or park covering the given position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static DistrictParkHelper FromPosition(Vector3 position)
        {
            return new DistrictParkHelper
            {
                District = DistrictManager.instance.GetDistrict(position),
                Park = DistrictManager.instance.GetPark(position)
            };
        }

        /// <summary>
        /// Lists all current districts and parks, sorted by alphabetical order.
        /// </summary>
        /// <returns></returns>
        public static List<DistrictParkHelper> GetAllDistrictParks()
        {
            var dps = new List<DistrictParkHelper>();

            for (byte district = 1; district < DistrictManager.MAX_DISTRICT_COUNT; district++)
            {
                if ((DistrictManager.instance.m_districts.m_buffer[district].m_flags & global::District.Flags.Created) != 0)
                {
                    dps.Add(new DistrictParkHelper
                    {
                        District = district
                    });
                }
            }

            for (byte park = 1; park < DistrictManager.MAX_DISTRICT_COUNT; park++)
            {
                if ((DistrictManager.instance.m_parks.m_buffer[park].m_flags & global::DistrictPark.Flags.Created) != 0)
                {
                    dps.Add(new DistrictParkHelper
                    {
                        Park = park
                    });
                }
            }

            dps.Sort();
            return dps;
        }

        /// <summary>
        /// Returns true if this struct does not refer to a valid district or park.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return District == 0 && Park == 0;
            }
        }

        /// <summary>
        /// The name of the district and/or park.
        /// </summary>
        public string Name
        {
            get
            {
                if (District != 0 && Park != 0)
                {
                    var districtName = DistrictManager.instance.GetDistrictName(District);
                    var parkName = DistrictManager.instance.GetParkName(Park);
                    return $"{districtName}/{parkName}";
                }
                else if (District != 0)
                {
                    var districtName = DistrictManager.instance.GetDistrictName(District);
                    return $"{districtName}";
                }
                else if (Park != 0)
                {
                    var parkName = DistrictManager.instance.GetParkName(Park);
                    return $"{parkName}";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public int Population
        {
            get
            {
                if(District !=0)
                {
                    var districtPopulation = DistrictManager.instance.m_districts.m_buffer[District].m_populationData.m_finalCount;
                    return (int)districtPopulation;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int ElectricityConsumption
        {
            get
            {
                if (District != 0)
                {
                    var elecConsumption = DistrictManager.instance.m_districts.m_buffer[District].GetElectricityConsumption();
                    return elecConsumption;
                }
                else
                    return -1;
            }
        }

        public int ElectricityCapacity
        {
            get
            {
                if (District != 0)
                {
                    var elecCapacity = DistrictManager.instance.m_districts.m_buffer[District].GetElectricityCapacity();
                    return elecCapacity;
                }
                else
                    return -1;
            }
        }

        /// <summary>
        /// Returns true if the other struct has either a district and/or park in common with this one.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsServedBy(DistrictParkHelper other)
        {
            if (IsEmpty && other.IsEmpty)
            {
                return true;
            }

            return
                (District == other.District && District != 0) ||
                (Park == other.Park && Park != 0);
        }

        /// <summary>
        /// Returns true if this object "is served by" one of the elements in the given collection.
        /// </summary>
        /// <param name="districtParks"></param>
        /// <returns></returns>
        public bool IsServedBy(IEnumerable<DistrictParkHelper> districtParks)
        {
            if (districtParks == null)
            {
                return false;
            }

            foreach (var districtPark in districtParks)
            {
                if (IsServedBy(districtPark))
                {
                    return true;
                }
            }

            return false;
        }

        public District GetDistrictObject()
        {
            //if (District != 0)
                return DistrictManager.instance.m_districts.m_buffer[District];
            //else
                //return ;
        }
        #region Equality/Comparison

        /// <summary>
        /// For sorting ... we do this by name ...
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DistrictParkHelper other)
        {
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DistrictParkHelper other)
            {
                return District == other.District && Park == other.Park;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return District + Park;
        }

        #endregion

        #region Serialization

        /// <summary>
        /// The lower 8 bytes contains the district.  The upper 8 bytes contains the park.
        /// </summary>
        /// <param name="districtPark"></param>
        /// <returns></returns>
        public static DistrictParkHelper FromSerializedInt(int districtPark)
        {
            return new DistrictParkHelper
            {
                District = (byte)(districtPark & 255),
                Park = (byte)(districtPark >> 8)
            };
        }

        /// <summary>
        /// Packs this struct as an int.
        /// </summary>
        /// <returns></returns>
        public int ToSerializedInt()
        {
            return ((int)District) | (((int)Park) << 8);
        }

        #endregion
    }
}
