using HoloMeasurement.AppManager;
using UnityEngine;
using UniRx;
using HoloMeasurement.Figure;
using HoloMeasurement.Figure.Impl;

namespace HoloMeasurement.Sizer.Impl
{
    public class LineSizer : MonoBehaviour, IPointSettable
    {
        [SerializeField]
        private GameObject _linePrefab;

        private ReactiveCollection<Point> _pointList = new ReactiveCollection<Point>();

        private void Start()
        {
            _pointList
                .ObserveAdd()
                .Where(_ =>
                {
                    return _pointList.Count % 2 == 0 ? true : false;
                })
                .Subscribe(_ =>
                {
                    var index = _pointList.Count - 1;
                    var lastPoint = _pointList[index];
                    var previoudPoint = _pointList[index - 1];
                    LineGenerate(lastPoint, previoudPoint);
                })
                .AddTo(gameObject);
        }

        public void SetPoint(GameObject prefab, Vector3 position)
        {
            var go = Instantiate(prefab, position, Quaternion.identity);
            var point = go.GetComponent<Point>();
            point.SetPosition(position);
            _pointList.Add(point);
        }

        private void LineGenerate(Point last, Point previous)
        {
            var lastPos = last.Position.Value;
            var previousPos = previous.Position.Value;

            var centerPos = (lastPos + previousPos) * 0.5f;
            var direction = lastPos - previousPos;
            var distance = Vector3.Distance(lastPos, previousPos);

            var lineObj = Instantiate(_linePrefab, centerPos, Quaternion.LookRotation(direction));
            lineObj.transform.localScale = new Vector3(distance, 0.005f, 0.005f);
            lineObj.transform.Rotate(Vector3.down, 90.0f);

            var Root = new GameObject();
            Root.name = "Line";
            last.transform.parent = Root.transform;
            previous.transform.parent = Root.transform;
            lineObj.transform.parent = Root.transform;

            var line = Root.AddComponent<Line>();
            line.Initialize(previous, last, lineObj);
        }
    }
}