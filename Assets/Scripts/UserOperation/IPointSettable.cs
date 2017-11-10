using UnityEngine;

namespace HoloMeasurement.UserOperation
{
    public interface IPointSettable
    {
        void SetPoint(GameObject prefab, Vector3 position);
    }
}
