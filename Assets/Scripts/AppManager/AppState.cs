using System;
using UniRx;

namespace HoloMeasurement.AppManager
{
    public enum AppState
    {
        Line,
        Polygon,
        Solid
    }

    [Serializable]
    public class AppStateReactiveProperty : ReactiveProperty<AppState>
    {
        public AppStateReactiveProperty() { }
        public AppStateReactiveProperty(AppState initialState) : base(initialState) { }
    }
}