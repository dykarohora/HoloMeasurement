using HoloMeasurement.AppManager;
using UnityEngine;

namespace HoloMeasurement.Figure
{
    public abstract class BaseFigure : MonoBehaviour, IDeletable
    {
        public abstract void DeleteFigure();
    }
}