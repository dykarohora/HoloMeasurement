using HoloMeasurement.Sizer.Impl;
using HoloToolkit.Unity;
using UnityEngine;
using UniRx;

namespace HoloMeasurement.AppManager
{
    public class AppStateManager : Singleton<AppStateManager>
    {
        public AppStateReactiveProperty CurrentState { get; set; } = new AppStateReactiveProperty(AppState.Line);
    }
}
