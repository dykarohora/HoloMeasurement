using HoloMeasurement.Sizer.Impl;
using HoloToolkit.Unity;
using UnityEngine;
using UniRx;

namespace HoloMeasurement.AppManager
{
    public class AppStateManager : Singleton<AppStateManager>
    {
        public AppStateReactiveProperty CurrentState { get; set; } = new AppStateReactiveProperty(AppState.Line);

        public IPointSettable Sizer { get; private set; }

        [SerializeField]
        private LineSizer _linesizer;


        private void Start()
        {
            CurrentState.Subscribe(state =>
            {
                switch(state)
                {
                    case AppState.Line:
                        Sizer = _linesizer;
                        break;
                    case AppState.Polygon:
                        break;
                    case AppState.Solid:
                        break;
                }
            });
        }
    }
}
