using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using System.IO;
using UnityStandardAssets.ImageEffects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ColossalFramework.UI;
using System.ComponentModel;
using ColossalFramework;
using System.Reflection;

namespace UpdateBuildingPrefix.Helpers
{

    public static class CameraHelper
    {
        /// <summary>
        /// Move the camera to a building. Also shows how to create an InstanceId from scratch.
        /// </summary>
        /// <param name="index">The index of the building in BuildingManager.instance.m_buildings.m_buffer</param>
        public static void MoveCameraToDistrict(byte index)
        {
            var instanceID = default(InstanceID);
            instanceID.District = index;
            MoveCameraToInstance(instanceID);        
        }

        /// <summary>
        /// Move the camera to something with an instance id
        /// </summary>
        /// <param name="instanceID"></param>
        private static void MoveCameraToInstance(InstanceID instanceID)
        {
            ToolsModifierControl.cameraController.SetTarget(instanceID, ToolsModifierControl.cameraController.transform.position, true);
        }
        /*if (!playBack)
        {
            return;
        }
        if (CameraDirector.useFps)
        {
            if (stepcount == 0L)
            {
                startTime = Stopwatch.GetTimestamp();
            }
            if (!MoveCamera(time, CameraDirector.mainWindow.playSpeed, CameraDirector.camera, knots, out time))
            {
                Stop();
                return;
            }
            simulationStep.Invoke(Singleton<SimulationManager>.instance, null);
            CameraDirector.camera.nearClipPlane = 1f;
            long num = startTime + (int)(waitTimeTarget * frequency * (float)stepcount++);
            int num2 = (int)(waitTimeTarget - (float)(Stopwatch.GetTimestamp() - num) / frequency) - 1;
            if (num2 > 0)
            {
                Thread.Sleep(num2);
            }
            while (playBack && (float)(Stopwatch.GetTimestamp() - num) / frequency < waitTimeTarget)
            {
            }
        }
        else if (!MoveCamera(time, CameraDirector.mainWindow.playSpeed, CameraDirector.camera, knots, out time))
        {
            Stop();
        }
    }*/

    }

    public class Knot
    {
        private CameraController _cameraController;
        private Camera _camera;

        public Vector3 position;

        public Quaternion rotation;

        public float size;

        public float height;

        [DefaultValue(2f)]
        public float duration = 2f;

        [DefaultValue(0f)]
        public float delay;

        [DefaultValue(45f)]
        public float fov;

        [DefaultValue(EasingMode.Auto)]
        public EasingMode mode = EasingMode.Auto;

        public Vector3 cameraPosition
        {
            get
            {
                float num = size * (1f - height / _cameraController.m_maxDistance) / Mathf.Tan((float)Math.PI / 180f * fov);
                Vector3 vector = position + rotation * new Vector3(0f, 0f, 0f - num);
                vector.y += CalculateCameraHeightOffset(vector, num);
                return vector;
            }
        }

        public Knot()
        {           
            CaptureCamera();
        }

        public void CaptureCamera()
        {
            position = _cameraController.m_currentPosition;
            size = _cameraController.m_currentSize;
            height = _cameraController.m_currentHeight;
            fov = _camera.fieldOfView;
            float num = size * (1f - height / _cameraController.m_maxDistance) / Mathf.Tan((float)Math.PI / 180f * fov);
            Vector2 currentAngle = _cameraController.m_currentAngle;
            rotation = Quaternion.AngleAxis(currentAngle.x, Vector3.up) * Quaternion.AngleAxis(currentAngle.y, Vector3.right);
            Vector3 worldPos = position + rotation * new Vector3(0f, 0f, 0f - num);
            position.y += CalculateCameraHeightOffset(worldPos, num);
        }

