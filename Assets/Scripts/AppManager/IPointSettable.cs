using UnityEngine;

namespace HoloMeasurement.AppManager
{
    interface IPointSettable
    {
        void SetPoint(GameObject prefab, Vector3 position);
    }
}
