using UnityEngine;
using UniRx;
using UniRx.Triggers;
using HoloToolkit.Unity.InputModule;

namespace HoloMeasurement.Figure
{
    public class Point : MonoBehaviour, IManipulationHandler, IPointMovable
    {
        private Vector3ReactiveProperty _position = new Vector3ReactiveProperty();
        public IReadOnlyReactiveProperty<Vector3> Position {
            get { return _position; }
        }

        // public bool IsLineElem { get; set; }

        private bool _isManipulating;
        private Vector3 _lastNavigatePos;
        private Vector3 _navigateVelocity;
        private Vector3 _smoothVelocity;

        public void SetPosition (Vector3 position)
        {
            _position.Value = position;
        }

        public void OnManipulationStarted(ManipulationEventData eventData)
        {
            InputManager.Instance.OverrideFocusedObject = gameObject;
            _isManipulating = true;
            _lastNavigatePos = eventData.CumulativeDelta;
        }

        public void OnManipulationUpdated(ManipulationEventData eventData)
        {
            var currentPos = eventData.CumulativeDelta;
            _navigateVelocity = _lastNavigatePos - currentPos;
            _lastNavigatePos = currentPos;
            _smoothVelocity = Vector3.Lerp(_smoothVelocity, _navigateVelocity, 0.5f);
        }

        public void OnManipulationCanceled(ManipulationEventData eventData)
        {
            InputManager.Instance.OverrideFocusedObject = null;
            _isManipulating = false;
        }

        public void OnManipulationCompleted(ManipulationEventData eventData)
        {
            InputManager.Instance.OverrideFocusedObject = null;
            _isManipulating = false;
        }

        private void Start()
        {
            _isManipulating = false;

            this.UpdateAsObservable()
                .Where(_ => _isManipulating)
                .Subscribe(_ => {
                    transform.position -= (_smoothVelocity * 10f);
                    _position.Value = transform.position;
                });
        }
    }
}
