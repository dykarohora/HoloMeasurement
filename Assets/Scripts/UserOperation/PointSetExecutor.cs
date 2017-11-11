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

        private void Start()
        {
            InputManager.Instance.PushFallbackInputHandler(gameObject);
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            var position = GazeManager.Instance.HitPosition;
            SizerManager.Instance.Sizer.SetPoint(_pointPrefab, position);
        }
    }
}