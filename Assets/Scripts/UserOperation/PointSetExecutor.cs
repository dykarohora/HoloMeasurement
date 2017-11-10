using HoloMeasurement.Sizer.Impl;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UniRx;
using HoloMeasurement.AppManager;

namespace HoloMeasurement.UserOperation
{
    public class PointSetExecutor : MonoBehaviour, IInputClickHandler
    {
        [SerializeField]
        private GameObject _pointPrefab;

        [SerializeField]
        private LineSizer _lineSizer;
        [SerializeField]
        private PolygonSizer _polygonSizer;

        private IPointSettable _sizer;

        private void Start()
        {
            InputManager.Instance.PushFallbackInputHandler(gameObject);
            AppStateManager.Instance.CurrentState
                .Subscribe(state =>
                {
                    switch(state)
                    {
                        case AppState.Line:
                            _sizer = _lineSizer;
                            break;
                        case AppState.Polygon:
                            _sizer = _polygonSizer;
                            break;
                        case AppState.Solid:
                            break;
                    }
                })
                .AddTo(gameObject);
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            var position = GazeManager.Instance.HitPosition;
            _sizer.SetPoint(_pointPrefab, position);
        }
    }
}