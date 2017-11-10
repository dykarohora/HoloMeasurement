using HoloMeasurement.Sizer.Impl;
using HoloToolkit.Unity;
using UnityEngine;
using UniRx;

namespace HoloMeasurement.AppManager
{
    public class AppStateManager : Singleton<AppStateManager>
    {
        private AppStateReactiveProperty _currentState = new AppStateReactiveProperty(AppState.Line);
        public AppStateReactiveProperty CurrentState { get { return _currentState; } }
    }
}
