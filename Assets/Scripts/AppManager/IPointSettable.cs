using UnityEngine;

namespace HoloMeasurement.AppManager
{
    public interface IPointSettable
    {
        void SetPoint(GameObject prefab, Vector3 position);
    }
}