        private static float CalculateCameraHeightOffset(Vector3 worldPos, float distance)
        {
            float num = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(worldPos, true, 2f);
            float a = num - worldPos.y;
            distance *= 0.45f;
            a = Mathf.Max(a, 0f - distance);
            a += distance * 0.375f * Mathf.Pow(1f + 1f / distance, 0f - a);
            num = worldPos.y + a;
            ItemClass.Availability availability = Singleton<ToolManager>.instance.m_properties.m_mode;
            if ((availability & ItemClass.Availability.AssetEditor) == 0)
            {
                ItemClass.Layer layer = ItemClass.Layer.Default;
                if ((availability & ItemClass.Availability.MapEditor) != 0)
                {
                    layer |= ItemClass.Layer.Markers;
                }
                worldPos.y -= 5f;
                num = Mathf.Max(num, Singleton<BuildingManager>.instance.SampleSmoothHeight(worldPos, layer) + 5f);
                num = Mathf.Max(num, Singleton<NetManager>.instance.SampleSmoothHeight(worldPos) + 5f);
                num = Mathf.Max(num, Singleton<PropManager>.instance.SampleSmoothHeight(worldPos) + 5f);
                num = Mathf.Max(num, Singleton<TreeManager>.instance.SampleSmoothHeight(worldPos) + 5f);
                worldPos.y += 5f;
            }
            return num - worldPos.y;
        }
    }

    public class Spline
    {
        public static float CalculateSplineT(object[] points, int size, int i, float t)
        {
            float duration = ((Knot)points[i]).duration;
            float num = (i <= 0 || ((Knot)points[i]).delay != 0f) ? 0f : (2f * duration / (duration + ((Knot)points[i - 1]).duration));
            float num2 = (i >= size - 2 || ((Knot)points[i + 1]).delay != 0f) ? 0f : (2f * duration / (duration + ((Knot)points[i + 1]).duration));
            return (t * t * t - 2f * t * t + t) * num + (-2f * t * t * t + 3f * t * t) + (t * t * t - t * t) * num2;
        }

        public static Vector3 CalculateSplinePosition(object[] points, int size, int i, float t)
        {
            Vector3 position = ((Knot)points[i]).position;
            Vector3 position2 = ((Knot)points[i + 1]).position;
            Vector3 val = (i <= 0) ? (position2 - position) : (0.5f * (position2 - ((Knot)points[i - 1]).position));
            Vector3 val2 = (i >= size - 2) ? (position2 - position) : (0.5f * (((Knot)points[i + 2]).position - position));
            return (2f * t * t * t - 3f * t * t + 1f) * position + (t * t * t - 2f * t * t + t) * val + (-2f * t * t * t + 3f * t * t) * position2 + (t * t * t - t * t) * val2;
        }

        public static Quaternion CalculateSplineRotationEuler(object[] points, int size, int i, float t)
        {
            Vector3 eulerAngles = ((Knot)points[i]).rotation.eulerAngles;
            Vector3 a = ClosestAngle(eulerAngles, ((Knot)points[i + 1]).rotation.eulerAngles);
            Vector3 a2 = (i <= 0) ? (a - eulerAngles) : (0.5f * (a - ClosestAngle(eulerAngles, ((Knot)points[i - 1]).rotation.eulerAngles)));
            Vector3 a3 = (i >= size - 2) ? (a - eulerAngles) : (0.5f * (ClosestAngle(a, ((Knot)points[i + 2]).rotation.eulerAngles) - eulerAngles));
            return Quaternion.Euler((2f * t * t * t - 3f * t * t + 1f) * eulerAngles + (t * t * t - 2f * t * t + t) * a2 + (-2f * t * t * t + 3f * t * t) * a + (t * t * t - t * t) * a3);
        }

        public static Vector3 ClosestAngle(Vector3 a, Vector3 b)
        {
            Vector3 val = a - b;
            if (val.x > 180f)
            {
                b.x += 360f;
            }
            else if (val.x < -180f)
            {
                b.x -= 360f;
            }
            if (val.y > 180f)
            {
                b.y += 360f;
            }
            else if (val.y < -180f)
            {
                b.y -= 360f;
            }
            if (val.z > 180f)
            {
                b.z += 360f;
            }
            else if (val.z < -180f)
            {
                b.z -= 360f;
            }
            return b;
        }
    }

    public enum EasingMode
    {
        None,
        Auto,
        EaseIn,
        EaseOut,
        EaseInOut
    }
}
