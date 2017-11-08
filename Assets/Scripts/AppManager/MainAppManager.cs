using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HoloMeasurement.AppManager
{
    public class MainAppManager : MonoBehaviour, IInputClickHandler
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
            AppStateManager.Instance.Sizer.SetPoint(_pointPrefab, position);
        }
    }
}