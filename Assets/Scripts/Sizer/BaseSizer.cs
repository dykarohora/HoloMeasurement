using HoloMeasurement.Figure;
using HoloMeasurement.UserOperation;
using UniRx;
using UnityEngine;

namespace HoloMeasurement.Sizer
{
    public abstract class BaseSizer : MonoBehaviour, IPointSettable
    {
        [SerializeField]
        protected GameObject _linePrefab;

        protected ReactiveCollection<Point> _pointList = new ReactiveCollection<Point>();

        public virtual void SetPoint(GameObject prefab, Vector3 position)
        {
            var go = Instantiate(prefab, position, Quaternion.identity);
            var point = go.GetComponent<Point>();
            point.SetPosition(position);
            _pointList.Add(point);
        }

        protected GameObject GenerateLine(Point previous, Point last)
        {
            var previousPos = previous.Position.Value;
            var lastPos = last.Position.Value;

            var centerPos = (lastPos + previousPos) * 0.5f;
            var direction = lastPos - previousPos;
            var distance = Vector3.Distance(lastPos, previousPos);

            var line = Instantiate(_linePrefab, centerPos, Quaternion.LookRotation(direction));
            line.transform.localScale = new Vector3(0.005f, 0.005f, distance);

            return line;
        }
    }
}
