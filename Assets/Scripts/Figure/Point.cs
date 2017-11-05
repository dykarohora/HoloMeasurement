using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloMeasurement.Figure
{
    public class Point : MonoBehaviour
    {
        private ReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
        public IReadOnlyReactiveProperty<Vector3> Position {
            get { return _position; }
        }

        public void SetPosition (Vector3 position)
        {
            _position.Value = position;
            Debug.Log("Point pos:" + _position.Value.ToString());
        }

            // TODO: 移動のしくみは後で考える
    }
}
