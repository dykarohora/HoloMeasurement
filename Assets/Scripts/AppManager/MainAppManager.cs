using HoloMeasurement.Sizer.Impl;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HoloMeasurement.AppManager
{
    public class MainAppManager : MonoBehaviour, IInputClickHandler
    {
        [SerializeField]
        private GameObject _pointPrefab;

        // Driver
        private LineSizer _lineSizer;

        private void Start()
        {
            // Driver
            _lineSizer = GetComponent<LineSizer>();
            
            InputManager.Instance.PushFallbackInputHandler(gameObject);
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            var pointSetter = _lineSizer.GetComponent<IPointSettable>();
            if(pointSetter != null)
            {
                var position = GazeManager.Instance.HitPosition;
                pointSetter.SetPoint(_pointPrefab, position);
            }
        }
    }
}